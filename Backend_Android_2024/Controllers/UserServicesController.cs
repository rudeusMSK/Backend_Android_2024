using Backend_Android_2024.Models.DTOModel;
using Backend_Android_2024.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Backend_Android_2024.Models.ServicesDTOModel.Backend_Android_2024.Models.DTOModel;
using System.Diagnostics;
using System.Security.Principal;
using Newtonsoft.Json;
using Swashbuckle.Swagger;
using System.Web.Security;
using System.Web;
using Microsoft.Owin;
using Owin;

namespace Backend_Android_2024.Controllers
{
    public class UserServicesController : ApiController
    {
        private TestShoppingEntities db = new TestShoppingEntities();

        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> Login(LoginUser loginUser)
        {
            try
            {
                // Kiểm tra thông tin đăng nhập
                var user = db.NguoiDungs
                    .FirstOrDefault(
                    u => u.TenDangNhap == loginUser.UserName.Trim()
                    && u.MatKhau == loginUser.Password.Trim());

                if (user == null)
                {
                    return NotFound();
                }

                // Tạo cookie chứa thông tin người dùng
                var userInfo = JsonConvert.SerializeObject(user);
                var cookie = new HttpCookie("userInfo", userInfo);
                cookie.Expires = DateTime.Now.AddDays(1); // Thiết lập cookie có hiệu lực trong 1 ngày
                HttpContext.Current.Response.Cookies.Add(cookie);

                // Trả về đối tượng UserDTO
                return Ok(new UserDTO
                {
                    IDND = user.IDND,
                    TenDangNhap = user.TenDangNhap,
                    GioiTinh = user.GioiTinh,
                    Email = user.Email
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return InternalServerError();
            }
        }
    }
}