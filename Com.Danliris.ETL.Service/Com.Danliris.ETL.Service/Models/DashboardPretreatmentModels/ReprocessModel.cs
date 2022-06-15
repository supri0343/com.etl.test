using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels
{
    public class ReprocessModel
    {
        public ReprocessModel(string id, int no, DateTime date, string shift, string group, string orderNo, string material, string trainNo, double? qtyIn, string machine, string processType, string reprocess, string problem)
        {
            Id = id;
            No = no;
            Date = date;
            Shift = shift;
            Group = group;
            OrderNo = orderNo;
            Material = material;
            TrainNo = trainNo;
            QtyIn = qtyIn;
            Machine = machine;
            ProcessType = processType;
            Reprocess = reprocess;
            Problem = problem;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public string TrainNo { get; set; }
        public double? QtyIn { get; set; }
        public string Machine { get; set; }
        public string ProcessType { get; set; }
        public string Reprocess { get; set; }
        public string Problem { get; set; }
    }
}
