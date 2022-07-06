using System;
using System.Collections.Generic;
using System.Text;

namespace QUANLYCUAHANGBANSACH
{
    class SanPham:ICloneable
    {
        private string masp, tensp, theloai, tacgia;
        private int soluong, giaban;

        public string Masp { get => masp; set => masp = value; }
        public string Tensp { get => tensp; set => tensp = value; }
        public string Theloai { get => theloai; set => theloai = value; }
        public string Tacgia { get => tacgia; set => tacgia = value; }
        public int Soluong { get => soluong; set => soluong = value; }
        public int Giaban { get => giaban; set => giaban = value; }

        public SanPham(string masp, string tensp, int giaban, int soluong, string theloai, string tacgia)
        {
            this.masp = masp;
            this.tensp = tensp;
            this.giaban = giaban;
            this.soluong = soluong;
            this.theloai = theloai;
            this.tacgia = tacgia;
        }
        
        public override string ToString()
        {
            return "Mã sản phẩm: " + masp + "| Tên sản phẩm: " + tensp + "| Giá bán: " + Giaban + "| Số lượng: "
                + Soluong + "| Tên thể loại: " + theloai + "| Tác giả: " + tacgia;
        }

        public object Clone()
        {
            return new SanPham(masp, tensp, giaban, soluong, theloai, tacgia);
        }
       
    }
}
