using System;

namespace Com.Danliris.ETL.Service.ExcelModels.KoranQCLine
{
    public class DigitalPrint
    {
        public DigitalPrint(string iD, int? no, DateTime date, string oPerator, char? groupIM, string shiftIM, string orderNo, string material, string patternColor, string buyer, string trainNo, double? total, double? gradeA, double? gradeB, double? gradeC, double? gradeBS, string description1, string description2, string description3)
        {
            ID = iD;
            No = no;
            Date = date;
            Operator = oPerator;
            GroupIM = groupIM;
            ShiftIM = shiftIM;
            OrderNo = orderNo;
            Material = material;
            PatternColor = patternColor;
            Buyer = buyer;
            TrainNo = trainNo;
            Total = total;
            GradeA = gradeA;
            GradeB = gradeB;
            GradeC = gradeC;
            GradeBS = gradeBS;
            Description1 = description1;
            Description2 = description2;
            Description3 = description3;
        }
        public string ID { get; set; }
        public int? No { get; set; }
        public DateTime Date { get; set; }
        public string Operator { get; set; }
        public char? GroupIM { get; set; }
        public string ShiftIM { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public string PatternColor { get; set; }
        public string Buyer { get; set; }
        public string TrainNo { get; set; }
        public double? Total { get; set; }
        public double? GradeA { get; set; }
        public double? GradeB { get; set; }
        public double? GradeC { get; set; }
        public double? GradeBS { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
    }
}