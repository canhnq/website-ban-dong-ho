using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using website_ban_dong_ho.Models;

namespace website_ban_dong_ho.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        QLCHDongHoEntities db = new QLCHDongHoEntities();
        
        public List<ItemGioHang> LayGioHang()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int MaSP, string strURL)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> lstGioHang = LayGioHang();

            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck != null)
            {
                if (sp.SoLuongTon < spCheck.SoLuong)
                {
                    return View("ThongBao");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(strURL);
            }
            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            lstGioHang.Add(itemGH);
            return Redirect(strURL);
        }

        public ActionResult GioHang()
        {
            List<ItemGioHang> lstGioHang = LayGioHang();

            return View(lstGioHang);
        }

        public ActionResult SuaGioHang(int MaSP)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("index", "Home");
            }

            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);

            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.GioHang = lstGioHang;
            return View(spCheck);
        }

        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {
            SanPham spCheck = db.SanPhams.Single(n => n.MaSP == itemGH.MaSP);
            if (spCheck.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            List<ItemGioHang> lstGH = LayGioHang();
            ItemGioHang itemGHUpdate = lstGH.Find(n => n.MaSP == itemGH.MaSP);
            itemGHUpdate.SoLuong = itemGH.SoLuong;
            itemGHUpdate.ThanhTien = itemGHUpdate.SoLuong * itemGHUpdate.DonGia;
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGioHang(int MaSP)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("index", "Home");
            }

            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);

            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            lstGioHang.Remove(spCheck);

            return RedirectToAction("GioHang");
        }

        [HttpPost]
        public ActionResult DatHang(KhachHang kh)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("index", "Home");
            }
            KhachHang khachHang = new KhachHang();
            if (Session["TaiKhoan"] == null)
            {
                khachHang = kh;
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
            }
            else
            {
                ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
                khachHang.TenKH = tv.HoTen;
                khachHang.DiaChi = tv.DiaChi;
                khachHang.Email = tv.Email;
                khachHang.SoDienThoai = tv.SoDienThoai;
                khachHang.MaThanhVien = tv.MaThanhVien;
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
            }

            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = int.Parse(khachHang.MaKH.ToString());
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.UuDai = 0;
            ddh.DaXoa = false;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            List<ItemGioHang> lstGH = LayGioHang();
            foreach (var item in lstGH)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.MaSP = item.MaSP;
                ctdh.TenSP = item.TenSP;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia = item.DonGia;
                db.ChiTietDonHangs.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("GioHang");
        }
    }
}