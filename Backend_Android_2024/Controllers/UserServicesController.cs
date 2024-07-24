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
using System.Security.Cryptography;
using System.Text;

namespace Backend_Android_2024.Controllers
{

    public class UserServicesController : ApiController
    {
        private TestShoppingEntities db = new TestShoppingEntities();


        [ResponseType(typeof(CookieUser))]
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


                var userInfo = JsonConvert.SerializeObject(user);
                var hashedUserInfo = HashString(userInfo);
                var cookie = new HttpCookie("userInfo", hashedUserInfo);
                cookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.Cookies.Add(cookie);

                // Trả về đối tượng UserDTO
                return Ok(new CookieUser
                {
                    UserID = user.IDND,
                    Token = hashedUserInfo
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return InternalServerError();
            }
        }
        private string HashString(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder();
                foreach (var @byte in bytes)
                {
                    builder.Append(@byte.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

