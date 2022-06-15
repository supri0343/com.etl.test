using Com.Danliris.ETL.Service.Models.DashboardMaintenanceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.ETL.Service.DBAdapters.MaintenanceAdapters
{
    public interface IMaintenanceAdapter<TModel>
    {
        
    }

    public interface IDashboardHandoverAdapter : IBaseAdapter<DashboardHandoverModel>, IMaintenanceAdapter<DashboardHandoverModel>
    {

    }
    public interface IDashboardLkmAdapter : IBaseAdapter<DashboardLkmModel>, IMaintenanceAdapter<DashboardLkmModel>
    {

    }
}
