using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPrintingModels
{
    public class DashBoardMainMachineModel
    {
        public DashBoardMainMachineModel(string id, int no, DateTime date, string machine, string shift, string group, string orderNo, string material, string colorCW, string trainNo, string processType, string patternCode, string designCode, string screen, double? speed, double? qtyInMeter, double? qtyOutBQMeter, double? qtyOutBSMeter, double? widthInch)
        {
            Id = id;
            No = no;
            Date = date;
            Machine = machine;
            Shift = shift;
            Group = group;
            OrderNo = orderNo;
            Material = material;
            ColorCW = colorCW;
            TrainNo = trainNo;
            ProcessType = processType;
            PatternCode = patternCode;
            DesignCode = designCode;
            Screen = screen;
            Speed = speed;
            QtyInMeter = qtyInMeter;
            QtyOutBQMeter = qtyOutBQMeter;
            QtyOutBSMeter = qtyOutBSMeter;
            WidthInch = widthInch;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Machine { get; set; }
        public string Shift { get; set; }
        public string  Group { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public string ColorCW { get; set; }
        public string TrainNo { get; set; }
        public string ProcessType { get; set; }
        public string PatternCode { get; set; }
        public string DesignCode { get; set; }
        public string Screen { get; set; }
        public double? Speed { get; set; }
        public double? QtyInMeter { get; set; }
        public double? QtyOutBQMeter { get; set; }
        public double? QtyOutBSMeter { get; set; }
        public double? WidthInch { get; set; }        
    }
}
