using System;

namespace Com.Danliris.ETL.Service.ExcelModels
{
    public class SOOrderDigitalTransfer
    {
        public SOOrderDigitalTransfer(string iD, int? no, DateTime date, decimal? sONo,string patternCode, string processType, string serianTotal, string designType, string designSize, string buyer, string material, string designFormat, string rUN, DateTime deliveryRequestDate, DateTime? accDate, double? leadTime, string status, string description)
        {
            ID = iD;
            No = no;
            Date = date;
            SONo = sONo;
            PatternCode = patternCode;
            ProcessType = processType;
            SerianTotal = serianTotal;
            DesignType = designType;
            DesignSize = designSize;
            Buyer = buyer;
            Material = material;
            DesignFormat = designFormat;
            RUN = rUN;
            DeliveryRequestDate = deliveryRequestDate;
            AccDate = accDate;
            LeadTime = leadTime;
            Status = status;
            Description = description;
        }
        public string ID { get; set; }
        public int? No { get; set; }
        public DateTime Date { get; set; }
        public decimal? SONo { get; set; }
        public string PatternCode { get; set; }
        public string ProcessType { get; set; }
        public string SerianTotal { get; set; }
        public string DesignType { get; set; }
        public string DesignSize { get; set; }
        public string Buyer { get; set; }
        public string Material { get; set; }
        public string DesignFormat { get; set; }
        public string RUN { get; set; }
        public DateTime DeliveryRequestDate { get; set; }
        public DateTime? AccDate { get; set; }
        public double? LeadTime { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}