//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace website_ban_dong_ho.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietDonHang
    {
        public int MaChiTietDH { get; set; }
        public Nullable<int> MaDDH { get; set; }
        public Nullable<int> MaSP { get; set; }
        public string TenSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> DonGia { get; set; }
    
        public virtual DonDatHang DonDatHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
