using System;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardYarnDyeing
{
    public class DashboardDyeYarnRequest
    {
        public DashboardDyeYarnRequest()
        {

        }

        public DashboardDyeYarnRequest(string id, int no, string sPNo, string color, string referenceNo, string material, string sales, string buyer, DateTime? orderDate, DateTime? deliveryTarget, DateTime? labRealization, string yarnColorCode, DateTime? deliveryRealDate, string description, string evaluationDownOrder, string review)
        {
            this.Id = id;
            this.No = no;
            this.SPNo = sPNo;
            this.Color = color;
            this.ReferenceNo = referenceNo;
            this.Material = material;
            this.Sales = sales;
            this.Buyer = buyer;
            this.OrderDate = orderDate;
            this.DeliveryTarget = deliveryTarget;
            this.LabRealization = labRealization;
            this.YarnColorCode = yarnColorCode;
            this.DeliveryRealDate = deliveryRealDate;
            this.Description = description;
            this.EvaluationDownOrder = evaluationDownOrder;
            this.Review = review;

        }
        public string Id { get; set; }
        public int No { get; set; }
        public string SPNo { get; set; }
        public string Color { get; set; }
        public string ReferenceNo { get; set; }
        public string Material { get; set; }
        public string Sales { get; set; }
        public string Buyer { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryTarget { get; set; }
        public DateTime? LabRealization { get; set; }
        public string YarnColorCode { get; set; }
        public DateTime? DeliveryRealDate { get; set; }
        public string Description { get; set; }
        public string EvaluationDownOrder { get; set; }
        public string Review { get; set; }
    }
}