using De1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace De1.Controllers
{
    public class HomeController : Controller
    {
        private QLBanHangQuanAoEntities db = new QLBanHangQuanAoEntities();
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.PhanLoai).Include(s => s.PhanLoaiPhu);
            ViewBag.PhanLoai = new SelectList(db.PhanLoais,"MaPhanLoai","PhanLoaiChinh");
            return View(sanPhams.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ThemSanPham()
        {
            ViewBag.MaPhanLoai = new SelectList(db.PhanLoais, "MaPhanLoai", "PhanLoaiChinh");
            ViewBag.MaPhanLoaiPhu = new SelectList(db.PhanLoaiPhus, "MaPhanLoaiPhu", "TenPhanLoaiPhu");
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSanPham([Bind(Include = "MaSanPham,TenSanPham,MaPhanLoai,GiaNhap,DonGiaBanNhoNhat,DonGiaBanLonNhat,TrangThai,MoTaNgan,AnhDaiDien,NoiBat,MaPhanLoaiPhu")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPhanLoai = new SelectList(db.PhanLoais, "MaPhanLoai", "PhanLoaiChinh", sanPham.MaPhanLoai);
            ViewBag.MaPhanLoaiPhu = new SelectList(db.PhanLoaiPhus, "MaPhanLoaiPhu", "TenPhanLoaiPhu", sanPham.MaPhanLoaiPhu);
            return View(sanPham);
        }

        public ActionResult GetSanPhams(string filterValue)
        {
            List<SanPham> listSanPham = db.SanPhams.Where(a => a.MaPhanLoai.Contains(filterValue)).ToList();

            return PartialView("_PartialSanPham", listSanPham);
        }
    }
}