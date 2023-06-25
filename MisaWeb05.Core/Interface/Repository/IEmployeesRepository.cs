using MisaWeb05.Core.Entity;
namespace MisaWeb05.Core.Interface.Repository
{
    public interface IEmployeesRepository: IGenaralRepository<Employee>
    {
        List<Employee> GetAllDataEmployee();
        int ImportEmployee(IEnumerable<Employee> employees);
     }
        
    
}
