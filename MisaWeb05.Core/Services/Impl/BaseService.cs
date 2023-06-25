using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MisaWeb05.Core.Dto;
using MisaWeb05.Core.Interface.Services;
using MisaWeb05.Core.Interface.Repository;
namespace MisaWeb05.Core.Services.Impl
{
    /// <summary>
    /// Class baseservice chưa những hàm tổng quát
    /// createdBy: Nguyễn Văn Đông (23/06/2022)
    /// </summary>
   
    public class BaseService<T> : IGeneralService<T>
    {
        // inject interface repository
        IGenaralRepository<T> _genaralRepository;

        #region Constructor
        public BaseService(IGenaralRepository<T> genaralRepository)
        {
            _genaralRepository = genaralRepository;
        }
        #endregion

        /// <summary>
        /// hàm thực hiện xóa 1 bản ghi
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>1 nếu thành công</returns>
        public int DeleteService(Guid entityId)
        {
            return _genaralRepository.DeteleRecordById(entityId);
        }


        /// <summary>
        /// hàm thực hiện lấy toàn bộ dữ liệu từ bảng Employee
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>1 nếu thành công</returns>
        public List<T> GetAllDatasService()
        {
            return _genaralRepository.GetAllData();
        }

        /// <summary>
        /// hàm thực hiện lấy toàn bộ dữ liệu từ bảng theo filter
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>1 nếu thành công</returns>
        public List<T> GetDatasByFilter(string? employeeFilter)
        {
            return _genaralRepository.GetElementsByFilter(employeeFilter);
        }


        /// <summary>
        /// hàm thực hiện lấy các bản ghi theo pagesize,pageNumber và employeeFilter(mã nhân viên, tên nhân viên)
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>bản ghi lọc theo sql</returns>
        public List<T> GetDatasWithNumberPage(int? pageSize, int? pageCurrentNumber, string? employeeFilter)
        {
            //TODO: Đang làm đoạn này
            int pSize = pageSize ?? 10;
            int pNumber = pageCurrentNumber ?? 1;
            List<T> lEmployee = GetAllDatasService();
            Page<T> page = new(lEmployee, true)
            {
                pageNumber = pNumber,
                pageSize = pSize
            };
            int offset = (page.GetPageNumber() - 1) * page.GetPageSize();
            return _genaralRepository.GetElementsWithNumberPage(page.GetPageSize(), offset,employeeFilter);
        }


        /// <summary>
        /// hàm thực lấy 1 bản ghi theo ID
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>1 nếu thành công</returns>
        public T GetServiceById(Guid entityId)
        {
            return _genaralRepository.GetById(entityId);
        }


        /// <summary>
        /// hàm thực hiện thêm 1 bản ghi
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>1 nếu thành công</returns>
        public int InsertService(T entity)
        {

            return _genaralRepository.InsertIntoTable(entity);
        }


        /// <summary>
        /// hàm thực hiện sửa 1 bản ghi
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns>1 nếu thành công</returns>
        public int UpdateService(T entity)
        {
            return _genaralRepository.UpdateTable(entity);
        }


    }
}
