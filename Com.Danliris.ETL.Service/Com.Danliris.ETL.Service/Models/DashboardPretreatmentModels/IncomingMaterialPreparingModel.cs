using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels
{
    public class IncomingMaterialPreparingModel
    {
        public IncomingMaterialPreparingModel(string id, int no, string activity, string dPRFP, DateTime date, string shift, string group, string sPNo, string construction, string color, string trainNo, double? piecesNo, string grade, double? meter, DateTime? sealDate, DateTime? sewingDate, DateTime? rollDate, string time, DateTime? pretreatmentOutDate, DateTime? pretreatmentOutTotal)
        {
            Id = id;
            No = no;
            Activity = activity;
            DPRFP = dPRFP;
            Date = date;
            Shift = shift;
            Group = group;
            SPNo = sPNo;
            Construction = construction;
            Color = color;
            TrainNo = trainNo;
            PiecesNo = piecesNo;
            Grade = grade;
            Meter = meter;
            SealDate = sealDate;
            SewingDate = sewingDate;
            RollDate = rollDate;
            Time = time;
            PretreatmentOutDate = pretreatmentOutDate;
            PretreatmentOutTotal = pretreatmentOutTotal;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public string Activity { get; set; }
        public string DPRFP { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string SPNo { get; set; }
        public string Construction { get; set; }
        public string Color { get; set; }
        public string TrainNo { get; set; }
        public double? PiecesNo { get; set; }
        public string Grade { get; set; }
        public double? Meter { get; set; }
        public DateTime? SealDate { get; set; }
        public DateTime? SewingDate { get; set; }
        public DateTime? RollDate { get; set; }
        public string Time { get; set; }
        public DateTime? PretreatmentOutDate { get; set; }
        public DateTime? PretreatmentOutTotal { get; set; }
    }
}
