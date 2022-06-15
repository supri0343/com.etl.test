using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPrintingModels
{
    public class DashBoardPasteModel
    {
        public DashBoardPasteModel(string id, int no, DateTime date, string production, string shift, string group, string sPNo, double? newProductionDrum, double? reworkDrum, double? thicknerProductionDrum, double? pVAGlueProductionDrum)
        {
            Id = id;
            No = no;
            Date = date;
            Production = production;
            Shift = shift;
            Group = group;
            SPNo = sPNo;
            NewProductionDrum = newProductionDrum;
            ReworkDrum = reworkDrum;
            ThicknerProductionDrum = thicknerProductionDrum;
            PVAGlueProductionDrum = pVAGlueProductionDrum;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date{ get; set; }
        public string  Production { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string SPNo { get; set; }
        public double? NewProductionDrum { get; set; }
        public double? ReworkDrum { get; set; }
        public double? ThicknerProductionDrum { get; set; }
        public double? PVAGlueProductionDrum { get; set; }

    }
}
