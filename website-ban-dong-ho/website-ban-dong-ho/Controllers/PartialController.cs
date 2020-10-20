using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using website_ban_dong_ho.Models;

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
    }
}