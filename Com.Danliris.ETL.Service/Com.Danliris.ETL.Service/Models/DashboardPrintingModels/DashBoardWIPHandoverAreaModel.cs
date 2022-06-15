using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPrintingModels
{
    public class DashBoardWIPHandoverAreaModel
    {
        public DashBoardWIPHandoverAreaModel(string id, int no, DateTime date, string sPNo, string construction, string trainNo, string activity, string productionSubcon, string destination, string reprocess, string description, string area, DateTime? pOK, string descriptionSubcon, string processTypeItem, double? a, double? b, double? c, double? bS, double? total, string yarnNo, double? fabricWidth)
        {
            Id = id;
            No = no;
            Date = date;
            SPNo = sPNo;
            Construction = construction;
            TrainNo = trainNo;
            Activity = activity;
            ProductionSubcon = productionSubcon;
            Destination = destination;
            Reprocess = reprocess;
            Description = description;
            Area = area;
            POK = pOK;
            DescriptionSubcon = descriptionSubcon;
            ProcessTypeItem = processTypeItem;
            A = a;
            B = b;
            C = c;
            BS = bS;
            Total = total;
            YarnNo = yarnNo;
            FabricWidth = fabricWidth;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string SPNo { get; set; }
        public string Construction { get; set; }
        public string  TrainNo { get; set; }
        public string Activity { get; set; }
        public string ProductionSubcon { get; set; }
        public string Destination { get; set; }
        public string Reprocess { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
        public DateTime? POK { get; set; }
        public string DescriptionSubcon { get; set; }
        public string ProcessTypeItem { get; set; }
        public double? A { get; set; }
        public double? B { get; set; }
        public double? C { get; set; }
        public double? BS { get; set; }
        public double? Total { get; set; }
        public string YarnNo { get; set; }
        public double? FabricWidth { get; set; }
    }
}
