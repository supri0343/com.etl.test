using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardGudangChemicalModels
{
    public class ChemicalReceiptItemModel
    {
        public ChemicalReceiptItemModel(string id, int no, DateTime date, string unitArea, string bonNo, string supplierName, string pONo, string code, string itemName, double? total, string unit, string description)
        {
            Id = id;
            No = no;
            Date = date;
            UnitArea = unitArea;
            BonNo = bonNo;
            SupplierName = supplierName;
            PONo = pONo;
            Code = code;
            ItemName = itemName;
            Total = total;
            Unit = unit;
            Description = description;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string UnitArea { get; set; }
        public string BonNo { get; set; }
        public string SupplierName { get; set; }
        public string PONo { get; set; }
        public string Code{ get; set; }
        public string ItemName { get; set; }
        public double? Total { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
    }
}
