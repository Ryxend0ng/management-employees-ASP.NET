using Dapper;
using MisaWeb05.Core.Entity;
using MisaWeb05.Core.Interface.Repository;

namespace MisaWeb05.Infastructure.Repositories.Impl
{
    public class EmployeesRepository: BaseRepository<Employee>,IEmployeesRepository
    {

        /// <summary>
        /// hàm tổng quát lấy toàn bộ dữ liệu từ bảng employee
        /// createdBy: Nguyễn Văn Đông (23/06/2022)
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAllDataEmployee()
        {
            try
            {
                var sqlQuery = "Proc_GetDataEmployee";

                // trả về dữ liệu về client
                return conn.Query<Employee>(sqlQuery, commandType: System.Data.CommandType.StoredProcedure).ToList();
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

        public int ImportEmployee(IEnumerable<Employee> employees)
        {
            try
            {
                var sqlQuery = "Proc_InsertEmployee";
                var empInsert = 0;
                foreach(var employee in employees)
                {
                    var rowInsert = conn.Execute(sqlQuery, commandType: System.Data.CommandType.StoredProcedure);
                    if(rowInsert != null)
                    {
                        empInsert++;
                    }
                }
                // trả về dữ liệu về client
                return empInsert;
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
