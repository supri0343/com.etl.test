using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.ETL.Service.Services.Interfaces
{
    public interface IBaseService
    {
        Task Upload(ExcelWorksheets excel, DateTime periode);
    }
}
