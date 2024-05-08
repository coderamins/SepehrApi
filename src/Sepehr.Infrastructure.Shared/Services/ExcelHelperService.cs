using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Sepehr.Domain.Entities;
using System.Threading;

namespace Sepehr.Infrastructure.Shared.Services
{
    public static class ExcelHelperService
    {
        public static List<T> Import<T>(IFormFile formFile) where T : new()
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return DemoResponse<List<T>>.GetResult(-1, "formfile is empty");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return DemoResponse<List<UserInfo>>.GetResult(-1, "Not Support file extension");
            }

            var list = new List<UserInfo>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, CancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        list.Add(new ProductPrice
                        {
                            UserName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            Age = int.Parse(worksheet.Cells[row, 2].Value.ToString().Trim()),
                        });
                    }
                }
            }

            return listResult;
        }
    }
}
