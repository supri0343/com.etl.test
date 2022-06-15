using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels
{
    public class PretreatmentMachineProductionModel
    {
        public PretreatmentMachineProductionModel(string id, int no, DateTime date, string shift, string group, string machine, string orderNo, string material, string colorCW, double? lengthIn, double? lengthOutBQ, double? lengthOutBS, double? widthFabric, string trainNo, string processType, double? speed, string description)
        {
            Id = id;
            No = no;
            Date = date;
            Shift = shift;
            Group = group;
            Machine = machine;
            OrderNo = orderNo;
            Material = material;
            ColorCW = colorCW;
            LengthIn = lengthIn;
            LengthOutBQ = lengthOutBQ;
            LengthOutBS = lengthOutBS;
            WidthFabric = widthFabric;
            TrainNo = trainNo;
            ProcessType = processType;
            Speed = speed;
            Description = description;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string Machine { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public string ColorCW { get; set; }
        public double? LengthIn { get; set; }
        public double? LengthOutBQ { get; set; }
        public double? LengthOutBS { get; set; }
        public double? WidthFabric { get; set; }
        public string TrainNo { get; set; }
        public string ProcessType { get; set; }
        public double? Speed { get; set; }
        public string Description { get; set; }
    }
}
