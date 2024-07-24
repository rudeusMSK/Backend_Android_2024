using System.Data;
using System.Linq;
using System.Web.Http;
using Backend_Android_2024.Models;
using Backend_Android_2024.Models.ServicesDTOModel.Backend_Android_2024.Models.DTOModel;
using System.Diagnostics;
using System;

namespace Backend_Android_2024.Controllers
{
    public class ProductServicesController : ApiController
    {
        private TestShoppingEntities db = new TestShoppingEntities();

        /// <summary>
        /// Get list Card Item for home page Flutter 
        /// </summary>
        /// <returns></returns>
        // GET: api/ProductServices
        [HttpGet]
        [Route("api/ProductServices")]
        public IHttpActionResult GetProductCart()
        {
            try
            {
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
                    }).ToList();

                return Ok(query);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetProductCart: {ex.Message}");
                return InternalServerError();
            }
        }

        private bool SanPhamExists(int id)
        {
            return db.SanPhams.Count(e => e.IDSP == id) > 0;
        }
    }
}