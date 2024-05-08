using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Sepehr.Infrastructure.Shared.Services.ExcelUtility;
using Sepehr.Application.Interfaces;

namespace Sepehr.Infrastructure.Shared.Services
{
    public class ExcelUtility:IExportUtility
    {
        public System.Data.DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            System.Data.DataTable dataTable = new System.Data.DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        private List<PropertyInfo> GetSelectedProperties(PropertyInfo[] props, string include, string exclude)
        {
            List<PropertyInfo> propList = new List<PropertyInfo>();
            if (include != "") //Do include first
            {
                var includeProps = include.ToLower().Split(',').ToList();
                foreach (var item in props)
                {
                    var propName = includeProps.Where(a => a == item.Name.ToLower()).FirstOrDefault();
                    if (!string.IsNullOrEmpty(propName))
                        propList.Add(item);
                }
            }
            else if (exclude != "") //Then do exclude
            {
                var excludeProps = exclude.ToLower().Split(',');
                foreach (var item in props)
                {
                    var propName = excludeProps.Where(a => a == item.Name.ToLower()).FirstOrDefault();
                    if (string.IsNullOrEmpty(propName))
                        propList.Add(item);
                }
            }
            else //Default
            {
                propList.AddRange(props.ToList());
            }
            return propList;
        }

        public byte[] ExportExcel(System.Data.DataTable dataTable, string[,] columnsToTake, string heading = "", bool showSrNo = false)
        {
            if (dataTable.Rows.Count == 0)
            {
                dataTable.Rows.Add(new string[1]);
            }

            byte[] result = null;
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));

                    int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 4;
                    //foreach (DataColumn dr in dataTable.Columns) // search whole table
                    //{
                    //        dr.ColumnName = "cde"; //change the name
                    //}
                    //dataTable.Rows[0]["RowNum"] = "تست";
                    workSheet.View.RightToLeft = true;
                    if (showSrNo)
                    {
                        DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                        dataColumn.SetOrdinal(0);
                        int index = 1;
                        foreach (DataRow item in dataTable.Rows)
                        {
                            item[0] = index;
                            index++;
                        }
                    }

                    int i;
                    for (i = 0; i < columnsToTake.GetLength(0); i++)
                    {
                        dataTable.Columns[columnsToTake[i, 0]].SetOrdinal(i);
                    }

                    // removed ignored columns
                    bool isExist = false;
                    i = 0;
                    for (i = dataTable.Columns.Count - 1; i >= 0; i--)
                    {
                        if (i == 0 && showSrNo)
                        {
                            continue;
                        }

                        int j;
                        for (j = 0; j < columnsToTake.GetLength(0); j++)
                        {
                            if (columnsToTake[j, 0].Equals(dataTable.Columns[i].ColumnName))
                            {
                                isExist = true;
                                break;
                            }
                        }
                        if (isExist)
                        {
                            dataTable.Columns[i].ColumnName = columnsToTake[j, 1];
                            isExist = false;
                        }
                        else
                        {
                            dataTable.Columns.Remove(dataTable.Columns[i].ColumnName);
                            //workSheet.DeleteColumn(i + 1);
                        }
                        //if (!columnsToTake[i,0].Contains(dataTable.Columns[i].ColumnName))
                        //{
                        //    workSheet.DeleteColumn(i + 1);
                        //}
                        //else
                        //{
                        //    dataTable.Columns[i].ColumnName = columnsToTake[i,1];
                        //}
                    }

                    // add the content into the Excel file
                    workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);
                    // autofit width of cells with small content
                    int columnIndex = 1;
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                        int maxLength = columnCells.Max(cell => cell.Value == null ? 0 : cell.Value.ToString().Count());
                        if (maxLength < 150)
                        {
                            workSheet.Column(columnIndex).AutoFit();
                        }

                        columnIndex++;
                    }

                    // format header - bold, yellow on black
                    using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                    {
                        r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        r.Style.Font.Bold = true;
                        r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                    }

                    // format cells - add borders
                    using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                    {
                        r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                    }

                    if (!String.IsNullOrEmpty(heading))
                    {
                        workSheet.Cells["A1"].Value = heading;
                        workSheet.Cells["A1"].Style.Font.Size = 20;

                        workSheet.InsertColumn(1, 1);
                        workSheet.InsertRow(1, 1);
                        workSheet.Column(1).Width = 5;
                    }
                    workSheet.View.FreezePanes(startRowFrom + 1, 1);

                    result = package.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public byte[] ExportExcel<T>(List<T> data, string[,] columnsToTake, string Heading = "", bool showSlno = false)
        {
            PropertyInfo[] props = typeof(T).GetProperties();
            List<PropertyInfo> propList = GetSelectedProperties(props, "", "");
            object[,] listArray = new object[data.Count + 1, propList.Count];

            //Add property name to array as the first row
            int colIdx = 0;
            foreach (var prop in propList)
            {
                DisplayAttribute dispAttr = (DisplayAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayAttribute));
                listArray[0, colIdx] = (dispAttr == null || string.IsNullOrEmpty(dispAttr.Name)) ? prop.Name : dispAttr.Name;
                colIdx++;
            }

            return ExportExcel(ListToDataTable<T>(data), columnsToTake, Heading, showSlno);
        }

        //public byte[] ExportToExcel<T>(List<T> data, string[,] Cols)
        //{
        //    var data_table = ListToDataTable(data);
        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("Users");
        //        var currentRow = 1;
        //        int col_ind = 1;
        //        foreach (var col in Cols)
        //        {
        //            worksheet.Cell(currentRow, col_ind++).Value = col[1];
        //        }

        //        foreach (DataRow dt in data_table.Rows)
        //        {
        //            currentRow++;
        //            foreach (var col in Cols)
        //            {
        //                worksheet.Cell(currentRow, 1).Value = dt[col[0]];
        //            }
        //        }

        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //            var content = stream.ToArray();

        //            return content;
        //        }
        //    }
        //}
    }
}
