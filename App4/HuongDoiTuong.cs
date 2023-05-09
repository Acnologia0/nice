using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App4
{
    public class HuongDoiTuong
    {
        public class DoanhThu
        {
            public int TongThu { get; set; }
            public int TongChi { get; set; }
            public int TienLoi => TongThu - TongChi;
        }
        public class SanPham
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public float GiaBan { get; set; }
            public float GiaNhap { get; set; }
            public float TienLoi => GiaBan - GiaNhap;
            public int[] Loai { get; set; }
            public int[] ThanhPhan { get; set; }
            public int[] DinhDuong { get; set; }
            public SanPham() 
            {

            }
        }

        public class DonDatHang
        {
            public int ID { get; set; }
            public int[] SanPham { get; set; }
            public string GhiChu { get; set; }
            public bool DaHoanThanh { get; set; }
            public DateTime NgayDat { get; set; }
            public DateTime NgayHoanThanh { get; set; }
        }
        public class KhachHang
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string SDT { get; set; }
            public int DonDatHang { get; set; }
        }
        public class Loai
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class ThanhPhan
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        public class DinhDuong
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }    
    }
}
