using Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.Models.DashboardGudangChemicalModels
{
    public class ChemicalStockModel //: IEquatable<ChemicalStockModel>
    {
        public ChemicalStockModel()
        {

        }
        public ChemicalStockModel(string id, int no, DateTime date, string itemCode, string rackNo, string masterKd, string itemName, string unit, double? earlyS, double? @in, double? @out, double? finalS):this()
        {
            this.Id = id;
            this.No = no;
            this.Date = date;
            this.ItemCode = itemCode;
            this.RackNo = rackNo;
            this.MasterKd = masterKd;
            this.ItemName = itemName;
            this.Unit = unit;
            this.EarlyS = earlyS;
            this.In = @in;
            this.Out = @out;
            this.FinalS = finalS;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string ItemCode { get; set; }
        public string RackNo { get; set; }
        public string MasterKd { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double? EarlyS { get; set; }
        public double? In { get; set; }
        public double? Out { get; set; }
        public double? FinalS { get; set; }

    }
}
