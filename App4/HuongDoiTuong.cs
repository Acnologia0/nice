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
            public int[] loais { get; set; }
            public int[] thanhphan { get; set; }
            public int[] dinhduong { get; set; }
            public SanPham() 
            {

            }
        }

        public class DonDatHang
        {
            public int ID { get; set; }
            public int[] sanpham { get; set; }
            public string note { get; set; }
            public bool dahoanthanh { get; set; }
            public DateTime ngaydathang { get; set; }
            public DateTime ngayhoanthanh { get; set; }
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
            public string Name
            {
                get; set;
            } 
        }    
    }
}
