using Com.Danliris.ETL.Service.ExcelModels;

namespace Com.Danliris.ETL.Service.DBAdapters.FinishingAdapters
{
    public interface IFinishingAdapters<TModel>
    {
        
    }

    public interface IChemicalAdapter : IBaseAdapter<DashboardChemicalF>, IFinishingAdapters<DashboardChemicalF>
    {
        
    }

    public interface IProductionAdapter : IBaseAdapter<DashboardProductionF>, IFinishingAdapters<DashboardProductionF>
    {
        
    }

    public interface IReceiveAdapter : IBaseAdapter<DashboardReceiveF>, IFinishingAdapters<DashboardReceiveF>
    {
        
    }

    public interface IReprocessAdapter : IBaseAdapter<DashboardReprocessF>, IFinishingAdapters<DashboardReprocessF>
    {
        
    }

    public interface IStockOpnameAdapter : IBaseAdapter<DashboardStockOpnameF>, IFinishingAdapters<DashboardStockOpnameF>
    {
        
    }
    public interface ITransferAdapter : IBaseAdapter<DashboardTransferF>, IFinishingAdapters<DashboardTransferF>
    {
        
    }
}