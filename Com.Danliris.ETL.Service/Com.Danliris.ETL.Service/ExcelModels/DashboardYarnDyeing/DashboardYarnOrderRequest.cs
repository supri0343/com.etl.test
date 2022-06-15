using System;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardYarnDyeing
{
    public class DashboardYarnOrderRequest
    {
        public DashboardYarnOrderRequest(){

        }
        public DashboardYarnOrderRequest(string id, int no, string sPNo, string color, string buyer, string noLabDip, string material, decimal? qty, string sales, DateTime? orderDate, DateTime? deliveryTarget, DateTime? dyeYarnRealization, DateTime? rewind, DateTime? finishedWarehouse, string description, string review):this()
        {
            this.Id = id;
            this.No = no;
            this.SPNo = sPNo;
            this.Color = color;
            this.Buyer = buyer;
            this.NoLabDip = NoLabDip;
            this.Material = material;
            this.Qty = qty;
            this.Sales = sales;
            this.OrderDate = orderDate;
            this.DeliveryTarget = deliveryTarget;
            this.DyeYarnRealization = dyeYarnRealization;
            this.Rewind = rewind;
            this.FinishedWarehouse = finishedWarehouse;
            this.Description = description;
            this.Review = review;

        }
        public string Id { get; set; }
        public int No { get; set; }
        public string SPNo { get; set; }
        public string Color { get; set; }
        public string Buyer { get; set; }
        public string NoLabDip { get; set; }
        public string Material { get; set; }
        public decimal? Qty { get; set; }
        public string Sales { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryTarget { get; set; }
        public DateTime? DyeYarnRealization { get; set; }
        public DateTime? Rewind { get; set; }
        public DateTime? FinishedWarehouse { get; set; }
        public string Description { get; set; }
        public string Review { get; set; }
    }
}