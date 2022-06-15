using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPrintingModels
{
    public class DashBoardSupportMachineModel
    {
        public DashBoardSupportMachineModel(string id, int no, DateTime date, string machine, string shift, string group, string orderNo, string material, string patternCode, string trainNo, double? qtyIn, double? bQ, double? bS, double? speed)
        {
            Id = id;
            No = no;
            Date = date;
            Machine = machine;
            Shift = shift;
            Group = group;
            OrderNo = orderNo;
            Material = material;
            PatternCode = patternCode;
            TrainNo = trainNo;
            QtyIn = qtyIn;
            BQ = bQ;
            BS = bS;
            Speed = speed;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Machine { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public string PatternCode { get; set; }
        public string TrainNo { get; set; }
        public double? QtyIn { get; set; }
        public double? BQ { get; set; }
        public double? BS{ get; set; }
        public double? Speed { get; set; }
        
    }
}
