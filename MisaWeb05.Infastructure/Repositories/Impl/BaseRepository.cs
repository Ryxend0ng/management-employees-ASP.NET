using Dapper;
using MySqlConnector;
using Missa05.Configs;
using MisaWeb05.Core.Interface.Repository;
namespace MisaWeb05.Infastructure.Repositories.Impl
{
    public  class BaseRepository<T> : IGenaralRepository<T> 
    {
        // biến lấy tên của bảng
        public string tableName;
        // biến lưu trữ két nối csdl
        public MySqlConnection conn;

        protected BaseRepository()
        {
            tableName = typeof(T).Name;
            conn= DBConnect.dbConnect();
        }

        /// <summary>
        /// hàm tổng quát lấy toàn bộ dữ liệu từ bảng
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllData()
        {
            try
            {
                var sqlQuery = "select * from " + tableName;

                // trả về dữ liệu về client
                return conn.Query<T>(sqlQuery).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            
        }


        /// <summary>
        /// hàm tổng quát lấy dữ liệu theo ID
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns></returns>
        public T GetById(Guid valueColumnId)
        {
            try
            {
                // TODO: đang làm đoạn này
                var sqlQuery = $"select * from   {tableName} where  {tableName}Id = @{tableName}Id ";
                var parameter = new DynamicParameters();
                parameter.Add($"@{tableName}Id", valueColumnId);
                // trả về dữ liệu về client
                return conn.QueryFirstOrDefault<T>(sqlQuery,parameter);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
           
        }

        /// <summary>
        /// hàm tổng quát thực hiện thêm 1 bản ghi từ bảng
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns></returns>
        public int InsertIntoTable(T t)
        {
            try
            {
                //tên proc
                var sqlQuery = $"Proc_Insert{tableName}";

                // excute để thực hiện khi thêm sửa 
                // parame là tham số kiểu object
                // phải khai báo là loại proc để sql biết đc và thực hiện truy vấn
                return conn.Execute(sqlQuery, param: t, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception)
            {
               
                throw;
            }
            finally{
                conn.Close();   
            }
          
        }

        /// <summary>
        /// hàm tổng quát thực hiện chỉnh sửa 1 bản ghi
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns></returns>
        public int UpdateTable( T t)
        {
            try
            {
                //tên proc
                var sqlQuery = $"Proc_Update{tableName}";

                // excute để thực hiện khi thêm sửa 
                // parame là tham số kiểu object
                // phải khai báo là loại proc để sql biết đc và thực hiện truy vấn
                return conn.Execute(sqlQuery, param: t, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// hàm tổng quát xóa 1 bản ghi từ table theo id
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns></returns>
        public int DeteleRecordById(Guid id)
        {
            try
            {
                var sqlQuery = $"Delete from {tableName} where {tableName}Id =@{tableName}Id";
                var parameter = new DynamicParameters();
                parameter.Add($"@{tableName}Id", id);
                // trả về dữ liệu về client
                return conn.Execute(sqlQuery,parameter);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// hàm tổng quát lấy dữ liệu theo pagenumber , paesize và employeeFilter(mã nhân viên, tên nhân viên)
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns></returns>
        public List<T> GetElementsWithNumberPage(int? limit,int? offset, string? employeeFilter)
        {
            try
            {
                var sqlQuery = $"select * from Employee e INNER JOIN Department d ON e.DepartmentId = d.DepartmentId  where EmployeeCode LIKE '%{employeeFilter}%' or EmployeeName LIKE '%{employeeFilter}%' ORDER BY e.CreatedDate DESC  limit {limit} offset {offset} ";
                var parameter = new DynamicParameters();
                parameter.Add("@employeeFilter", employeeFilter);
               
                // trả về dữ liệu về client
                return conn.Query<T>(sqlQuery).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// hàm tổng quát lấy mã nhân viên mới nhân viên
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns></returns>
        public T GetNearestEmployeeCodeByCreatedDate()
        {
            try
            {
                var sqlQuery = $"select EmployeeCode from  {tableName} e order by e.CreatedDate  DESC limit 1";
              

                // trả về dữ liệu về client
                return conn.QueryFirstOrDefault<T>(sqlQuery);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// hàm tổng quát lấy dữ liệu theo employeeFilter(mã nhân viên, tên nhân viên)
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns></returns>
        public List<T> GetElementsByFilter(string? employeeFilter)
        {
            try
            {
                var sqlQuery = $"select * from  {tableName} where {tableName}Code LIKE '%{employeeFilter}%' or {tableName}Name LIKE '%{employeeFilter}%' ";
                var parameter = new DynamicParameters();
                parameter.Add("@employeeFilter", employeeFilter);

                // trả về dữ liệu về client
                return conn.Query<T>(sqlQuery).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }



    }
}