using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisaWeb05.Core.Interface.Repository
{
    public interface  IGenaralRepository<T>
    {
        int UpdateTable(T entity);
        int InsertIntoTable(T entity);        
        List<T> GetAllData();

        T GetById(Guid id);
        T GetNearestEmployeeCodeByCreatedDate();
        int DeteleRecordById(Guid id);
        List<T> GetElementsWithNumberPage(int? limit, int? offset, string? employeeFilter);
        List<T> GetElementsByFilter(string? employeeFilter);
    }
}
