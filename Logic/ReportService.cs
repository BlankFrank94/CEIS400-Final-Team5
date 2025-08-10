using CEIS400_Final_Team5.Data;

namespace CEIS400_Final_Team5.Logic
{
    public class ReportService
    {
        private readonly DataManager _data;
        public ReportService(DataManager data) => _data = data;

        public Report GenerateUsageReport()
        {
            // TODO: summarize transactions/stock into a string/CSV
            return new Report { Body = "Placeholder report." };
        }
    }
}
