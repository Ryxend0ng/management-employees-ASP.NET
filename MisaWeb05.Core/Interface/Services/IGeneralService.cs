using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisaWeb05.Core.Interface.Services
{
    public interface IGeneralService<T>
    {
        List<T> GetAllDatasService();
        T GetServiceById(Guid entityId);
        int InsertService(T entity);
        int UpdateService(T entity);

        int DeleteService(Guid entityId);

        List<T> GetDatasWithNumberPage(int? pageSize, int? pageCurrentNumber, string? employeeFilter);

        List<T> GetDatasByFilter(string? employeeFilter);
    }
}
