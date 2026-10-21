using System.Text;
using ClosedXML.Excel;

namespace Application.Helpers;

public static class ExcelToCsvConverter
{
    public static string ConvertExcelToCsv(Stream stream)
    {
        var sb = new StringBuilder();

        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1); // première feuille uniquement

        foreach (var row in worksheet.RowsUsed())
        {
            var values = row.Cells().Select(cell =>
            {
                var value = cell.GetValue<string>() ?? "";
                if (value.Contains(',') || value.Contains('"'))
                    value = '"' + value.Replace("\"", "\"\"") + '"';
                return value;
            });

            sb.AppendLine(string.Join(",", values));
        }

        return sb.ToString();
    }
}