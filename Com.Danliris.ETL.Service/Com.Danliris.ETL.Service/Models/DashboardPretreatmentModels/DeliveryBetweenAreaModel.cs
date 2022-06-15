using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels
{
    public class DeliveryBetweenAreaModel
    {
        public DeliveryBetweenAreaModel(string id, int no, DateTime date, string sP, string construction, double? qty, string train, string grade, string activity, string productionSubcon, string destination, string reprocess, string desc, string area, string description, string problem)
        {
            Id = id;
            No = no;
            Date = date;
            SP = sP;
            Construction = construction;
            Qty = qty;
            Train = train;
            Grade = grade;
            Activity = activity;
            ProductionSubcon = productionSubcon;
            Destination = destination;
            Reprocess = reprocess;
            Desc = desc;
            Area = area;
            Description = description;
            Problem = problem;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string SP { get; set; }
        public string  Construction { get; set; }
        public double? Qty { get; set; }
        public string Train { get; set; }
        public string Grade { get; set; }
        public string Activity { get; set; }
        public string ProductionSubcon { get; set; }
        public string Destination { get; set; }
        public string Reprocess { get; set; }
        public string Desc { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public string Problem { get; set; }

    }
}
