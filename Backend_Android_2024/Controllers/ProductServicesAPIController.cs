using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Backend_Android_2024.Models;
using Backend_Android_2024.Models.DTOModel;
using Backend_Android_2024.Models.ServicesDTOModel.Backend_Android_2024.Models.DTOModel;

namespace Backend_Android_2024.Controllers
{
    public class ProductServicesController : ApiController
    {
        private TestShoppingEntities db = new TestShoppingEntities();

        /// <summary>
        /// Get list Card Item for home page Flutter 
        /// </summary>
        /// <returns></returns>
        // GET: api/ServicesAPI
        public IQueryable<ProductCartItemDTO> GetProductCart()
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
                    });

            return query;
        }

        private bool SanPhamExists(int id)
        {
            return db.SanPhams.Count(e => e.IDSP == id) > 0;
        }
    }
}