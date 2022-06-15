using System;

namespace Com.Danliris.ETL.Service.ExcelModels
{
    public class OrderIn
    {
        public OrderIn(string iD, int? no, DateTime date, string sPNo, string design, string processType, double? qtyInMeter, string material, string yarnNo, string width, string patternCode, string reference, string series, string buyer, string materialSource, DateTime orderCreateDate, DateTime orderEntryProductionDate, DateTime deliveryRequestDate, DateTime? productionDate, DateTime? deliveryRealizationDate, string status, double? leadTime)
        {
            ID = iD;
            No = no;
            Date = date;
            SPNo = sPNo;
            Design = design;
            ProcessType = processType;
            QtyInMeter = qtyInMeter;
            Material = material;
            YarnNo = yarnNo;
            Width = width;
            PatternCode = patternCode;
            Reference = reference;
            Series = series;
            Buyer = buyer;
            MaterialSource = materialSource;
            OrderEntryProductionDate = orderEntryProductionDate;
            DeliveryRequestDate = deliveryRequestDate;
            OrderCreateDate = orderCreateDate;
            ProductionDate = productionDate;
            DeliveryRealizationDate = deliveryRealizationDate;
            Status = status;
            LeadTime = leadTime;
        }
        public string ID { get; set; }
        public int? No { get; set; }
        public DateTime Date { get; set; }
        public string SPNo { get; set; }
        public string Design { get; set; }
        public string ProcessType { get; set; }
        public double? QtyInMeter { get; set; }
        public string Material { get; set; }
        public string YarnNo { get; set; }
        public string Width { get; set; }
        public string PatternCode { get; set; }
        public string Reference { get; set; }
        public string Series { get; set; }
        public string Buyer { get; set; }
        public string MaterialSource { get; set; }
        public DateTime OrderEntryProductionDate { get; set; }
        public DateTime DeliveryRequestDate { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? DeliveryRealizationDate { get; set; }
        public string Status { get; set; }
        public double? LeadTime { get; set; }
    }
}