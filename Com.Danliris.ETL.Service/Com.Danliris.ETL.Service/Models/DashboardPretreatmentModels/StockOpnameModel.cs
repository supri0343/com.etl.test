using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels
{
    public class StockOpnameModel
    {
        public StockOpnameModel(string id, int no, DateTime date, string sPNo, string material, string train, double? length, string description)
        {
            Id = id;
            No = no;
            Date = date;
            SPNo = sPNo;
            Material = material;
            Train = train;
            Length = length;
            Description = description;
        }

        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string SPNo { get; set; }
        public string Material { get; set; }
        public string Train { get; set; }
        public double? Length { get; set; }
        public string Description { get; set; }        
    }
}
