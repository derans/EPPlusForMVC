using System;
using System.Collections.Generic;
using ExportToExcelSample.Core.Domain;

namespace ExportToExcelSample.Core.Infrastructure
{
    public interface IGetSampleInfoQuery : IParameterLessQuery<IList<SampleInfo>>{}

    public class GetSampleInfoQuery : IGetSampleInfoQuery
    {
        public IList<SampleInfo> Execute()
        {
            return new List<SampleInfo>
                           {
                               new SampleInfo {Amount = 1, College = "Test", CreatedBy = "Data", CreatedDate = DateTime.Now, PercentageExample = .14},
                               new SampleInfo {Amount = 2, College = "Test", CreatedBy = "Data", CreatedDate = DateTime.Now, PercentageExample = .15},
                               new SampleInfo {Amount = 3, College = "Test", CreatedBy = "Data", CreatedDate = DateTime.Now, PercentageExample = .16},
                               new SampleInfo {Amount = 4, College = "Test", CreatedBy = "Data", CreatedDate = DateTime.Now, PercentageExample = .17},
                               new SampleInfo {Amount = 5, College = "Test", CreatedBy = "Data", CreatedDate = DateTime.Now, PercentageExample = .18},
                           };
        }
    }
}