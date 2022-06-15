using System;

namespace Com.Danliris.ETL.Service.ExcelModels
{
    public class DailyDelivery
    {
        public DailyDelivery(string iD, int? no, DateTime date, string sPNo, string patternCode, string material, string processType, double? a, double? b, double? c, double? bS, double? aval, double? total, string descriptionBS, double? qtyBS)
        {
            ID = iD;
            No = no;
            Date = date;
            SPNo = sPNo;
            PatternCode = patternCode;
            Material = material;
            ProcessType = processType;
            A = a;
            B = b;
            C = c;
            BS = bS;
            Aval = aval;
            Total = total;
            DescriptionBS = descriptionBS;
            QtyBS = qtyBS;
        }
        public string ID { get; set; }
        public int? No { get; set; }
        public DateTime Date { get; set; }
        public string SPNo { get; set; }
        public string PatternCode { get; set; }
        public string Material { get; set; }
        public string ProcessType { get; set; }
        public double? A { get; set; }
        public double? B { get; set; }
        public double? C { get; set; }
        public double? BS { get; set; }
        public double? Aval { get; set; }
        public double? Total { get; set; }
        public string DescriptionBS { get; set; }
        public double? QtyBS { get; set; }
    }
}