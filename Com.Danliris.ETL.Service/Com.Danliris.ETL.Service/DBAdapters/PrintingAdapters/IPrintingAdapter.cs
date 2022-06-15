using Com.Danliris.ETL.Service.Models.DashboardPrintingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.ETL.Service.DBAdapters.PrintingAdapters
{
    public interface IPrintingAdapter<TModel>
    {
        
    }

    public interface IMainMachineAdapter : IBaseAdapter<DashBoardMainMachineModel>, IPrintingAdapter<DashBoardMainMachineModel>
    {

    }

    public interface ISupportMachineAdapter : IBaseAdapter<DashBoardSupportMachineModel>, IPrintingAdapter<DashBoardSupportMachineModel>
    {

    }

    public interface IPasteAdapter : IBaseAdapter<DashBoardPasteModel>, IPrintingAdapter<DashBoardPasteModel>
    {

    }

    public interface IEngravingAdapter : IBaseAdapter<DashBoardEngravingModel>, IPrintingAdapter<DashBoardEngravingModel>
    {

    }
    public interface IWipHandoverAdapter : IBaseAdapter<DashBoardWIPHandoverAreaModel>, IPrintingAdapter<DashBoardWIPHandoverAreaModel>
    {

    }
}
