using Com.Danliris.ETL.Service.ExcelModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.DBAdapters.DigitalPrintingAdapters
{
    public interface IDigitalPrintingAdapter<TModel>
    {

    }

    public interface IDailyDeliveryAdapter : IBaseAdapter<DailyDelivery>, IDigitalPrintingAdapter<DailyDelivery>
    {

    }

    public interface IOrderInAdapter : IBaseAdapter<OrderIn>, IDigitalPrintingAdapter<OrderIn>
    {

    }

    public interface IProductionDigitalTransferAdapter : IBaseAdapter<ProductionDigitalTransfer>, IDigitalPrintingAdapter<ProductionDigitalTransfer>
    {

    }

    public interface ISOOrderDigitalTransferAdapter : IBaseAdapter<SOOrderDigitalTransfer>, IDigitalPrintingAdapter<SOOrderDigitalTransfer>
    {

    }

    public interface IWipAreaAdapter : IBaseAdapter<WipArea>, IDigitalPrintingAdapter<WipArea>
    {

    }
}
