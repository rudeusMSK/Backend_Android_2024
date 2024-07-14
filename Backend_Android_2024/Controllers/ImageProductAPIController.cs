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
    public class ImageProductAPIController : ApiController
    {
        private TestShoppingEntities db = new TestShoppingEntities();

        // GET: api/ImageProductAPI
        public IQueryable<ProductImageDTO> GetHinhAnhs()
        {
            return db.HinhAnhs.Select(
                hinh => new ProductImageDTO
                {
                    IDHinh = hinh.IDHinh,
                    // Url:
                    TenHinh = hinh.TenHinh,
                    IDSP = hinh.IDSP
                });
        }

        // GET: api/ImageProductAPI/5
        [ResponseType(typeof(HinhAnh))]
        public async Task<IHttpActionResult> GetHinhAnh(int id)
        {
            HinhAnh hinhAnh = await db.HinhAnhs.FindAsync(id);
            if (hinhAnh == null)
            {
                return NotFound();
            }

            return Ok(hinhAnh);
        }

        // PUT: api/ImageProductAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHinhAnh(int id, HinhAnh hinhAnh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hinhAnh.IDHinh)
            {
                return BadRequest();
            }

            db.Entry(hinhAnh).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HinhAnhExists(id))
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

        // POST: api/ImageProductAPI
        [ResponseType(typeof(HinhAnh))]
        public async Task<IHttpActionResult> PostHinhAnh(HinhAnh hinhAnh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HinhAnhs.Add(hinhAnh);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = hinhAnh.IDHinh }, hinhAnh);
        }

        // DELETE: api/ImageProductAPI/5
        [ResponseType(typeof(HinhAnh))]
        public async Task<IHttpActionResult> DeleteHinhAnh(int id)
        {
            HinhAnh hinhAnh = await db.HinhAnhs.FindAsync(id);
            if (hinhAnh == null)
            {
                return NotFound();
            }

            db.HinhAnhs.Remove(hinhAnh);
            await db.SaveChangesAsync();

            return Ok(hinhAnh);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HinhAnhExists(int id)
        {
            return db.HinhAnhs.Count(e => e.IDHinh == id) > 0;
        }
    }
}