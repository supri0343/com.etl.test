using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPrintingModels
{
    public class DashBoardEngravingModel
    {
        public DashBoardEngravingModel(string id, int no, DateTime date, string shift, string group, string machine, string sPNo, double? totalProductionScreen, double? totalRepairScreen)
        {
            Id = id;
            No = no;
            Date = date;
            Shift = shift;
            Group = group;
            Machine = machine;
            SPNo = sPNo;
            TotalProductionScreen = totalProductionScreen;
            TotalRepairScreen = totalRepairScreen;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }        
        public string Shift { get; set; }
        public string Group { get; set; }
        public string Machine { get; set; }
        public string SPNo { get; set; }
        public double? TotalProductionScreen { get; set; }
        public double? TotalRepairScreen { get; set; }
    }
}
