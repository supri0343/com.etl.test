using System;
using System.Diagnostics.CodeAnalysis;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels
{
    public class DashboardDyeingMachine : IEquatable<DashboardDyeingMachine>
    {
        public DashboardDyeingMachine()
        {

        }

        public DashboardDyeingMachine(string id, int no, string machine, DateTime date, string shift, string group, string orderNo, string material, string color, string trainNo, string processType, string processDescription, decimal? speed, decimal? qtyInMeter, decimal? qtyOutMeter) : this()
        {
            this.Id = id;
            this.No = no;
            this.Machine = machine;
            this.Date = date;
            this.Shift = shift;
            this.Group = group;
            this.OrderNo = orderNo;
            this.Material = material;
            this.Color = color;
            this.TrainNo = trainNo;
            this.ProcessType = processType;
            this.ProcessDescription = processDescription;
            this.Speed = speed;
            this.QtyInMeter = qtyInMeter;
            this.QtyOutMeter = qtyOutMeter;

        }
        
        public string Id { get; set; }
        public int No { get; set; }
        public string Machine { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public string TrainNo { get; set; }
        public string ProcessType { get; set; }
        public string ProcessDescription { get; set; }
        public decimal? Speed { get; set; }
        public decimal? QtyInMeter { get; set; }
        public decimal? QtyOutMeter { get; set; }

        public bool Equals([AllowNull] DashboardDyeingMachine other)
        {
            if (other is null)
                return false;

            return this.Id == other.Id;
        }
        public override bool Equals(object obj) => Equals(obj as DashboardDyeingMachine);
        public override int GetHashCode() => (Id).GetHashCode();
    }
}