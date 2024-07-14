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

namespace Backend_Android_2024.Controllers
{
    public class ProductAPIController : ApiController
    {
        private TestShoppingEntities db = new TestShoppingEntities();

        // GET: api/ProductAPI
        public IQueryable<ProductDTO> GetSanPhams()
        {
            return db.SanPhams.Select(sp => new ProductDTO
            {
                IDSP = sp.IDSP,
                TenSP = sp.TenSP,
                IDThuongHieu = sp.IDThuongHieu,
                IDLoai = sp.IDLoai,
                IDMau = sp.IDMau,
                IDTrangThai = sp.IDTrangThai,
                IDNV  = sp.IDNV
            });
        }

        // GET: api/ProductAPI/5
        [ResponseType(typeof(ProductDTO))]
        public async Task<IHttpActionResult> GetSanPham(int id)
        {
            var sanPham = await db.SanPhams
                .Where(sp => sp.IDSP == id)
                .Select(sp => new ProductDTO
                {
                    IDSP = sp.IDSP,
                    TenSP = sp.TenSP,
                    // Chọn các thuộc tính cần thiết khác
                })
                .FirstOrDefaultAsync();

            if (sanPham == null)
            {
                return NotFound();
            }

            return Ok(sanPham);
        }

        // PUT: api/ProductAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSanPham(int id, ProductDTO sanPhamDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sanPhamDTO.IDSP)
            {
                return BadRequest();
            }

            var sanPham = await db.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            sanPham.TenSP = sanPhamDTO.TenSP;
            // Cập nhật các thuộc tính khác

            db.Entry(sanPham).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanPhamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //// POST: api/ProductAPI
        //[ResponseType(typeof(ProductDTO))]
        //public async Task<IHttpActionResult> PostSanPham(ProductDTO productDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var sanPham = new SanPham
        //    {
        //        IDSP = productDTO.IDSP,
        //        TenSP = productDTO.TenSP,
        //        // Chuyển đổi các thuộc tính khác
        //    };

        //    db.SanPhams.Add(sanPham);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (SanPhamExists(sanPham.IDSP))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = sanPham.IDSP }, productDTO);
        //}

        // POST: api/ProductAPI
        [ResponseType(typeof(ProductDTO))]
        public async Task<IHttpActionResult> PostProduct(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to entity
            SanPham sanPham = new SanPham
            {
                TenSP = productDTO.TenSP,
                IDThuongHieu = productDTO.IDThuongHieu,
                IDLoai = productDTO.IDLoai,
                IDMau = productDTO.IDMau,
                IDTrangThai = productDTO.IDTrangThai,
                IDNV = productDTO.IDNV
            };

            db.SanPhams.Add(sanPham);
            await db.SaveChangesAsync();

            // Return the created entity
            return CreatedAtRoute("DefaultApi", new { id = sanPham.IDSP }, productDTO);
        }


        // DELETE: api/ProductAPI/5
        [ResponseType(typeof(ProductDTO))]
        public async Task<IHttpActionResult> DeleteSanPham(int id)
        {
            var sanPham = await db.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            db.SanPhams.Remove(sanPham);
            await db.SaveChangesAsync();

            return Ok(new ProductDTO
            {
                IDSP = sanPham.IDSP,
                TenSP = sanPham.TenSP,
                // Chuyển đổi các thuộc tính khác
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SanPhamExists(int id)
        {
            return db.SanPhams.Count(e => e.IDSP == id) > 0;
        }
    }
}