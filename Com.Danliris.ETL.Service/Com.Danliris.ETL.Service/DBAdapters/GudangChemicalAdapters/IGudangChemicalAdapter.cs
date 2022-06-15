using Com.Danliris.ETL.Service.Models.DashboardGudangChemicalModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.ETL.Service.DBAdapters.GudangChemicalAdapters
{
    public interface IGudangChemicalAdapter<TModel>
    {
         
    }

    public interface IChemicalStockAdapter : IBaseAdapter<ChemicalStockModel> ,  IGudangChemicalAdapter<ChemicalStockModel>
    {

    }
    public interface IReceiptItemAdapter : IBaseAdapter<ChemicalReceiptItemModel>, IGudangChemicalAdapter<ChemicalReceiptItemModel>
    {

    }
    public interface IReleaseItemAdapter : IBaseAdapter<ChemicalReleaseItemModel>, IGudangChemicalAdapter<ChemicalReleaseItemModel>
    {

    }
}
