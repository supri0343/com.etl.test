using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardMaintenanceModels
{
    public class DashboardLkmModel
    {
        public DashboardLkmModel(string id, int no, DateTime date, string section, string machine, string problem, string action, string usageSparepart, string description, string operator1, string operator2, string operator3, string knownBy, string team, DateTime? remaintenanceDate)
        {
            Id = id;
            No = no;
            Date = date;
            Section = section;
            Machine = machine;
            Problem = problem;
            Action = action;
            UsageSparepart = usageSparepart;
            Description = description;
            Operator1 = operator1;
            Operator2 = operator2;
            Operator3 = operator3;
            KnownBy = knownBy;
            Team = team;
            RemaintenanceDate = remaintenanceDate;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Section { get; set; }
        public string Machine { get; set; }
        public string Problem { get; set; }
        public string Action { get; set; }
        public string UsageSparepart { get; set; }
        public string Description { get; set; }
        public string Operator1 { get; set; }
        public string Operator2 { get; set; }
        public string Operator3 { get; set; }
        public string KnownBy { get; set; }
        public string Team { get; set; }
        public DateTime? RemaintenanceDate { get; set; }
    }
}
