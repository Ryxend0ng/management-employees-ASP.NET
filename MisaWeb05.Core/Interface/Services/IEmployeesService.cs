using Microsoft.AspNetCore.Http;
using MisaWeb05.Core.Entity;
using MisaWeb05.Core.Interface.Services;
using System.Collections;

namespace MisaWeb05.Core.Interface.Services
{
    public interface IEmployeesService :IGeneralService<Employee>
    {
        Hashtable ValidateFormEmployee(Employee employee,string type);
        string GetNewEmployeeCode();

        List<Employee> GetAllDataEmployee();

        // thực hiện nhập khẩu dữ liệu
         IEnumerable<Employee> Import(IFormFile fileImport);
        
    }
}
