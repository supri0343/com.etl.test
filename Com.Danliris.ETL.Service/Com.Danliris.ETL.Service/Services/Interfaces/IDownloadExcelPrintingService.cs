using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.ETL.Service.Services.Interfaces
{
    public interface IDownloadExcelPrintingService
    {
        Stream Download(DateTime periode);
    }
}
