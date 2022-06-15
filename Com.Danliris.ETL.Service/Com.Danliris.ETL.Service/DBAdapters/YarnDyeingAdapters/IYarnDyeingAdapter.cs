using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.ExcelModels.DashboardYarnDyeing;

namespace Com.Danliris.ETL.Service.DBAdapters.YarnDyeingAdapters
{
    public interface IYarnDyeingAdapter<TModel>
    {
        Task Update(TModel model);
        Task<IEnumerable<TModel>> GetBySPNo(List<string> listSPNos);
    }

    public interface IDyeYarnRequestAdapter : IYarnDyeingAdapter<DashboardDyeYarnRequest>, IBaseAdapter<DashboardDyeYarnRequest>
    {
        
    }

    public interface IFabricYDOrderRequestAdapter : IYarnDyeingAdapter<DashboardFabricYDOrderRequest>, IBaseAdapter<DashboardFabricYDOrderRequest>
    {
        
    }

    public interface IMiniloomRequestAdapter : IYarnDyeingAdapter<DashboardMiniloomRequest>, IBaseAdapter<DashboardMiniloomRequest>
    {
        
    }

    public interface IYarnOrderRequestAdapter : IYarnDyeingAdapter<DashboardYarnOrderRequest>, IBaseAdapter<DashboardYarnOrderRequest>
    {
        
    }
}