using Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.ETL.Service.DBAdapters.DyeingAdapters
{
    public interface IDyeingAdapter<TModel>
    {
        
    }
    public interface IDyeingMachineAdapter : IBaseAdapter<DashboardDyeingMachine>, IDyeingAdapter<DashboardDyeingMachine>
    {
    }

    public interface IHandoverAdapter : IBaseAdapter<DashboardHandOverArea>, IDyeingAdapter<DashboardHandOverArea>
    {

    }

    public interface ICkDyeingAdapter : IBaseAdapter<DashboardCKDyeing>, IDyeingAdapter<DashboardCKDyeing>
    {

    }

    public interface ILpgMachineUsageAdapter : IBaseAdapter<DashboardLPGMachineUsage>, IDyeingAdapter<DashboardLPGMachineUsage>
    {

    }

    public interface IWipMaterialAdapter : IBaseAdapter<DashboardWIPMaterial>, IDyeingAdapter<DashboardWIPMaterial>
    {

    }
}
