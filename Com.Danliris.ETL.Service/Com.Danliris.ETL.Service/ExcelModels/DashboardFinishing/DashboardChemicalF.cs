using System;
namespace Com.Danliris.ETL.Service.ExcelModels
{
    public class DashboardChemicalF
    {
        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Machine { get; set; }
        public string OrderType { get; set; }
        public string OrderNo { get; set; }
        public string Construction { get; set; }
        public string Color { get; set; }
        public string ProcessType { get; set; }
        public decimal? Length { get; set; }
        public decimal? SolutionVolTotal { get; set; }
        public string ChemicalTypeUsed { get; set; }
        public decimal? ChemicalWeightUsed { get; set; }

        public DashboardChemicalF(int no, DateTime date, string machine, string orderType, string orderNo,
            string construction, string color, string processType, decimal? length, decimal? solutionVolTotal, string chemicalTypeUsed,
            decimal? chemicalWeightUsed)
        {
            Id = generateId(date, no);
            No = no;
            Machine = machine;
            Date = date;
            OrderType = orderType;
            OrderNo = orderNo;
            Construction = construction;
            Color = color;
            ProcessType = processType;
            Length = length;
            SolutionVolTotal = solutionVolTotal;
            ChemicalTypeUsed = chemicalTypeUsed;
            ChemicalWeightUsed = chemicalWeightUsed;
        }

        public DashboardChemicalF() {}

        private string generateId(DateTime date, int no)
        {
            string year = date.Year + "";
            var month = date.Month;
            var number = no;

            return $"{year}{month.ToString("00")}{number.ToString("0000")}";
        }
    }
}
