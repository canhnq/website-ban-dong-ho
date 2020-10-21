using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using website_ban_dong_ho.Models;
using CaptchaMvc;
using CaptchaMvc.HtmlHelpers;

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

        public ActionResult DanhSachSanPham(int maNSX,int maLoaiSP)
        {
            var lstSP = db.SanPhams.Where(n => n.MaNSX == maNSX && n.MaLoaiSP == maLoaiSP && n.DaXoa == false).ToList();
            ViewBag.lstSP = lstSP;

            return View();
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
    }
}