using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisaWeb05.Core.Dto
{
    /// <summary>
    /// Lớp Page
    /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
    /// </summary>
    public class Page<T> : APage
    {
        private bool isPage = true;

       #region Constructor
        public Page(List<T> content, bool isPage)
        {
            // kiểm tra nếu dánh sách là null thì gán bằng 0 ngược lại gán là số lượng bản ghi của danh sách
            totalElements = null == content ? 0 : content.Count;
            this.isPage = isPage;
        }
        #endregion

        /// <summary>
        /// Hàm kiểm tra  danh sách có phải lọc theo pagesize,pagenumber không
        /// mặc định isPage=true
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsPage()
        {
            return isPage;  
        }

        /// <summary>
        /// Hàm trả về trang hiện tại
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>nếu IsPage() là true thì trả về pageNumber ngược lại về 0</returns>
        public override int GetPageNumber()
        {        
            return IsPage() ? pageNumber : 0;
        }

        /// <summary>
        /// Hàm trả về số lượng trang
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>nếu IsPage() là true thì trả về số bản ghi trên trang ngược lại về tổng số phần tử của danh sách</returns>
        public int GetSize()
        {
            return IsPage()?GetPageSize():(int)totalElements;
        }

        /// <summary>
        /// Hàm trả về tổng số lượng bản ghi
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>tổng số lượng phần tử</returns>
        public override long GetTotalElements()
        {
            return totalElements;
        }

        /// <summary>
        /// Hàm trả về số lương trang
        /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>nếu GetSize()==0 thì trả về 1 ngược lại tính toán trả về số lượng trang</returns>
        public override int GetTotalPages()
        {
            return GetSize()==0?1:(int) Math.Ceiling((double) totalElements/(double)GetSize());
        }

        public override int GetPageSize()
        {
          
            return pageSize;
        }

       /* public bool HasPrevious()
        {
            return GetPageNumber() > 0;
        }

        public bool IsFirst()
        {
            return !HasPrevious();
        }

        public bool IsLast()
        {
            return !HasNext();
        }
        
        public bool HasNext()
        {
            return GetPageNumber()<GetTotalElements();
        }*/
    }
}
