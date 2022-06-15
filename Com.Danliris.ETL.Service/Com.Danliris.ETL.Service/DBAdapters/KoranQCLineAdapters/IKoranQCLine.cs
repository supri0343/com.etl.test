using Com.Danliris.ETL.Service.ExcelModels.KoranQCLine;

namespace Com.Danliris.ETL.Service.DBAdapters.KoranQCLineAdapters
{
    public interface IKoranQCLine<TModel>
    {
        
    }

    public interface IDigitalPrintAdapter : IBaseAdapter<DigitalPrint>, IKoranQCLine<DigitalPrint>
    {

    }
    public interface IDyeingAdapter : IBaseAdapter<Dyeing>, IKoranQCLine<Dyeing>
    {

    }
    public interface IPretreatmentAdapter : IBaseAdapter<Pretreatment>, IKoranQCLine<Pretreatment>
    {

    }
    public interface IPrintingAdapter : IBaseAdapter<Printing>, IKoranQCLine<Printing>
    {

    }
}