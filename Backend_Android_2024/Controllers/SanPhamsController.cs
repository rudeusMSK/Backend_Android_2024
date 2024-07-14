using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backend_Android_2024.Models;

namespace Backend_Android_2024.Controllers
{
    public class SanPhamsController : Controller
    {
        private TestShoppingEntities db = new TestShoppingEntities();

        // GET: SanPhams
        public async Task<ActionResult> Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiSP).Include(s => s.MauSac).Include(s => s.NhanVien).Include(s => s.ThuongHieu).Include(s => s.TrangThai);
            return View(await sanPhams.ToListAsync());
        }

        // GET: SanPhams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = await db.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.IDLoai = new SelectList(db.LoaiSPs, "IDLoai", "TenLoai");
            ViewBag.IDMau = new SelectList(db.MauSacs, "IDMau", "TenMau");
            ViewBag.IDNV = new SelectList(db.NhanViens, "IDNV", "TenNV");
            ViewBag.IDThuongHieu = new SelectList(db.ThuongHieux, "IDThuongHieu", "TenThuongHieu");
            ViewBag.IDTrangThai = new SelectList(db.TrangThais, "IDTrangThai", "TenTrangThaI");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TenSP,IDThuongHieu,IDLoai,IDMau,IDTrangThai,IDNV")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDLoai = new SelectList(db.LoaiSPs, "IDLoai", "TenLoai", sanPham.IDLoai);
            ViewBag.IDMau = new SelectList(db.MauSacs, "IDMau", "TenMau", sanPham.IDMau);
            ViewBag.IDNV = new SelectList(db.NhanViens, "IDNV", "TenNV", sanPham.IDNV);
            ViewBag.IDThuongHieu = new SelectList(db.ThuongHieux, "IDThuongHieu", "TenThuongHieu", sanPham.IDThuongHieu);
            ViewBag.IDTrangThai = new SelectList(db.TrangThais, "IDTrangThai", "TenTrangThaI", sanPham.IDTrangThai);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = await db.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLoai = new SelectList(db.LoaiSPs, "IDLoai", "TenLoai", sanPham.IDLoai);
            ViewBag.IDMau = new SelectList(db.MauSacs, "IDMau", "TenMau", sanPham.IDMau);
            ViewBag.IDNV = new SelectList(db.NhanViens, "IDNV", "TenNV", sanPham.IDNV);
            ViewBag.IDThuongHieu = new SelectList(db.ThuongHieux, "IDThuongHieu", "TenThuongHieu", sanPham.IDThuongHieu);
            ViewBag.IDTrangThai = new SelectList(db.TrangThais, "IDTrangThai", "TenTrangThaI", sanPham.IDTrangThai);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDSP,TenSP,IDThuongHieu,IDLoai,IDMau,IDTrangThai,IDNV")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDLoai = new SelectList(db.LoaiSPs, "IDLoai", "TenLoai", sanPham.IDLoai);
            ViewBag.IDMau = new SelectList(db.MauSacs, "IDMau", "TenMau", sanPham.IDMau);
            ViewBag.IDNV = new SelectList(db.NhanViens, "IDNV", "TenNV", sanPham.IDNV);
            ViewBag.IDThuongHieu = new SelectList(db.ThuongHieux, "IDThuongHieu", "TenThuongHieu", sanPham.IDThuongHieu);
            ViewBag.IDTrangThai = new SelectList(db.TrangThais, "IDTrangThai", "TenTrangThaI", sanPham.IDTrangThai);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = await db.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SanPham sanPham = await db.SanPhams.FindAsync(id);
            db.SanPhams.Remove(sanPham);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
