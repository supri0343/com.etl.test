using System;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardKoranLaborat
{
    public class DashboardPrintingLab
    {
        public DashboardPrintingLab(){

        }
        public DashboardPrintingLab(string id, int no, DateTime date,int sONO, string codeRCFC, string newReSO, string designCode, string material, string buyer, string sales, DateTime? sentDate, decimal? cW, decimal? sPP1, decimal? sPP2, decimal? sPP3, decimal? sPP4, decimal? sPP5, decimal? sPP6, decimal? sPP7, decimal? sPP8, decimal? sPP9, decimal? sPP10, decimal? sPP11, decimal? sPP12, decimal? sPPTotal, string status, string downOrder, DateTime? accDate, string feedbackSales, string salesDescription, string labDescriptionLatePotential):this()
        {
            this.Id = id;
            this.No = no;
            this.Date=date;
            this.SONO = sONO;
            this.CodeRCFC = codeRCFC;
            this.NewReSO = newReSO;
            this.DesignCode = designCode;
            this.Material = material;
            this.Buyer = buyer;
            this.Sales = sales;
            this.SentDate = sentDate;
            this.CW = cW;
            this.SPP1 = sPP1;
            this.SPP2 = sPP2;
            this.SPP3 = sPP3;
            this.SPP4 = sPP4;
            this.SPP5 = sPP5;
            this.SPP6 = sPP6;
            this.SPP7 = sPP7;
            this.SPP8 = sPP8;
            this.SPP9 = sPP9;
            this.SPP10 = sPP10;
            this.SPP11 = sPP11;
            this.SPP12 = sPP12;
            this.SPPTotal = sPPTotal;
            this.Status = status;
            this.DownOrder = downOrder;
            this.AccDate = accDate;
            this.FeedbackSales = feedbackSales;
            this.SalesDescription = salesDescription;
            this.LabDescriptionLatePotential = labDescriptionLatePotential;

        }
        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public int SONO { get; set; }
        public string CodeRCFC { get; set; }
        public string NewReSO { get; set; }
        public string DesignCode { get; set; }
        public string Material { get; set; }
        public string Buyer { get; set; }
        public string Sales { get; set; }
        public DateTime? SentDate { get; set; }
        public decimal? CW { get; set; }
        public decimal? SPP1 { get; set; }
        public decimal? SPP2 { get; set; }
        public decimal? SPP3 { get; set; }
        public decimal? SPP4 { get; set; }
        public decimal? SPP5 { get; set; }
        public decimal? SPP6 { get; set; }
        public decimal? SPP7 { get; set; }
        public decimal? SPP8 { get; set; }
        public decimal? SPP9 { get; set; }
        public decimal? SPP10 { get; set; }
        public decimal? SPP11 { get; set; }
        public decimal? SPP12 { get; set; }
        public decimal? SPPTotal { get; set; }
        public string Status { get; set; }
        public string DownOrder { get; set; }
        public DateTime? AccDate { get; set; }
        public string FeedbackSales { get; set; }
        public string SalesDescription { get; set; }
        public string LabDescriptionLatePotential { get; set; }
    }
}