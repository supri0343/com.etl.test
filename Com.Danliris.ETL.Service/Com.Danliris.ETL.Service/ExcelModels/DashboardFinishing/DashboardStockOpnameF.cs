using System;
namespace Com.Danliris.ETL.Service.ExcelModels
{
    public class DashboardStockOpnameF
    {
        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string SPNo { get; set; }
        public string Material { get; set; }
        public string TrainNo { get; set; }
        public decimal? Length { get; set; }
        public string Description { get; set; }

        public DashboardStockOpnameF(){}

        public DashboardStockOpnameF(int no, DateTime date, string spNo, string material, string trainNo, decimal? length,
            string description)
        {
            Id = generateId(date, no);
            No = no;
            Date = date;
            SPNo = spNo;
            Material = material;
            TrainNo = trainNo;
            Length = length;
            Description = description;
        }

        private string generateId(DateTime date, int no)
        {
            string year = date.Year + "";
            var month = date.Month;
            var number = no;

            return $"{year}{month.ToString("00")}{number.ToString("0000")}";
        }
    }
}
