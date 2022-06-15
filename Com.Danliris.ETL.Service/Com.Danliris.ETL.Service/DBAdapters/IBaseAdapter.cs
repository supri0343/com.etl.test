using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.ETL.Service.DBAdapters
{
    public interface IBaseAdapter<TModel>
    {
        // Task Insert(TModel model);
        // Task Delete(TModel model);
        // Task Update(TModel model);
        Task DeleteByMonthAndYear(DateTime periode);
        Task InsertBulk(IEnumerable<TModel> models);
    }
}
