using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using website_ban_dong_ho.Models;
using CaptchaMvc;
using CaptchaMvc.HtmlHelpers;
using PagedList;
using System.Net;

namespace website_ban_dong_ho.Controllers
{
    public class PartialController : Controller
    {
        QLCHDongHoEntities db = new QLCHDongHoEntities();
        public ActionResult MenuPartial()
        {
            var lstSP = db.SanPhams;

            return PartialView(lstSP);
        }

        public ActionResult DanhSachSanPham(int? MaLoaiSP, int? MaNSX, int? page)
        {
            if (MaLoaiSP == null || MaNSX == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSP = db.SanPhams.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaNSX && n.DaXoa == false);
            if (lstSP.Count() == 0)
            {
                return HttpNotFound();
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);

            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;

            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(ThanhVien tv)
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                ViewBag.ThongBao = "Đăng ký thành công!!!";
                db.ThanhViens.Add(tv);
                db.SaveChanges();
                return View();
            }

            ViewBag.ThongBao = "Sai mã Captcha!!!";

            return View();
        }

        public List<string> LoadCauHoi()
        {
            List<string> lstCauHoi = new List<string>();
            lstCauHoi.Add("Con vật mà bạn yêu thích?");
            lstCauHoi.Add("Ca sĩ mà bạn yêu thích?");
            lstCauHoi.Add("Nghề nghiệp của bạn là gì?");
            return lstCauHoi;
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f["txtMatKhau"].ToString();

            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);

            if (tv != null)
            {
                Session["TaiKhoan"] = tv;
                return RedirectToAction("Index","Home");
            }
            ViewBag.ThongBao = "Sai tên đăng nhập hoặc tài khoản!!!";
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}