using System;
using System.Collections;
using System.IO;

namespace QUANLYCUAHANGBANSACH
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            ArrayList SP = new ArrayList(); //gian hàng
            ArrayList TL = new ArrayList(); //danh sách thể loại
            ArrayList KH = new ArrayList(); //danh sách khách hàng
            ArrayList QL = new ArrayList(); //danh sách quản lý
            ArrayList arr_masp = new ArrayList(); //lưu lại các mã sản phẩm, để khi thêm sản phẩm mới không bị trùng
            LoadTheLoai(ref TL);
            LoadKhachHang(ref KH);
            LoadQuanLy(ref QL);
            LoadGianHang(ref SP, ref arr_masp);
            HeThongCuaHang(ref SP, ref KH, ref QL, ref TL, ref arr_masp);
        }
        private static void HeThongCuaHang(ref ArrayList SP, ref ArrayList KH, ref ArrayList QL, ref ArrayList TL, ref ArrayList arr_masp)
        {
            Console.WriteLine(" Chào mừng đế với cửa hàng bán sách KT");
            Console.WriteLine(" 1.Khách hàng | 2.Quản lý cửa hàng");
            Console.Write(" Bạn là: ");
            string text = Console.ReadLine();
            if (text == "1") // NẾU LÀ KHÁCH HÀNG
            {
                KhachHang khachdangmua = (KhachHang)KH[0]; /*giả sử khách hàng này mua hàng trên hệ thống*/
                DangNhapTaiKhoan(text); // Tên đăng nhập:"khachhang" và mật khẩu:"12345" tự quy định để mô phỏng quá trình đăng nhập    
                do
                {
                    NhungChucNangDanhChoKhachHang();
                    khachdangmua.XemGianHang(SP, TL);
                    Console.WriteLine();
                    Console.Write("  Chọn chức năng: ");
                    text = Console.ReadLine();
                    switch (text)
                    {

                        case "1":
                            khachdangmua.TimKiemKeyWord(SP);
                            break;
                        case "2": khachdangmua.TaoGioHang(); break;
                        case "3":
                            khachdangmua.Giohang.XoaGioHang();
                            break;
                        case "4": khachdangmua.Giohang.ThemSPVaoGioHang(SP); break;
                        case "5":
                            khachdangmua.Giohang.XemGioHang();
                            khachdangmua.Giohang.XoaSPKhoiGioHang(); break;
                        case "6":
                            khachdangmua.Giohang.XemGioHang();
                            khachdangmua.Giohang.CapNhatGioHang(SP); break;
                        case "7": khachdangmua.Giohang.XemGioHang(); break;
                        case "8":
                            khachdangmua.MuaHang(khachdangmua, ref SP);
                            GhiDuLieuSP(SP);
                            break;
                        default: break;
                    }
                    if (text != "0")
                    {
                        Console.WriteLine();
                        Console.Write(" Enter để clear màn hình và tiếp tục "); Console.ReadLine();
                        Console.Clear();
                    }
                } while (text != "0");
                Console.WriteLine(" -->Đã đăng xuất, cảm ơn bạn đã sử dụng hệ thống <3");
            }

            if (text == "2") //NẾU LÀ QUẢN LÝ
            {
                QuanLy quanly = (QuanLy)QL[0]; /*giả sử quản lý này đang dùng hệ thống*/
                DangNhapTaiKhoan(text); //// Tên đăng nhập:"quanly" và mật khẩu:"56789" tự quy định để mô phỏng quá trình đăng nhập
                do
                {
                    NhungChucNangDanhChoQuanLy();
                    quanly.XemGianHang(SP, TL);
                    Console.WriteLine();
                    Console.Write(" Chọn chức năng: ");
                    text = Console.ReadLine();
                    switch (text)
                    {
                        case "1": quanly.TimKiemKeyWord(SP); break;
                        case "2":
                            quanly.ThemSanPhamVaoGianHang(ref SP, ref arr_masp, ref TL, quanly);
                            GhiDuLieuSP(SP); GhiDuLieuTL(TL); break;
                        case "3":
                            quanly.XoaSanPhamKhoiGianHang(ref SP);
                            GhiDuLieuSP(SP); break;
                        case "4":
                            quanly.CapNhatSanPhamTrongGianHang(ref SP);
                            GhiDuLieuSP(SP); break;
                        case "5":
                            quanly.ThemTheLoai(ref TL);
                            GhiDuLieuTL(TL); break;
                        case "6":quanly.HienThiKhachHang(KH); break;
                        case "7":quanly.HienThiTheLoai(TL); break;
                        default: break;
                    }
                    if (text != "0")
                    {
                        Console.WriteLine();
                        Console.Write(" Enter để clear màn hình và tiếp tục "); Console.ReadLine();
                        Console.Clear();
                    }
                } while (text != "0");
                Console.WriteLine(" --->Đã đăng xuất!");
            }
        }

        private static void NhungChucNangDanhChoQuanLy()
        {
            Console.WriteLine(" Những chức năng dành cho bạn:");
            Console.WriteLine(" 1.Tìm kiếm sản phẩm");
            Console.WriteLine(" 2.Thêm sản phẩm vào trong gian hàng");
            Console.WriteLine(" 3.Xóa sản phẩm khỏi gian hàng");
            Console.WriteLine(" 4.Cập nhật sản phẩm");
            Console.WriteLine(" 5.Thêm thể loại sách");
            Console.WriteLine(" 6.Xem danh sách khách hàng");
            Console.WriteLine(" 7.Xem thể loại");
            Console.WriteLine(" 0.Đăng xuất!");
        }
        private static void NhungChucNangDanhChoKhachHang()
        {
            Console.WriteLine(" Những chức năng dành cho bạn:");
            Console.WriteLine(" 1.Tìm kiếm sản phẩm");
            Console.WriteLine(" 2.Tạo giỏ hàng");
            Console.WriteLine(" 3.Xóa giỏ hàng");
            Console.WriteLine(" 4.Thêm sản phẩm vào giỏ hàng");
            Console.WriteLine(" 5.Xóa sản phẩm khỏi giỏ hàng");
            Console.WriteLine(" 6.Cập nhật giỏ hàng");
            Console.WriteLine(" 7.Xem giỏ hàng của bạn");
            Console.WriteLine(" 8.Mua hàng");
            Console.WriteLine(" 0.Đăng xuất!");
        }
        private static string[] ChuanHoaDuLieu(string long_st) // loại bỏ khoảng trắng đầu, cuối chuỗi, cắt chuỗi giữa cặp "-" và lưu vào mảng
        {
            long_st = long_st.Trim();
            while (long_st.IndexOf("  ") != -1)
            {
                long_st.Replace("  ", " ");
            }
            string[] sub_st = long_st.Split("-");
            return sub_st;
        }
        private static void DangNhapTaiKhoan(string text)
        {
            string tdn = "", mk = "";
            if (text == "1")
                do
                {
                    Console.Write(" Tên đăng nhập: ");
                    tdn = Console.ReadLine();
                    Console.Write(" Mật khẩu: ");
                    mk = Console.ReadLine();
                    if (tdn != "khachhang" || mk != "12345") Console.WriteLine("-->Nhập sai, mời nhập lại!");
                } while (tdn != "khachhang" || mk != "12345");
            if (text == "2")
                do
                {
                    Console.Write(" Tên đăng nhập: ");
                    tdn = Console.ReadLine();
                    Console.Write(" Mật khẩu: ");
                    mk = Console.ReadLine();
                    if (tdn != "quanly" || mk != "56789") Console.WriteLine("-->Nhập sai, mời nhập lại!");
                } while (tdn != "quanly" || mk != "56789");
            Console.Clear();
            Console.WriteLine("-->Đăng nhập thành công");
            Console.WriteLine(" ==========================================================================");
        }
        /*GHI DỮ LIỆU VÀO FILE SP*/
        static void GhiDuLieuSP(ArrayList sp)
        {
            string filepath = @"C:\Users\PC_LENOVO\Desktop\SP.txt";
            System.IO.FileStream sfs = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(sfs);
            for (int i = 0; i < sp.Count; i++)
            {
                sw.WriteLine(((SanPham)sp[i]).Masp + "-" + ((SanPham)sp[i]).Tensp + "-" + ((SanPham)sp[i]).Giaban + "-" + ((SanPham)sp[i]).Soluong + "-" + ((SanPham)sp[i]).Theloai + "-" + ((SanPham)sp[i]).Tacgia);
            }
            sw.Flush();
            sw.Close();
        }
        /*GHI DỮ LIỆU VÀO FILE TL*/
        static void GhiDuLieuTL(ArrayList tl)
        {
            string filepath = @"C:\Users\PC_LENOVO\Desktop\TL.txt";
            System.IO.FileStream sfs = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(sfs);
            for (int i = 0; i < tl.Count; i++)
            {
                sw.WriteLine(((TheLoai)tl[i]).Theloai + "-" + ((TheLoai)tl[i]).Mota + "-" + ((TheLoai)tl[i]).Sachnoibat);
            }
            sw.Flush();
            sw.Close();
        }
        private static void LoadGianHang(ref ArrayList sP, ref ArrayList arr_masp)
        {
            string filepath = @"C:\Users\PC_LENOVO\Desktop\SP.txt";
            StreamReader sr = File.OpenText(filepath);
            string input;
            input = sr.ReadLine();
            string[] arr = ChuanHoaDuLieu(input);
            sP.Add(new SanPham(arr[0], arr[1], int.Parse(arr[2]), int.Parse(arr[3]), arr[4], arr[5]));
            arr_masp.Add(int.Parse(arr[0]));
            while (true)
            {
                input = sr.ReadLine();
                if (input == null) break;
                arr = ChuanHoaDuLieu(input);
                sP.Add(new SanPham(arr[0], arr[1], int.Parse(arr[2]), int.Parse(arr[3]), arr[4], arr[5]));
                arr_masp.Add(int.Parse(arr[0]));
            }
            sr.Close();
        }

        private static void LoadQuanLy(ref ArrayList qL)
        {
            string filepath = @"C:\Users\PC_LENOVO\Desktop\QL.txt";
            StreamReader sr = File.OpenText(filepath);
            string input;
            input = sr.ReadLine();
            string[] arr = ChuanHoaDuLieu(input);
            qL.Add(new QuanLy(arr[0], arr[1], arr[2], arr[3], bool.Parse(arr[4])));
            while (true)
            {
                input = sr.ReadLine();
                if (input == null) break;
                arr = ChuanHoaDuLieu(input);
                qL.Add(new QuanLy(arr[0], arr[1], arr[2], arr[3], bool.Parse(arr[4])));
            }
            sr.Close();
        }

        private static void LoadKhachHang(ref ArrayList kH)
        {
            string filepath = @"C:\Users\PC_LENOVO\Desktop\KH.txt";
            StreamReader sr = File.OpenText(filepath);
            string input;
            input = sr.ReadLine();
            string[] arr = ChuanHoaDuLieu(input);
            kH.Add(new KhachHang(arr[0], arr[1], arr[2], arr[3], bool.Parse(arr[4])));
            while (true)
            {
                input = sr.ReadLine();
                if (input == null) break;
                arr = ChuanHoaDuLieu(input);
                kH.Add(new KhachHang(arr[0], arr[1], arr[2], arr[3], bool.Parse(arr[4])));
            }
            sr.Close();
        }

        private static void LoadTheLoai(ref ArrayList tL)
        {
            string filepath = @"C:\Users\PC_LENOVO\Desktop\TL.txt";
            StreamReader sr = File.OpenText(filepath);
            string input;
            input = sr.ReadLine();
            string[] arr = ChuanHoaDuLieu(input);
            tL.Add(new TheLoai(arr[0], arr[1], arr[2]));
            while (true)
            {
                input = sr.ReadLine();
                if (input == null) break;
                arr = ChuanHoaDuLieu(input);
                tL.Add(new TheLoai(arr[0], arr[1], arr[2]));
            }
            sr.Close();
        }

    }
}

