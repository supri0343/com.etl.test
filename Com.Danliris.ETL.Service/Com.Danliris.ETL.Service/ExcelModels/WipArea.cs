using System;

namespace Com.Danliris.ETL.Service.ExcelModels
{
    public class WipArea
    {
        public WipArea(string iD, int? no, DateTime date, string sPNo, string material, string trainNo, double? qtyInMeter, double? bQ, double? bS, string activity, string destination)
        {
            ID = iD;
            No = no;
            Date = date;
            SPNo = sPNo;
            Material = material;
            TrainNo = trainNo;
            QtyInMeter = qtyInMeter;
            BQ = bQ;
            BS = bS;
            Activity = activity;
            Destination = destination;
        }

        public string ID { get; set; }
        public int? No { get; set; }
        public DateTime Date { get; set; }
        public string SPNo { get; set; }
        public string Material { get; set; }
        public string TrainNo { get; set; }
        public double? QtyInMeter { get; set; }
        public double? BQ { get; set; }
        public double? BS { get; set; }
        public string Activity { get; set; }
        public string Destination { get; set; }
    }
}