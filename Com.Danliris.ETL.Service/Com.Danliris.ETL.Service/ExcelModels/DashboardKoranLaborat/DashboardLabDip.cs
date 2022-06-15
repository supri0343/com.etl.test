using System;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardKoranLaborat
{
    public class DashboardLabDip
    {
        public DashboardLabDip(){

        }
        public DashboardLabDip(string id, int no, DateTime date, string team, int? labDipNo, string buyer, string colorLevel, string color, string colorDescription, string yarnFabric, string fabricType, string processType, decimal? processingTime, decimal? matchingTotalDisperse, decimal? matchingTotalReactive2, DateTime? deliveryPlan, DateTime? deliveryRealization, decimal? leadTime, string description, string buyerAcc):this()
        {
            this.Id = id;
            this.No = no;
            this.Date = date;
            this.Team = team;
            this.LabDipNo = labDipNo;
            this.Buyer = buyer;
            this.ColorLevel = colorLevel;
            this.Color = color;
            this.ColorDescription = colorDescription;
            this.YarnFabric = yarnFabric;
            this.FabricType = fabricType;
            this.ProcessType = processType;
            this.ProcessingTime = processingTime;
            this.MatchingTotalDisperse = matchingTotalDisperse;
            this.MatchingTotalReactive2 = matchingTotalReactive2;
            this.DeliveryPlan = deliveryPlan;
            this.DeliveryRealization = deliveryRealization;
            this.LeadTime = leadTime;
            this.Description = description;
            this.BuyerAcc = buyerAcc;

        }
        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Team { get; set; }
        public int? LabDipNo { get; set; }
        public string Buyer { get; set; }
        public string ColorLevel { get; set; }
        public string Color { get; set; }
        public string ColorDescription { get; set; }
        public string YarnFabric { get; set; }
        public string FabricType { get; set; }
        public string ProcessType { get; set; }
        public decimal? ProcessingTime { get; set; }
        public decimal? MatchingTotalDisperse { get; set; }
        public decimal? MatchingTotalReactive2 { get; set; }
        public DateTime? DeliveryPlan { get; set; }
        public DateTime? DeliveryRealization { get; set; }
        public decimal? LeadTime { get; set; }
        public string Description { get; set; }
        public string BuyerAcc { get; set; }
    }
}