using Com.Danliris.ETL.Service.Models.DashboardGudangSparepartModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.ETL.Service.DBAdapters.GudangSparepartAdapters
{
    public interface ISparepartAdapter<TModel>
    {

    }

    public interface IStockSparepartAdapter : IBaseAdapter<StockSparepartModel>, ISparepartAdapter<StockSparepartModel>
    {

    }

    public interface IReceiptItemSparepartAdapter : IBaseAdapter<SparepartItemReceipt>, ISparepartAdapter<SparepartItemReceipt>
    {

    }

    public interface IReleaseItemSparepartAdapter :IBaseAdapter<SparepartItemRelease>, ISparepartAdapter<SparepartItemRelease>
    {

    }
}
