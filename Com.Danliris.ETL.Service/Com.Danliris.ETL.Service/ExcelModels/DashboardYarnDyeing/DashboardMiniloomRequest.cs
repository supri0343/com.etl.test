using System;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardYarnDyeing
{
    public class DashboardMiniloomRequest
    {
        public DashboardMiniloomRequest()
        {

        }

        public DashboardMiniloomRequest(string id, int no, string sPNo, string nameDesign, string buyer, string construction, string material, decimal? totalColor, string sales, DateTime? orderDate, DateTime? deliveryTarget, DateTime? labRealization, DateTime? deliveryWeaving, DateTime? finishMiniLoomLaboratoryDate, DateTime? deliverySales, string refMiniLoomNumber, decimal? qtyRealinKG, string description, string evaluationDownOrder, string review)
        {
            this.Id = id;
            this.No = no;
            this.SPNo = sPNo;
            this.NameDesign = nameDesign;
            this.Buyer = buyer;
            this.Construction = construction;
            this.Material = material;
            this.TotalColor = totalColor;
            this.Sales = sales;
            this.OrderDate = orderDate;
            this.DeliveryTarget = deliveryTarget;
            this.LabRealization = labRealization;
            this.DeliveryWeaving = deliveryWeaving;
            this.FinishMiniLoomLaboratoryDate = finishMiniLoomLaboratoryDate;
            this.DeliverySales = deliverySales;
            this.RefMiniLoomNumber = refMiniLoomNumber;
            this.QtyRealinKG = qtyRealinKG;
            this.Description = description;
            this.EvaluationDownOrder = evaluationDownOrder;
            this.Review = review;

        }
        public string Id { get; set; }
        public int No { get; set; }
        public string SPNo { get; set; }
        public string NameDesign { get; set; }
        public string Buyer { get; set; }
        public string Construction { get; set; }
        public string Material { get; set; }
        public decimal? TotalColor { get; set; }
        public string Sales { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryTarget { get; set; }
        public DateTime? LabRealization { get; set; }
        public DateTime? DeliveryWeaving { get; set; }
        public DateTime? FinishMiniLoomLaboratoryDate { get; set; }
        public DateTime? DeliverySales { get; set; }
        public string RefMiniLoomNumber { get; set; }
        public decimal? QtyRealinKG { get; set; }
        public string Description { get; set; }
        public string EvaluationDownOrder { get; set; }
        public string Review { get; set; }

    }
}