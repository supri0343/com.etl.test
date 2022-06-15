using System;
using System.Diagnostics.CodeAnalysis;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels
{
    public class DashboardWIPMaterial : IEquatable<DashboardWIPMaterial>
    {
        public DashboardWIPMaterial(){

        }
        public DashboardWIPMaterial(string id, int no, DateTime date, string machine, string sPNo, string material, string trainNo, string color, decimal? length, string descriptionProcess) : this()
        {
            this.Id = id;
            this.No = no;
            this.Date = date;
            this.Machine = machine;
            this.SPNo = sPNo;
            this.Material = material;
            this.TrainNo = trainNo;
            this.Color = color;
            this.Length = length;
            this.DescriptionProcess = descriptionProcess;

        }
        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Machine { get; set; }
        public string SPNo { get; set; }
        public string Material { get; set; }
        public string TrainNo { get; set; }
        public string Color { get; set; }
        public decimal? Length { get; set; }
        public string DescriptionProcess { get; set; }

        public bool Equals([AllowNull] DashboardWIPMaterial other)
        {
            if (other is null)
                return false;

            return this.Id == other.Id;
        }

        public override bool Equals(object obj) => Equals(obj as DashboardWIPMaterial);
        public override int GetHashCode() => (Id).GetHashCode();
    }
}