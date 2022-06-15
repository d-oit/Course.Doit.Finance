using System.Collections.Generic;

namespace BlazorHero.CleanArchitecture.Application.Features.Dashboards.Queries.GetData
{
    public class DashboardDataResponse
    {
        public decimal CurrentAmount { get; set; }
        public int FinanceAccountCount { get; set; }
        public long InvestmentCount { get; set; }
        public int DocumentCount { get; set; }
        public int DocumentTypeCount { get; set; }
        public int DocumentExtendedAttributeCount { get; set; }
        public int UserCount { get; set; }
        public int RoleCount { get; set; }
        public List<ChartSeries> DataEnterBarChart { get; set; } = new();
        public Dictionary<string, double> ProductByBrandTypePieChart { get; set; }
    }

    public class ChartSeries
    {
        public ChartSeries() { }

        public string Name { get; set; }
        public double[] Data { get; set; }
    }

}