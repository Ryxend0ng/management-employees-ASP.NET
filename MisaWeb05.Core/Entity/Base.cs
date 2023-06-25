namespace MisaWeb05.Core.Entity
{
    /// <summary>
    /// Lớp BaseModel là lớp chứa các thuộc tính chung được các lớp khác kế thừa
    /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
    /// </summary>
    public abstract class Base
    {

        #region Properties
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }


        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public string? ModifiedBy { get; set; }
        #endregion

    }
}
