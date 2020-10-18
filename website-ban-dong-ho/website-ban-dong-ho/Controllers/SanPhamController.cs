using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using website_ban_dong_ho.Models;

namespace website_ban_dong_ho.Controllers
{
    public class SanPhamController : Controller
    {
        QLCHDongHoEntities db = new QLCHDongHoEntities();
        // GET: SanPham
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult XemChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id && n.DaXoa == false);
            if (sp == null)
            {
                return HttpNotFound();
            }

            return View(sp);
        }
    }
}