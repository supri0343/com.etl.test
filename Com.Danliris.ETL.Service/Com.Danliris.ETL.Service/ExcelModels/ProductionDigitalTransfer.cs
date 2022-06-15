using System;

namespace Com.Danliris.ETL.Service.ExcelModels
{
    public class ProductionDigitalTransfer
    {
        public ProductionDigitalTransfer(string iD, int no, DateTime date, char shift, string group, string sPNo, string processType, string material, string colorCW, string patternCode, double? qtyInMeter, DateTime? start, DateTime? finish, decimal? leadTime, int? a, int? b, int? c, int? bS, string? descriptionBS)
        {
            this.ID = iD;
            this.no = no;
            this.Date = date;
            this.Shift = shift;
            this.Group = group;
            this.SPNo = sPNo;
            this.ProcessType = processType;
            this.Material = material;
            this.ColorCW = colorCW;
            this.PatternCode = patternCode;
            this.QtyInMeter = qtyInMeter;
            this.Start = start;
            this.Finish = finish;
            this.LeadTime = leadTime;
            this.A = a;
            this.B = B;
            this.C = c;
            this.BS = bS;
            this.DescriptionBS = descriptionBS;

        }
        public string ID { get; set; }
        public int no { get; set; }
        public DateTime Date { get; set; }
        public char Shift { get; set; }
        public string Group { get; set; }
        public string SPNo { get; set; }
        public string ProcessType { get; set; }
        public string Material { get; set; }
        public string ColorCW { get; set; }
        public string PatternCode { get; set; }
        public double? QtyInMeter { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Finish { get; set; }
        public decimal? LeadTime { get; set; }
        public int? A { get; set; }
        public int? B { get; set; }
        public int? C { get; set; }
        public int? BS { get; set; }
        public string? DescriptionBS { get; set; }

    }
}