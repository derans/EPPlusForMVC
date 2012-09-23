using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using EPPlusForMVC;
using ExportToExcelSample.Core.Domain;
using ExportToExcelSample.Core.Infrastructure;

namespace ExportToExcelSample.Core.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetSampleInfoQuery _getSampleInfoQuery;

        public HomeController(IGetSampleInfoQuery getSampleInfoQuery)
        {
            _getSampleInfoQuery = getSampleInfoQuery;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SampleDownload1()
        {
            var records = _getSampleInfoQuery.Execute();
            var columns = new[]
                              {
                                  Column(x => x.College),
                                  Column(x => x.Amount, ExcelFormat.Money),
                                  Column(x => x.CreatedDate, ExcelFormat.Date, "Date"),
                                  Column(x => x.CreatedBy),
                                  Column(x => x.CreatedDate, ExcelFormat.Time, "Time"),
                                  Column(x => x.PercentageExample, ExcelFormat.Percent),
                              };

            return new ExcelFileResult<SampleInfo>(records) {ColumnDefinitions = columns};
        }

        private static ExcelColumnDefinition Column(Expression<Func<SampleInfo, object>> member, string format = null, string header = null)
        {
            return ExcelColumnDefinition.Create(member, format, header);
        }

        public ActionResult SampleDownload2()
        {
            var records = _getSampleInfoQuery.Execute();
            return new ExcelFileResult<SampleInfo>(records) { FileDownloadName = "Specific_file_name_" + DateTime.Now.ToFileTime()};
        }

        public ActionResult SampleDownload3()
        {
            var records = _getSampleInfoQuery.Execute();
            return new ExcelFileResult<SampleInfo>(records);
        }

        public ActionResult SampleDownload4()
        {
            var records = _getSampleInfoQuery.Execute();
            var columns = new[]
                              {
                                  Column(x => x.College),
                                  Column(x => x.Amount),
                                  Column(x => x.CreatedDate),
                              };

            return new ExcelFileResult<SampleInfo>(records) { ColumnDefinitions = columns };
        }

        public ActionResult SampleDownload5()
        {
            var records = _getSampleInfoQuery.Execute();
            var columns = new[]
                              {
                                  Column(x => x.College, header: "College Name"),
                                  Column(x => x.Amount, ExcelFormat.Money, "Amount Owed"),
                                  Column(x => x.CreatedDate, ExcelFormat.DateTime, "Date Created"),
                              };

            return new ExcelFileResult<SampleInfo>(records) { ColumnDefinitions = columns };
        }
    }
}