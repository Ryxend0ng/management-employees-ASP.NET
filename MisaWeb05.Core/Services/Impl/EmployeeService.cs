using MisaWeb05.Core.Entity;
using MisaWeb05.Core.Interface.Services;
using MisaWeb05.Core.Interface.Repository;
using MisaWeb05.Core.Dto;
using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;
using System;
using OfficeOpenXml.Style;
namespace MisaWeb05.Core.Services.Impl
{
    public class EmployeeService : BaseService<Employee>, IEmployeesService
    {

        IEmployeesRepository employeesRepository;
        #region Constructor
        public EmployeeService(IEmployeesRepository genaralRepository) : base(genaralRepository)
        {
            employeesRepository = genaralRepository;
        }


        #endregion


        /// <summary>
        /// hàm thực hiện lấy mã nhân viên mới nhất
        /// Giải thích: Lấy bản ghi mới thêm gần nhất từ csdl
        /// dùng regex để lấy là phần chữ và phần số của employeeCode
        /// về phần số thực hiện ramdom từ 0 đến 1000 và cộng với phần số của employeeCode
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        public string GetNewEmployeeCode()
        {
            Employee employ = employeesRepository.GetNearestEmployeeCodeByCreatedDate();
            string empCode = employ.EmployeeCode;
            /** Xử lý khi nhân bản nhân viên */
            // dùng regex lọc ra gía trị số của mã nhân viên
            Regex rg = new Regex(@"\d");
            var empNumber = "";
            foreach (Match item in rg.Matches(empCode))
            {
                empNumber += item.Value;
            }
            // dùng regex lọc ra chữ của mã nhân viên
            rg = new Regex(@"[a-zA-Z\-_]");
            var empString = "";
            foreach (Match item in rg.Matches(empCode))
            {
                empString += item.Value;
            }
            // thực hiện tạo mã nhân viên mới , lấy ngẫu nhiên 1 số từ 0 - 1000 
            // và cộng với giá trị trong mã nhân viên hiện tại
            var empNumberNew = (int.Parse(empNumber) + 1).ToString();
            var employeeCodeNew = "";
            if (int.Parse(empNumber) < 10)
            {

                empNumberNew = $"0{empNumberNew}";
                employeeCodeNew = (empString + empNumberNew);
            }
            else
            {
                employeeCodeNew = empString + empNumberNew;
            }

            return employeeCodeNew;
            /** kết thúc Xử lý khi nhân bản nhân viên */
        }


