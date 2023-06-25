using MisaWeb05.Core.Entity;
using MisaWeb05.Core.Interface.Services;
using MisaWeb05.Core.Interface.Repository;
namespace MisaWeb05.Core.Services.Impl
{
    public class DepartmentSevice : BaseService<Department>, IDepartmentService
    {
        IDepartmentsRepository _departmentsRepository;
        public DepartmentSevice(IDepartmentsRepository genaralRepository) : base(genaralRepository)
        {
            _departmentsRepository = genaralRepository;
        }
    }
}
