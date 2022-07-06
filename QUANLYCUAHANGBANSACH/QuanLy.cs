using System;
using System.Collections;

namespace QUANLYCUAHANGBANSACH
{
    class QuanLy
    {
        private string maql, tenql, sdtql, diachiql;
        private bool gioitinh;

        public string Maql { get => maql; set => maql = value; }
        public string Tenql { get => tenql; set => tenql = value; }
        public string Sdtql { get => sdtql; set => sdtql = value; }
        public bool Gioitinh { get => gioitinh; set => gioitinh = value; }
        public QuanLy(string maql, string tenql, string sdtql, string diachiql, bool gioitinh)
        {
            this.maql = maql;
            this.tenql = tenql;
            this.sdtql = sdtql;
            this.diachiql = diachiql;
            this.gioitinh = gioitinh;
        }
        public override string ToString()
        {
            return "Mã quản lý: " + maql + " Tên quản lý: " + tenql + " Số điện thoại: " + sdtql + " Địa chỉ: " + diachiql + " Giới tính: " + (gioitinh ? "Nam" : "Nữ");
        }
        /*XEM GIAN HÀNG*/
        public void XemGianHang(ArrayList sp, ArrayList tl)
        {
            Console.WriteLine("------------------------------------------------GIAN HÀNG--------------------------------------------");
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
        /*THÊM MỘT SẢN PHẨM MỚI VÀO GIAN HÀNG*/
        public void ThemSanPhamVaoGianHang(ref ArrayList SP, ref ArrayList arr_masp, ref ArrayList TL, QuanLy quanly)
        {
            Console.WriteLine(" Nhập thông tin theo quy ước:Tên sản phẩm-Giá bán-Số lượng-Thể loại-Tác giả");
            SinhMaSP(ref arr_masp);
            string[] arr = ChuanHoaDuLieu(Console.ReadLine());
            bool kt = false;
            for (int i = 0; i < TL.Count; i++)
            {
                if (((TheLoai)TL[i]).Theloai == arr[3]) kt = true;
            }
            if (kt == false) //đã phát hiện thể loại mới
            {
                Console.WriteLine(" --->Hệ thống phát hiện thể loại mới, hoặc có thể bạn đã nhập sai:");
                Console.WriteLine("     3.1 Nhập lại thể loại | 3.2 Thêm thể loại");
                switch (Console.ReadLine())
                {
                    case "3.1":
                        string st; bool kt1 = false;
                        do
                        {
                            Console.Write("Nhập tên thể loại: ");
                            st = Console.ReadLine();
                            for (int i = 0; i < TL.Count; i++)
                            {
                                if (((TheLoai)TL[i]).Theloai == st) kt1 = true;
                            }
                            if (kt1 == false) Console.WriteLine("Lại sai nữa rồi, hãy nhập lại");
                        } while (kt1== false);
                        arr[3] = st;
                        break;
                    case "3.2":
                        quanly.ThemTheLoai(ref TL);
                        break;
                    default: break;
                }

            }
            SanPham sp_tmp = new SanPham(SinhMaSP(ref arr_masp).ToString(), arr[0], int.Parse(arr[1]), int.Parse(arr[2]), arr[3], arr[4]);
            SP.Add(sp_tmp);
            Console.WriteLine(" --->Thêm thành công: " + ((SanPham)sp_tmp).Tensp.ToString());
        }
        public int SinhMaSP(ref ArrayList arr_masp) //đây là một hàm sinh mảng ngẫu nhiên dùng khi quản lý thêm một sp mới
        {
            int rd;
            while (true)
            {
                rd = new Random().Next(1, 100);
                if (arr_masp.Contains(rd) == false)
                {
                    arr_masp.Add(rd);
                    return rd;
                }
            }
        }
        /*XÓA TOÀN BỘ MỘT SẢN PHẨM KHỎI GIAN HÀNG*/
        public void XoaSanPhamKhoiGianHang(ref ArrayList SP)
        {
            Console.Write(" Nhập mã sản phẩm cần xóa khỏi gian hàng, hoặc nhập 0 nếu không muốn xóa: ");
            string x = Console.ReadLine();
            if (x != "0")
            {
                int vitri = TimKiemMasp(x, SP);
                if (vitri != -1)
                {
                    Console.WriteLine(" Đã xóa sản phẩm: " + ((SanPham)SP[vitri]).Tensp);
                    SP.RemoveAt(vitri);
                }
                else Console.WriteLine("Không tìm thấy mã sản phẩm");
            }
        }
        /*CẬP NHẬT LẠI THÔNG TIN SẢN PHẨM NGOẠI TRỪ MÃ SẢN PHẨM*/
        public void CapNhatSanPhamTrongGianHang(ref ArrayList sp)
        {
            string text_masp; bool kt = false;
            do
            {
                Console.Write(" Mã sản phẩm cần cập nhật: ");
                text_masp = Console.ReadLine();
                if (TimKiemMasp(text_masp, sp) != -1) kt = true;
                if (kt == false) Console.WriteLine("Nhập sai, hãy nhập lại");
            } while (kt == false);
            SanPham sp_update = (SanPham)sp[TimKiemMasp(text_masp, sp)];
            Console.WriteLine(" Thông tin được phép cập nhật: 5.1 Tên sản phẩm | 5.2 Giá bán | 5.3 Số lượng | 5.4 Thể loại | 5.5 Tác giả | 5.0 Thoát khỏi chức năng cập nhật");
            string text_update = "", text1 = "";
            do
            {
                Console.Write(" Bạn muốn cập nhật: ");
                text_update = Console.ReadLine();
                switch (text_update)
                {
                    case "5.1":
                        Console.Write(" Sửa tên sản phẩm thành: ");
                        text1 = Console.ReadLine();
                        sp_update.Tensp = text1;
                        break;
                    case "5.2":
                        Console.Write(" Sửa giá bán thành: ");
                        text1 = Console.ReadLine();
                        sp_update.Giaban = int.Parse(text1);
                        break;
                    case "5.3":
                        Console.Write(" Sửa số lượng thành: ");
                        text1 = Console.ReadLine();
                        sp_update.Soluong = int.Parse(text1);
                        break;
                    case "5.4":
                        Console.Write(" Sửa thể loại thành: ");
                        text1 = Console.ReadLine();
                        sp_update.Theloai = text1;
                        break;
                    case "5.5":
                        Console.Write(" Sửa tác giả thành: ");
                        text1 = Console.ReadLine();
                        sp_update.Tacgia = text1;
                        break;
                    default: break;
                }
            } while (text_update != "5.0");
            Console.WriteLine(" Cập nhật thành công: " + sp_update.ToString());
        }
        /*THÊM THỂ LOẠI*/
        public void ThemTheLoai(ref ArrayList TL)
        {
            Console.WriteLine(" Nhập thông tin theo quy ước: Tên thể loại-Mô tả-Các sản phẩm tiêu biểu");
            string[] arr = ChuanHoaDuLieu(Console.ReadLine());
            TL.Add(new TheLoai(arr[0], arr[1], arr[2]));
            Console.WriteLine(" --->Đã thêm thể loại mới");
        }
        /*XEM DANH SÁCH KHÁCH HÀNG*/
        public void HienThiKhachHang(ArrayList kh)
        {
            Console.WriteLine("----------------------------------------------------Danh sách khách hàng-----------------------------------------------");
            for (int i = 0; i < kh.Count; i++)
            {
                Console.WriteLine("  " + kh[i].ToString());
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
        }
        /*XEM THỂ LOẠI*/
        public void HienThiTheLoai(ArrayList tl)
        {
            Console.WriteLine("-------------------------------------------------Các thể loại----------------------------------------------------------------------------------");
            for (int i = 0; i < tl.Count; i++)
            {
                Console.WriteLine("  " + tl[i].ToString());
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------");
        }
        public int TimKiemMasp(string x, ArrayList sp) //hàm tìm kiếm mã sản phẩm
        {
            for (int i = 0; i < sp.Count; i++)
            {
                if (((SanPham)sp[i]).Masp.Equals(x))
                    return i;
            }
            return -1;
        }
        public string[] ChuanHoaDuLieu(string long_st) //chuẩn hóa lại dữ liệu đọc từ màn hình console
        {
            long_st = long_st.Trim();
            while (long_st.IndexOf("  ") != -1)
            {
                long_st.Replace("  ", " ");
            }
            string[] sub_st = long_st.Split("-");
            return sub_st;
        }
    }

}
