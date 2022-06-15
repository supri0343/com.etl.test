using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardMaintenanceModels
{
    public class DashboardHandoverModel
    {
        public DashboardHandoverModel(string id, int no, DateTime date, string machine, string repair, string result, string team, string implementer, string kasubsieVerification, string kasieVerification, string production)
        {
            Id = id;
            No = no;
            Date = date;
            Machine = machine;
            Repair = repair;
            Result = result;
            Team = team;
            Implementer = implementer;
            KasubsieVerification = kasubsieVerification;
            KasieVerification = kasieVerification;
            Production = production;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Machine { get; set; }
        public string Repair { get; set; }
        public string Result { get; set; }
        public string Team { get; set; }
        public string Implementer { get; set; }
        public string KasubsieVerification { get; set; }
        public string KasieVerification { get; set; }
        public string Production { get; set; }


    }
}
