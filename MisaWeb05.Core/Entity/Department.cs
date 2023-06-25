namespace MisaWeb05.Core.Entity
{
    /// <summary>
    /// Lớp phòng  ban
    /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
    /// </summary>
    public class Department:Base
    {
        #region Constructor
        public Department()
        {
            this.DepartmentId = Guid.NewGuid();
        }
        #endregion


        #region Properties
        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }


        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        #endregion


    }
}
