using System;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardYarnDyeing
{
    public class DashboardFabricYDOrderRequest
    {
        public DashboardFabricYDOrderRequest(){

        }
        public DashboardFabricYDOrderRequest(string id, int no, string sPNo, string designName, string buyer, string sampleReferenceNumber, int? color, string construction, string material, decimal? qtyInMeter, string sales, DateTime? orderDate, DateTime? dateDel, DateTime? deliveryRealization, DateTime? bookingYarn, decimal? qtyInKg, decimal? qtySendWeavingTotal, decimal? qtyDeliverytoWeavinginKG, DateTime? preTreatment, DateTime? washing, DateTime? finishing, DateTime? qCIn, DateTime? sendWarehouse, decimal? qtyDeliverytoFinishedWarehouse, string description, string orderStatus, string review) : this()
        {
            this.Id = id;
            this.No = no;
            this.SPNo = sPNo;
            this.DesignName = designName;
            this.Buyer = buyer;
            this.SampleReferenceNumber = sampleReferenceNumber;
            this.Color = color;
            this.Construction = construction;
            this.Material = material;
            this.QtyInMeter = qtyInMeter;
            this.Sales = sales;
            this.OrderDate = orderDate;
            this.DateDel = dateDel;
            this.DeliveryRealization = deliveryRealization;
            this.BookingYarn = bookingYarn;
            this.QtyInKg = qtyInKg;
            this.QtySendWeavingTotal = qtySendWeavingTotal;
            this.QtyDeliverytoWeavinginKG = qtyDeliverytoWeavinginKG;
            this.PreTreatment = preTreatment;
            this.Washing = washing;
            this.Finishing = finishing;
            this.QCIn = qCIn;
            this.SendWarehouse = sendWarehouse;
            this.QtyDeliverytoFinishedWarehouse = qtyDeliverytoFinishedWarehouse;
            this.Description = description;
            this.OrderStatus = orderStatus;
            this.Review = review;

        }
        public string Id{get;set;}
        public int No{get;set;}
        public string SPNo{get;set;}
        public string DesignName{get;set;}
        public string Buyer{get;set;}
        public string SampleReferenceNumber{get;set;}
        public int? Color{get;set;}
        public string Construction {get;set;}
        public string Material {get;set;}
        public decimal? QtyInMeter{get;set;}
        public string Sales{get;set;}
        public DateTime? OrderDate{get;set;}
        public DateTime? DateDel{get;set;}
        public DateTime? DeliveryRealization{get;set;}
        public DateTime? BookingYarn{get;set;}
        public decimal? QtyInKg {get;set;}
        public decimal? QtySendWeavingTotal{get;set;}
        public decimal? QtyDeliverytoWeavinginKG{get;set;}
        public DateTime? PreTreatment{get;set;}
        public DateTime? Washing{get;set;}
        public DateTime? Finishing{get;set;}
        public DateTime? QCIn{get;set;}
        public DateTime? SendWarehouse{get;set;}
        public decimal? QtyDeliverytoFinishedWarehouse { get; set; }
        public string Description { get; set; }
        public string OrderStatus { get; set; }
        public string Review { get; set; }
    }
}