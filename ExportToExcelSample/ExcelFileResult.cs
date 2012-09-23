using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace EPPlusForMVC
{
    public class ExcelFileResult<T> : ActionResult
    {
        private readonly IEnumerable<T> _records;

        public ExcelFileResult(IEnumerable<T> records)
        {
            _records = records;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add(WorksheetName);

                if (ColumnDefinitions == null)
                {
                    ws.Cells["A1"].LoadFromCollection(_records, true, TableStyles.None);
                }
                else
                {
                    ws.Cells["A1"].LoadFromCollection(_records, true, TableStyles.None, BindingFlags.Default, ColumnDefinitions.Select(x => x.MemberInfo).ToArray());
                    Format(ws, ColumnDefinitions);
                }

                const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileContentResult = new FileContentResult(pck.GetAsByteArray(), contentType) { FileDownloadName = FileDownloadName };
                fileContentResult.ExecuteResult(context);
            }
        }

        private static void Format(ExcelWorksheet worksheet, IList<ExcelColumnDefinition> columnDefinitions)
        {
            worksheet.Row(1).Style.Font.Bold = true;
            for (var columnIndex = 1; columnIndex <= columnDefinitions.Count; columnIndex++)
            {
                var columnDefinitionIndex = columnIndex - 1;
                if (columnDefinitions[columnDefinitionIndex].Format != null)
                {
                    worksheet.Column(columnIndex).Style.Numberformat.Format = columnDefinitions[columnDefinitionIndex].Format;
                }
                if (columnDefinitions[columnDefinitionIndex].Header != null)
                {
                    worksheet.Cells[1, columnIndex].Value = columnDefinitions[columnDefinitionIndex].Header;
                }
                worksheet.Column(columnIndex).AutoFit();
            }
        }

        public ExcelColumnDefinition[] ColumnDefinitions { get; set; }

        private string _downloadName;
        public string FileDownloadName
        {
            get { return _downloadName ?? "ExcelFile"; }
            set { _downloadName = value; }
        }

        private string _worksheetName;
        public string WorksheetName
        {
            get { return _worksheetName ?? "Sheet1"; }
            set { _worksheetName = value; }
        }
    }
}