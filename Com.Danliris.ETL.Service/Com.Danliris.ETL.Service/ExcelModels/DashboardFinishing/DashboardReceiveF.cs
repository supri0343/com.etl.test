using System;
namespace Com.Danliris.ETL.Service.ExcelModels
{
    public class DashboardReceiveF
    {
        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string SPNo { get; set; }
        public string Construction { get; set; }
        public string TrainNo { get; set; }
        public decimal? Quantity { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }

        public DashboardReceiveF(int no, DateTime date, string spNo, string construction, string trainNo, decimal? quantity,
            string grade, string description, string source)
        {
            Id = generateId(date, no);
            No = no;
            Date = date;
            SPNo = spNo;
            Construction = construction;
            TrainNo = trainNo;
            Quantity = quantity;
            Grade = grade;
            Description = description;
            Source = source;
        }

        public DashboardReceiveF()
        {
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
