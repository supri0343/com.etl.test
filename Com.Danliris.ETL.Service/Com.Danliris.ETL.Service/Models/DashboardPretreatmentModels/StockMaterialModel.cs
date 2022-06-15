using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels
{
    public class StockMaterialModel
    {
        public StockMaterialModel(string id, int no, DateTime dateInOut, string activity, string orderNo, string material, string train, double? quantity)
        {
            Id = id;
            No = no;
            DateInOut = dateInOut;
            Activity = activity;
            OrderNo = orderNo;
            Material = material;
            Train = train;
            Quantity = quantity;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime DateInOut { get; set; }
        public string Activity { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public string Train { get; set; }
        public double? Quantity { get; set; }

    }
}
