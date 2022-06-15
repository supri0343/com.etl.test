using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardGudangSparepartModels
{
    public class StockSparepartModel
    {
        public StockSparepartModel(string id, int no, DateTime date, string itemCode, string rackNo, string masterCode, string itemName, string unit, double? earlyS, double? @in, double? @out, double? finalS)
        {
            Id = id;
            No = no;
            Date = date;
            ItemCode = itemCode;
            RackNo = rackNo;
            MasterCode = masterCode;
            ItemName = itemName;
            Unit = unit;
            EarlyS = earlyS;
            In = @in;
            Out = @out;
            FinalS = finalS;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string ItemCode { get; set; }
        public string RackNo { get; set; }
        public string MasterCode { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double? EarlyS { get; set; }
        public double? In { get; set; }
        public double? Out { get; set; }
        public double? FinalS { get; set; }
    }
}
