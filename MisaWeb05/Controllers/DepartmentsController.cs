using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisaWeb05.Core.Interface.Services;
using MisaWeb05.Core.Entity;
using Newtonsoft.Json;

namespace MisaWeb05.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class DepartmentsController : ControllerBase
    {
        //service
        public IDepartmentService _departmentsService;

        public DepartmentsController(IDepartmentService departmentsService)
        {
            _departmentsService = departmentsService;
        }


        /// <summary>
        /// API lấy toàn bộ phòng ban từ CSDL
        /// 
        /// </summary>
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            try
            {
                List<Department> listDep = _departmentsService.GetAllDatasService();
                return Ok(JsonConvert.SerializeObject(listDep, Formatting.Indented));
            }
            catch (Exception ex)
            {
                var resErr = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để biết thêm chi tiết"
                };
                return StatusCode(500, resErr);
                throw;
            }
        }

        /// <summary>
        /// API lấy phòng ban theo DepartmentId
        /// 
        /// </summary>
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>
        [HttpGet ("{departmentId}")]
        public IActionResult GetByDepartmentId( Guid departmentId)
        {
            try
            {
                return Ok(_departmentsService.GetServiceById(departmentId));
            }
            catch (Exception ex)
            {
                var resErr = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để biết thêm chi tiết"
                };
                return StatusCode(500, resErr);
                throw;
            }
        }

        /// <summary>
        /// API thêm phòng ban 
        /// 
        /// </summary>
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insertdepartment(Department department)
        {
            try
            {

                return Ok(_departmentsService.InsertService(department));
            }
            catch (Exception ex)
            {
                var resErr = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để biết thêm chi tiết"
                };
                return StatusCode(500, resErr);
                throw;
            }
        }

    }
}