        /// <summary>
        /// hàm thực hiện validate dữ liệu
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>trả về 1 hashtabel chứa 2 key
        /// check và err</returns>
        public Hashtable ValidateFormEmployee(Employee employee, string type)
        {
            try
            {
                string errMsg = "";
                bool check = true;
                List<Employee> listEmp = employeesRepository.GetAllData();
                string empCode = employee.EmployeeCode;

                // kiểm tra xem có trùng mã nhân viên không trong trường hợp thêm mới
                string empCodeCheck = "";
                if (!string.IsNullOrEmpty(empCode) && type.Equals("add"))
                {
                    Employee e = listEmp.Find(e => e.EmployeeCode.Equals(empCode));
                    empCodeCheck = e != null ? e.EmployeeCode : "";
                }
                //------------------
                string empName = employee.EmployeeName;
                // Tạo 1 nullable để tránh dữ liệu trả về null 
                Nullable<Guid> department = employee.DepartmentId.CompareTo(Guid.Empty) != 0 ? employee.DepartmentId : null;
                Nullable<DateTime> empDOB = employee.DateOfBirth.HasValue ? employee.DateOfBirth : null;
                int? empGen = employee.Gender;
                string? empIdentity = employee.IdentityNumber;
                Nullable<DateTime> empIdenPlace = employee.IdentityDate.HasValue ? employee.IdentityDate : null;
                string? empEmail = employee.Email;

                // bắt đầu kiểm tra dữ liệu
                if (string.IsNullOrEmpty(empCode))
                {
                    check = false;
                    errMsg += "Mã nhân viên không được phép trống ,";
                }
                else if (!Regex.IsMatch(empCode, @"^[a-zA-Z\-_]*\d+$"))
                {

                    check = false;
                    errMsg += "Mã nhân viên phải bao gồm chữ và số ,";

                }
                else if (!string.IsNullOrEmpty(empCodeCheck))
                {
                    check = false;
                    errMsg += "Mã nhân viên đã bị trùng ,";
                }

                if (string.IsNullOrEmpty(empName))
                {
                    check = false;
                    errMsg += "Tên nhân viên không được phép trống ,";
                }

                if (!(department.HasValue))
                {
                    check = false;
                    errMsg += "Thông tin đơn vị không được phép trống ,";
                }

                if (!string.IsNullOrEmpty(empEmail))
                {
                    if (!Regex.IsMatch(empEmail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                    {
                        check = false;
                        errMsg += "Email không đúng định dạng ,";
                    }
                }
                if (empDOB.HasValue)
                {
                    if (empDOB.Value.Year >= 2022)
                    {
                        check = false;
                        errMsg += "Năm sinh phải nhỏ hơn năm hiện tại ,";
                    }
                }

                if (empIdenPlace.HasValue)
                {
                    if (empIdenPlace.Value.Year >= 2022)
                    {
                        check = false;
                        errMsg += "Ngày cấp không được vượt quá năm hiện tại ";
                    }
                }
                // kết thúc kiểm tra dữ liệu
                if (string.IsNullOrEmpty(errMsg))
                {
                    check = true;
                }
                // bỏ dữ liệu vào 1 hashtable
                Hashtable hs = new Hashtable();
                hs.Add("check", check);
                hs.Add("err", errMsg);
                return hs;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// hàm thực hiện lấy toàn bộ dữ liệu nhân viên
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>trả về 1 hashtabel chứa 2 key
        /// check và err</returns>
        public List<Employee> GetAllDataEmployee()
        {
            return employeesRepository.GetAllDataEmployee();
        }

        public IEnumerable<Employee> Import(IFormFile formFile)
        {
            // check tệp
            /*if (formFile == null || formFile.Length <= 0)
            {
                return DemoResponse<List<UserInfo>>.GetResult(-1, "formfile is empty");
            }*/

            // check tệp là excel
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Tệp không đúng định dạng");
            }

            var list = new List<Employee>();

            // MemoryStream(): có tác dụng là luồng làm việc 
            // với dữ liệu trên vùng nhớ
            // một đối tượng nó quản lý bộ đệm
            // là 1 mảng các byte
            using (var stream = new MemoryStream())
            {
                // Sao chép không đồng bộ nội dung của tệp đã tải lên vào luồng đích.
                formFile.CopyToAsync(stream);

                // sau đó using Excelpage để nó làm việc trên tệp đã tải
                // đọc đc tệp đó
                using (var package = new ExcelPackage(stream))
                {
                    // Worksheets[0] lấy bảng sheet đầu tiên trong tệp excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    // lấy tổng bản số dòng mà execl có
                    var rowCount = worksheet.Dimension.Rows;

                    // dùng for để đọc từng dòng
                    // phải bắt đầu từ row=2 vì dòng 1 chứa tên tiêu đề
                    for (int row = 2; row <= rowCount; row++)
                    {
                        // lấy ra dữ liệu theo dòng và cột
                        var empCode = ConvertToString(worksheet.Cells[row, 2].Value);
                        var empName = ConvertToString(worksheet.Cells[row, 3].Value);
                        var empGender = ConvertToString(worksheet.Cells[row, 4].Value);
                        var departname = ConvertToString(worksheet.Cells[row, 6].Value);
                        var dateOfBirth = ConvertDateTime(worksheet.Cells[row, 5].Value);
                        var bankCount = ConvertToString(worksheet.Cells[row, 7].Value);
                        var bankName = ConvertToString(worksheet.Cells[row, 8].Value);
                        Employee emp = new Employee
                        {
                            EmployeeCode = empCode,
                            EmployeeName = empName,
                            GenderName = empGender,
                            DateOfBirth = dateOfBirth,
                            BankAccountNumber = bankCount,
                            BankBranchName = bankName,
                            DepartmentName = departname,

                        };
                        // thực hiện validate dữ liệu
                        Hashtable isValid = ValidateFormEmployee(emp, "add");
                        if (!(bool)isValid["check"])
                        {
                            if ((string)isValid["err"] != null)
                            {
                                emp.ImportSucess = false;
                                emp.ListErrorImport.Add((string)isValid["err"]);
                            }
                        }
                        list.Add(emp);
                    }
                }
            }
            if (list.Count > 0)
            {
                var res = employeesRepository.ImportEmployee(list.Where(e => e.ImportSucess == true));
            }
            return list;
        }

        public string ConvertToString(object? value)
        {
            if (value != null)
            {
                return value.ToString();
            }
            return null;
        }

        public DateTime? ConvertDateTime(object? value)
        {
            DateTime result;
            if (value != null)
            {
                if (DateTime.TryParse(value.ToString(), out result))
                {
                    return result;
                }
                else
                    return null;
            }
            else
                return null;

        }

       
    }
}
