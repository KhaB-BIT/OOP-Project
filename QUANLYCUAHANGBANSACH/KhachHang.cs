using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace QUANLYCUAHANGBANSACH
{
    class KhachHang
    {
        private string makh, tenkh, sdtkh, diachikh;
        private bool gioitinh;
        private GioHang giohang;

        public string Makh { get => makh; set => makh = value; }
        public string Tenkh { get => tenkh; set => tenkh = value; }
        public string Sdtkh { get => sdtkh; set => sdtkh = value; }
        public string Diachikh { get => diachikh; set => diachikh = value; }
        internal GioHang Giohang { get => giohang; set => giohang = value; }

        public KhachHang(string makh, string tenkh, string sdtkh, string diachikh, bool gioitinh)
        {
            this.makh = makh;
            this.tenkh = tenkh;
            this.sdtkh = sdtkh;
            this.diachikh = diachikh;
            this.gioitinh = gioitinh;
        }
        
        public override string ToString()
        {
            return "Mã khách hàng: " + makh + "| Tên khách hàng: " + tenkh + "| Số điện thoại " + sdtkh + "| Địa chỉ " + diachikh+"| Giới tính: "+(gioitinh?"Nam":"Nữ");
        }
        /*TẠO GIỎ HÀNG*/
        public void TaoGioHang() 
        {
            giohang = new GioHang();
            Console.WriteLine(" --->Đã tạo giỏ hàng mới, mời bạn mua hàng");
        }
        /*MUA HÀNG*/
        public void MuaHang(KhachHang kh,ref ArrayList sp)
        {
            kh.Giohang.XuatHoaDon(kh,ref sp);
            kh.Giohang.Danhsachsanpham.Clear();
        }
        /*XEM GIAN HÀNG*/
        public void XemGianHang(ArrayList sp, ArrayList tl)
        {
            Console.WriteLine("-------------------------------------------GIAN HÀNG-------------------------------------------------");
            for (int i = 0; i < tl.Count; i++)
            {
                Console.WriteLine(" # THỂ LOẠI: " + ((TheLoai)tl[i]).Theloai);
                for (int j = 0; j < sp.Count; j++)
                {
                    if (((SanPham)sp[j]).Theloai.Equals(((TheLoai)tl[i]).Theloai))
                        Console.WriteLine("    " + sp[j].ToString());
                }
            }
        }
        /*TÌM KIẾM THÔNG TIN DỰA TRÊN TÊN SẢN PHẨM, THỂ LOẠI, TÁC GIẢ*/
        public void TimKiemKeyWord(ArrayList sp)
        {
            Console.Write(" Nhập từ khóa cần tìm kiếm: ");
            string x = Console.ReadLine();
            x = x.ToLower();
            int j = 0;
            for (int i = 0; i < sp.Count; i++)
            {
                SanPham sp_tmp = (SanPham)sp[i];
                string tensach = (sp_tmp.Tensp).ToLower();
                string tacgia = (sp_tmp.Tacgia).ToLower();
                string theloai = (sp_tmp.Theloai).ToLower();
                if ((tensach.IndexOf(x) != -1) || (tacgia.IndexOf(x) != -1) || (theloai.IndexOf(x) != -1))
                {
                    Console.WriteLine("    " + sp_tmp.ToString());
                    j++;
                }
            }
            if (j == 0) Console.WriteLine(" --->Không tìm thấy thông tin");
        }
        
    }
}
