using System;

namespace Com.Danliris.ETL.Service.ViewModels
{
    public class FilePeriodeListViewModel
    {
        public int id { get; set; }
        public string month { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public int AreaId { get; set; }
    }
}