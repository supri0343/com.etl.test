using Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.ETL.Service.DBAdapters.PretreatmentAdapters
{
    public interface IPretreatmentAdapter<TModel>
    {
        
    }

    public interface IMachineProductionAdapter : IBaseAdapter<PretreatmentMachineProductionModel>, IPretreatmentAdapter<PretreatmentMachineProductionModel>
    {

    }
    public interface IMaterialPreparingAdapter : IBaseAdapter<IncomingMaterialPreparingModel>, IPretreatmentAdapter<IncomingMaterialPreparingModel>
    {

    }

    public interface IMaterialTroubleAdapter : IBaseAdapter<MaterialTroubleModel>, IPretreatmentAdapter<MaterialTroubleModel>
    {

    }

    public interface IReprocessAdapter : IBaseAdapter<ReprocessModel>, IPretreatmentAdapter<ReprocessModel>
    {

    }

    public interface IStockMaterialAdapter : IBaseAdapter<StockMaterialModel>, IPretreatmentAdapter<StockMaterialModel>
    {

    }

    public interface IStockOpnameAdapter : IBaseAdapter<StockOpnameModel>, IPretreatmentAdapter<StockOpnameModel>
    {

    }

    public interface IDeliveryBetweenAdapter : IBaseAdapter<DeliveryBetweenAreaModel>, IPretreatmentAdapter<DeliveryBetweenAreaModel>
    {

    }


}
