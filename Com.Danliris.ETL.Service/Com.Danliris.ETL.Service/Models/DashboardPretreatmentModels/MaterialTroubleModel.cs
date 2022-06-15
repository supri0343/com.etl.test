using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels
{
    public class MaterialTroubleModel
    {
        public MaterialTroubleModel(string id, int no, DateTime date, string orderNo, string material, string buyer, double? bQ, double? bS, double? total, string problem)
        {
            Id = id;
            No = no;
            Date = date;
            OrderNo = orderNo;
            Material = material;
            Buyer = buyer;
            BQ = bQ;
            BS = bS;
            Total = total;
            Problem = problem;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public string Buyer { get; set; }
        public double? BQ { get; set; }
        public double? BS { get; set; }
        public double? Total { get; set; }
        public string Problem { get; set; }
    }
}
