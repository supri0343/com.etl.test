using System;

namespace Com.Danliris.ETL.Service.Models
{
    public class FilePeriode
    {
        public FilePeriode(string month, string name, int year, DateTime? createdAt, DateTime? updatedAt, int areaId)
        {
            this.month = month;
            this.name = name;
            this.year = year;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
            AreaId = areaId;
        }
        public int id { get; set; }
        public string month { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public int AreaId { get; set; }
    }
}