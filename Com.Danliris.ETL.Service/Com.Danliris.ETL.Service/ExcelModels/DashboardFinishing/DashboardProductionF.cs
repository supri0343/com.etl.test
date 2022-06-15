using System;
namespace Com.Danliris.ETL.Service.ExcelModels
{
    public class DashboardProductionF
    {
        public string Id { get; set; }
        public int No { get; set; }
        public string Machine { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public string TrainNo { get; set; }
        public string ProcessType { get; set; }
        public string ProcessDescription { get; set; }
        public decimal? LengthIn { get; set; }
        public decimal? LengthOut { get; set; }
        public string OperationStart { get; set; }
        public string OperationStop { get; set; }
        public string Duration { get; set; }

        public DashboardProductionF(int no, string machine, DateTime date, string shift, string orderNo,
            string material, string trainNo, string processType, string processDescription, decimal? lengthIn, decimal? lengthOut,
            string operationStart, string operationStop, string duration)
        {
            Id = generateId(date, no);
            No = no;
            Machine = machine;
            Date = date;
            Shift = shift;
            OrderNo = orderNo;
            Material = material;
            TrainNo = trainNo;
            ProcessType = processType;
            ProcessDescription = processDescription;
            LengthIn = lengthIn;
            LengthOut = lengthOut;
            OperationStart = operationStart;
            OperationStop = operationStop;
            Duration = duration;
        }

        public DashboardProductionF() { }

        private string generateId(DateTime date, int no)
        {
            string year = date.Year + "";
            var month = date.Month;
            var number = no;

            return $"{year}{month.ToString("00")}{number.ToString("0000")}";
        }
    }
}
