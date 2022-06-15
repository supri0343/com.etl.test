using System;
namespace Com.Danliris.ETL.Service.ExcelModels
{
    public class DashboardReprocessF
    {
        public string Id { get; set; }
        public int No { get; set; }
        public string SPPNo { get; set; }
        public DateTime QCOutDate { get; set; }
        public string TrainNumber { get; set; }
        public string Material { get; set; }
        public string Code { get; set; }
        public string Reprocess { get; set; }
        public decimal? QuantityOut { get; set; }
        public string Information { get; set; }
        public string AreaDestination { get; set; }

        public DashboardReprocessF() {}

        public DashboardReprocessF(int no, string sppNo, DateTime qCOutDate, string trainNumber, string material, string code,
            string reprocess, decimal? quantityOut, string information, string areaDestination)
        {
            Id = generateId(qCOutDate, no);
            No = no;
            SPPNo = sppNo;
            QCOutDate = qCOutDate;
            TrainNumber = trainNumber;
            Material = material;
            Code = code;
            Reprocess = reprocess;
            QuantityOut = quantityOut;
            Information = information;
            AreaDestination = areaDestination;
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
