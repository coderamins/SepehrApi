
namespace Sepehr.Application.Interfaces
{
    public interface IExportUtility
    {
        byte[] ExportExcel(System.Data.DataTable dataTable, string[,] columnsToTake, string heading = "", bool showSrNo = false);
        byte[] ExportExcel<T>(List<T> data, string[,] columnsToTake, string Heading = "", bool showSlno = false);
    }
}
