using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MisaWeb05.api.Controllers
{
    /// <summary>
    /// Lớp base controller
    /// CreatedBy: Nguyễn Văn Đông (23/06/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
       /* public  IActionResult GetData()
        {
            try
            {
                var a = "";
                if (string.IsNullOrEmpty(a))
                {
                    var resService = new
                    { 
                        userMsg ="Tên phòng ban không được phép bỏ trống!"
                    };
                    return BadRequest(resService);
                }
                var x = 0;
                return StatusCode(201,1);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ Misa để trợ giúp"
                };
                return StatusCode(500,res);
               
            }
        }*/
        
    }
}
