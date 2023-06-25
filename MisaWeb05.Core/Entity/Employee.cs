namespace MisaWeb05.Core.Entity
{
    /// <summary>
    /// Lớp Nhân viên
    /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
    /// </summary>
    public class Employee :Base
    {
        #region Properties

        /// <summary>
        /// Id của nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Ngày sinh nhân viên
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }
        /// <summary>
        ///  Chứng minh nhân dân
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CMND
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string? EmployeePosition { get; set; }

        /// <summary>
        /// Nới cấp CMND
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Địa chỉ nhân viên
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Điện thoại nhân viên
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Tên chi nhánh
        /// </summary>
        public string? BankBranchName { get; set; }


        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string? GenderName { get; set; }


        #endregion

        #region Import
        public bool? ImportSucess { get; set; } = false;
        public List<string> ListErrorImport { get; set; } =new List<string>();
        #endregion
    }
}
