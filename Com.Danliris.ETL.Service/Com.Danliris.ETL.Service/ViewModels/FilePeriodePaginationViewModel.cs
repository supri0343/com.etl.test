using System.Collections.Generic;
using Com.Danliris.ETL.Service.ViewModels;

namespace Com.Danliris.ETL.Service.ViewModels
{
    public class FilePeriodePaginationViewModel
    {
        public FilePeriodePaginationViewModel(List<FilePeriodeListViewModel> data, object info) 
        {
            this.data = data;
            this.info = info;
        }
        public List<FilePeriodeListViewModel> data { get; set; }
        public object info { get; set; }
    }
}