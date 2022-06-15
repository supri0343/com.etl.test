using System;
using System.Diagnostics.CodeAnalysis;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels
{
    public class DashboardLPGMachineUsage : IEquatable<DashboardLPGMachineUsage>
    {
        public DashboardLPGMachineUsage(){

        }
        public DashboardLPGMachineUsage(string id, int no, int week, DateTime date, string group, string shift, double? timeRequiredMin, double? timeRequiredHour, decimal? costPerKg, decimal? kgPerHour, decimal? totalCost) : this()
        {
            this.Id = id;
            this.No = no;
            this.Week = week;
            this.Date = date;
            this.Group = group;
            this.Shift = shift;
            this.TimeRequiredMin = timeRequiredMin;
            this.TimeRequiredHour = timeRequiredHour;
            this.CostPerKg = costPerKg;
            this.KgPerHour = kgPerHour;
            this.TotalCost = totalCost;

        }
        public string Id { get; set; }
        public int No { get; set; }
        public int Week { get; set; }
        public DateTime Date { get; set; }
        public string Group { get; set; }
        public string Shift { get; set; }
        public double? TimeRequiredMin { get; set; }
        public double? TimeRequiredHour { get; set; }
        public decimal? CostPerKg { get; set; }
        public decimal? KgPerHour { get; set; }
        public decimal? TotalCost { get; set; }

        public bool Equals([AllowNull] DashboardLPGMachineUsage other)
        {
            if (other is null)
                return false;

            return this.Id == other.Id;
        }

        public override bool Equals(object obj) => Equals(obj as DashboardLPGMachineUsage);
        public override int GetHashCode() => (Id).GetHashCode();
    }
}