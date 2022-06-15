using System;
using System.Diagnostics.CodeAnalysis;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels
{
    public class DashboardCKDyeing : IEquatable<DashboardCKDyeing>
    {
        public DashboardCKDyeing(){

        }
        public DashboardCKDyeing(string id, int no, DateTime date, string machine, string orderNo, string construction, string color, string process, decimal? length, decimal? solutionVolume, string chemicalDyesDetail, double? lAB, double? prod) : this()
        {
            this.Id = id;
            this.No = no;
            this.Date = date;
            this.Machine = machine;
            this.OrderNo = orderNo;
            this.Construction = construction;
            this.Color = color;
            this.Process = process;
            this.Length = length;
            this.SolutionVolume = solutionVolume;
            this.ChemicalDyesDetail = chemicalDyesDetail;
            this.LAB = lAB;
            this.Prod = prod;

        }
        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Machine { get; set; }
        public string OrderNo { get; set; }
        public string Construction { get; set; }
        public string Color { get; set; }
        public string Process { get; set; }
        public decimal? Length { get; set; }
        public decimal? SolutionVolume { get; set; }
        public string ChemicalDyesDetail { get; set; }
        public double? LAB { get; set; }
        public double? Prod { get; set; }

        public bool Equals([AllowNull] DashboardCKDyeing other)
        {
            if (other is null)
                return false;

            return this.Id == other.Id;
        }

        public override bool Equals(object obj) => Equals(obj as DashboardCKDyeing);
        public override int GetHashCode() => (Id).GetHashCode();
    }
}