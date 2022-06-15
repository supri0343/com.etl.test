using System;
using System.Diagnostics.CodeAnalysis;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels
{
    public class DashboardHandOverArea : IEquatable<DashboardHandOverArea>
    {
        public DashboardHandOverArea(){

        }
        public DashboardHandOverArea(string id, int? no, DateTime? date, string orderNo, string construction, decimal? qty, string trainNo, string grade, string activity, string productionSubcon, string destination, string reprocess, string description, string area, string color, string descriptionSubcon, decimal? a, decimal? b, decimal? c, decimal? bS, decimal? total, decimal? fabricWidth) : this()
        {
            this.Id = id;
            this.No = no;
            this.Date = date;
            this.OrderNo = orderNo;
            this.Construction = construction;
            this.Qty = qty;
            this.TrainNo = trainNo;
            this.Grade = grade;
            this.Activity = activity;
            this.ProductionSubcon = productionSubcon;
            this.Destination = destination;
            this.Reprocess = reprocess;
            this.Description = description;
            this.Area = area;
            this.Color = color;
            this.DescriptionSubcon = descriptionSubcon;
            this.A = a;
            this.B = b;
            this.C = c;
            this.BS = bS;
            this.Total = total;
            this.FabricWidth = fabricWidth;

        }
        public string Id { get; set; }
        public int? No { get; set; }
        public DateTime? Date { get; set; }
        public string OrderNo { get; set; }
        public string Construction { get; set; }
        public decimal? Qty { get; set; }
        public string TrainNo { get; set; }
        public string Grade { get; set; }
        public string Activity { get; set; }
        public string ProductionSubcon { get; set; }
        public string Destination { get; set; }
        public string Reprocess { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
        public string Color { get; set; }
        public string DescriptionSubcon { get; set; }
        public decimal? A { get; set; }
        public decimal? B { get; set; }
        public decimal? C { get; set; }
        public decimal? BS { get; set; }
        public decimal? Total { get; set; }
        public decimal? FabricWidth { get; set; }        

        public bool Equals([AllowNull] DashboardHandOverArea other)
        {
            if (other is null)
                return false;

            return this.Id == other.Id;
        }
        public override bool Equals(object obj) => Equals(obj as DashboardHandOverArea);
        public override int GetHashCode() => (Id).GetHashCode();

    }
}