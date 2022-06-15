using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardGudangSparepartModels
{
    public class SparepartItemRelease
    {
        public SparepartItemRelease(string id, int no, DateTime date, string bonNo, string itemCode, string itemName, double? total, string unit, string mC, string takenBy, string area)
        {
            Id = id;
            No = no;
            Date = date;
            BonNo = bonNo;
            ItemCode = itemCode;
            ItemName = itemName;
            Total = total;
            Unit = unit;
            MC = mC;
            TakenBy = takenBy;
            Area = area;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string BonNo { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double? Total { get; set; }
        public string Unit { get; set; }
        public string MC { get; set; }
        public string TakenBy { get; set; }
        public string Area { get; set; }
    }
}
