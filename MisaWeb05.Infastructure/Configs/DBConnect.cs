using Dapper;
using MySqlConnector;
namespace Missa05.Configs
{

    public class DBConnect
    {
        public static MySqlConnection dbConnect()
        {
            try
            {
                //khai báo thông tin csdl
                var connection =
                    "Host=3.0.89.182;" +
                    " Port=3306;" +
                    "Database= MISA.WEB05.PVLINH_copy;" +
                    "User Id= dev;" +
                    "Password=12345678";
                    
                // khởi tạo kết nối
                var sqlConnection=new MySqlConnection(connection);

                // trả về dữ liệu về client 
                return sqlConnection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return null;
        }
    }
}
