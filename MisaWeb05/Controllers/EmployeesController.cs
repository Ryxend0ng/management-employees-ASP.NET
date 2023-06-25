using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisaWeb05.Core.Entity;
using MisaWeb05.Core.Dto;
using MisaWeb05.Core.Interface.Services;
using Newtonsoft.Json;
using System.Collections;
using OfficeOpenXml;

namespace MisaWeb05.api.Controllers
{
    /// <summary>
    /// Lớp nhan viên controller
    /// 
    /// </summary>
    /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IEmployeesService employeesService;
        IDepartmentService departmentService;
        public EmployeesController(IEmployeesService employeesService, IDepartmentService departmentService)
        {
            this.employeesService = employeesService;
            this.departmentService = departmentService;
        }


        /// <summary>
        /// API lấy toàn bộ employee
        /// 
        /// </summary>
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
              
                // lấy toàn bộ dữ liệu về danh sách nhân viên
                List<Employee> listEmpTotal = employeesService.GetAllDataEmployee();
                // Tạo đối tượng page để tính toán dành cho totalRecord và totalPage
                Page<Employee> pageEmp = new(listEmpTotal, true);
                pageEmp.pageSize = 10;

                // tạo đối tượng data lưu trữ dữ liệu, totalrecord,totalpage
                var data = new
                {
                    Data= (listEmpTotal),
                    TotalRecord= listEmpTotal.Count,
                    TotalPage=pageEmp.GetTotalPages()
                };
                // Format JSON về dạng viết hoa chữ cái đầu
                var str=JsonConvert.SerializeObject(data, Formatting.Indented);
                return Ok(str);
            }
            catch (Exception ex)
            {
                // trả về lỗi back end
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
        /// API lấy Employee theo EmployeeId
        ///
        /// </summary>
        ///  CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>
        /// <remarks>
        /// 
        ///    Test Id: "002988c1-f7bc-11ec-82ee-0259e1bc84a2"
        /// 
        /// </remarks>
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById(Guid employeeId)
        {
            try
            {
                return Ok(employeesService.GetServiceById(employeeId));
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
        /// API thêm employee
        /// 
        /// </summary>
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>

        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {
            // 1 đối tượng hashtable để lấy dữ liệu về 2 key
            // 1. key : check: để kiểm tra nếu là true thì thực hiện thêm , false ngược lại
            // 2. key: err: Biến để lưu trữ lỗi khi validate
            Hashtable checkForm = employeesService.ValidateFormEmployee(employee,"add");
            try
            {
                bool check = (bool)checkForm["check"];


                if (check)
                {
                    // lấy toàn bộ danh sách department
                    List<Department> lDepart = departmentService.GetAllDatasService();
                    // so sánh với departid trong đối tượng employee được gửi từ front end
                    // nếu đúng thì trả về object department
                    Department? dpObj = lDepart.Find(dp => dp.DepartmentId.Equals(employee.DepartmentId));
                   
                    return Ok(employeesService.InsertService(employee));
                }
                else
                {        
                        // nếu lỗi thì trả về 400
                        var resErr400 = new
                        {
                            devMsg = "Lỗi validate",
                            userMsg = checkForm["err"]
                        };
                        return StatusCode(400, resErr400);
                    
                    
                }
                
            }
            catch (Exception ex)
            {
               
                var resErr = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ Dong để biết thêm chi tiết"
                };
                return StatusCode(500, resErr);
                throw;
            }
        }

        /// <summary>
        /// API sửa employee
        /// 
        /// </summary>
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            // 1 đối tượng hashtable để lấy dữ liệu về 2 key
            // 1. key : check: để kiểm tra nếu là true thì thực hiện thêm , false ngược lại
            // 2. key: err: Biến để lưu trữ lỗi khi validate
            Hashtable checkForm = employeesService.ValidateFormEmployee(employee,"edit");
            try
            {

                bool check = (bool)checkForm["check"];


                if (check)
                {
                    // lấy toàn bộ danh sách department
                    List<Department> lDepart = departmentService.GetAllDatasService();
                    // so sánh với departid trong đối tượng employee được gửi từ front end
                    // nếu đúng thì trả về object department
                    Department? dpObj = lDepart.Find(dp => dp.DepartmentId.Equals(employee.DepartmentId));
                    
                    return Ok(employeesService.UpdateService(employee));
                }
                else
                {
                    // nếu lỗi thì trả về 400
                    var resErr400 = new
                    {
                        devMsg = "Lỗi validate",
                        userMsg = checkForm["err"]
                    };
                    return StatusCode(400, resErr400);


                }

            }
            catch (Exception ex)
            {

                var resErr = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ Dong để biết thêm chi tiết"
                };
                return StatusCode(500, resErr);
                throw;
            }
        }

        /// <summary>
        /// API xóa employee
        /// 
        /// </summary>
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>
        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(Guid employeeId)
        {
            try
            {

                return Ok(employeesService.DeleteService(employeeId));
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
        /// API lọc theo page và tìm kiếm employee
        /// 
        /// </summary>
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>
        [HttpGet ("filter")]
        public IActionResult FlterWithPagination(int? pageSize,int? pageNumber,string? employeeFilter)
        {
            try

            {
               

                int pSize = pageSize ?? 10;
                int pNumber=pageNumber ?? 1;
                // lấy danh sách nhân viên theo  mã nhân viên, tên nhân viên
                List<Employee> listEmpFilter = employeesService.GetDatasByFilter(employeeFilter);
                Page<Employee> pageEmpFilter = new Page<Employee>(listEmpFilter, true)
                {
                    pageSize = pSize
                };

                // lấy danh sách nhân viên theo pagesize,pagenunber và mã nhân viên, tên nhân viên
                // Class page để thực hiện totalrecord và totalpage
                List<Employee> listEmp = employeesService.GetDatasWithNumberPage(pSize, pNumber, employeeFilter);
                for(int i=0; i < listEmp.Count; i++)
                {
                    if (listEmp[i].Gender == 1)
                    {
                        listEmp[i].GenderName = "Nam";
                    }else if(listEmp[i].Gender == 0)
                    {
                        listEmp[i].GenderName = "Nữ";
                    }
                    else
                    {
                        listEmp[i].GenderName = "Khác";
                    }
                }
                Page<Employee> pageEmp = new Page<Employee>(listEmp, true)
                {
                    pageSize = pSize
                };

               
                
                // tạo 1 biến check = false, nếu employeeFilter là null thì gán là true
                // mục đích: để hiển thị totalrecord và totalpage theo tìm kiếm nếu employeeFilter khác null
                bool check = false;
                if (string.IsNullOrEmpty(employeeFilter))
                {
                    check=true;
                }

                // tạo 1 đối tượng data lưu trữ 3 thuộc tính
                // Data: dnah sách nhân viên
                // TotalRecord: tổng số record dựa theo biến check
                // TotalPage: tổng số page dựa theo biến check
                var data = new
                {
                    Data = (listEmp),
                    TotalRecord = listEmpFilter.Count ,
                    TotalPage =pageEmpFilter.GetTotalPages() 
                };
                var str = JsonConvert.SerializeObject(data, Formatting.Indented);
                return Ok(str);
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
        /// API lấy một mã  employee code mới
        /// 
        /// </summary>
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {

                return Ok(employeesService.GetNewEmployeeCode());
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
        /// API nhập khẩu
        /// 
        /// </summary>
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>
        [HttpPost("import")]
        public IActionResult ImportExcel(IFormFile fileImport)
        {
            try
            {

                return Ok(employeesService.Import(fileImport));
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
        /// API nhập khẩu
        /// 
        /// </summary>
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// <returns></returns>
        [HttpGet("export")]
        public IActionResult ExportExcel()
        {
            try
            {
                string excelName = $"Danhsachnhanvien.xlsx";
                var list =employeesService.GetAllDataEmployee();
                string downloadUrl = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, excelName);
                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                    workSheet.Cells.LoadFromCollection(list, true);
                    package.Save();
                }
                stream.Position = 0;

                //return File(stream, "application/octet-stream", excelName);  
                

                return (File(stream, "application/octet-stream", excelName));
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
