using Backend_Android_2024.Models.ServicesDTOModel.Backend_Android_2024.Models.DTOModel;
using Backend_Android_2024.Models;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Backend_Android_2024.Controllers
{
    public class ProductServicesController : ApiController
    {
        private TestShoppingEntities db = new TestShoppingEntities();

        /// <summary>
        /// Lấy danh sách các sản phẩm cho trang chính Flutter 
        /// </summary>
        /// <returns></returns>
        // GET: api/ProductServices
        public IHttpActionResult GetProductCart()
        {
            // Lấy tất cả các cookie từ header
            var cookies = Request.Headers.GetCookies("userInfo");

            // Kiểm tra xem có bất kỳ cookie nào không
            if (cookies.Count == 0 || !cookies.Any(cookie => cookie.Cookies.Any(c => c.Name == "userInfo")))
            {
                return Unauthorized(); // Trả về phản hồi 401 Unauthorized
            }

            var query = db.SanPhams
                .Select(sp => new ProductCartItemDTO
                {
                    IDSP = sp.IDSP,
                    TenSP = sp.TenSP,
                    Img_Url = db.HinhAnhs
                                .Where(i => i.IDSP == sp.IDSP)
                                .Select(i => i.TenHinh)
                                .FirstOrDefault(),
                    GiaBan = db.ChiTiet_SP
                                .Where(ct => ct.IDSP == sp.IDSP)
                                .Select(ct => ct.GiaBan)
                                .FirstOrDefault(),
                });

            return Ok(query); // Trả về phản hồi 200 OK với kết quả truy vấn
        }

        private bool SanPhamExists(int id)
        {
            return db.SanPhams.Count(e => e.IDSP == id) > 0;
        }
    }
}
