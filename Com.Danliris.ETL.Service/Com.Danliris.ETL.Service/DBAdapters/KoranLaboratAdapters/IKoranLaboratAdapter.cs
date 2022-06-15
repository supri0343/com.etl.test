using Com.Danliris.ETL.Service.ExcelModels.DashboardKoranLaborat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.ETL.Service.DBAdapters.KoranLaboratAdapters
{
    public interface IKoranLaboratAdapter<TModel>
    {
        
    }

    public interface IDyeingTestAdapter : IBaseAdapter<DashboardDyeingTest> ,IKoranLaboratAdapter<DashboardDyeingTest>
    {
        
    }

    public interface ILabDipAdapter : IBaseAdapter<DashboardLabDip> ,IKoranLaboratAdapter<DashboardLabDip>
    {
        
    }

    public interface IPrintingLabAdapter : IBaseAdapter<DashboardPrintingLab> ,IKoranLaboratAdapter<DashboardPrintingLab>
    {
        
    }

    public interface IPrintingTestAdapter : IBaseAdapter<DashboardPrintingTest> ,IKoranLaboratAdapter<DashboardPrintingTest>
    {
        
    }

    public interface IRnDTestResultAdapter : IBaseAdapter<DashboardRnDTestResult> ,IKoranLaboratAdapter<DashboardRnDTestResult>
    {
        
    }
    
}