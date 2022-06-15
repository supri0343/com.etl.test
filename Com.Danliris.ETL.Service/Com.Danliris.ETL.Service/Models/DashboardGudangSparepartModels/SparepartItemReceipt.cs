using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardGudangSparepartModels
{
    public class SparepartItemReceipt
    {
        public SparepartItemReceipt(string id, int no, DateTime date, string area, string bonNo, string supplier, string pONo, string itemCode, string itemName, double? total, string unit, string description)
        {
            Id = id;
            No = no;
            Date = date;
            Area = area;
            BonNo = bonNo;
            Supplier = supplier;
            PONo = pONo;
            ItemCode = itemCode;
            ItemName = itemName;
            Total = total;
            Unit = unit;
            Description = description;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public string Supplier { get; set; }
        public string PONo { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double? Total { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
    }
}
