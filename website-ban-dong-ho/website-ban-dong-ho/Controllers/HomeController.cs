using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using website_ban_dong_ho.Models;

namespace website_ban_dong_ho.Controllers
{  
    public class HomeController : Controller
    {
        QLCHDongHoEntities db = new QLCHDongHoEntities();
        public ActionResult Index()
        {
            var lstDHNamM = db.SanPhams.Where(n => n.MaLoaiSP == 1 && n.Moi == 1 && n.DaXoa == false).ToList();
            ViewBag.lstDHNamM = lstDHNamM;
            var lstDHNuM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1 && n.DaXoa == false).ToList();
            ViewBag.lstDHNuM = lstDHNuM;
            var lstDHTeM = db.SanPhams.Where(n => n.MaLoaiSP == 3 && n.Moi == 1 && n.DaXoa == false).ToList();
            ViewBag.lstDHTeM = lstDHTeM;
           
            return View();
        }
        
    }
}