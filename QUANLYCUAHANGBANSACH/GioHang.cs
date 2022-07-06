using System;
using System.Collections;
using System.IO;

namespace QUANLYCUAHANGBANSACH
{
    class GioHang
    {
        private ArrayList danhsachsanpham;

        public ArrayList Danhsachsanpham { get => danhsachsanpham; set => danhsachsanpham = value; }
        public GioHang()
        {
            Danhsachsanpham = new ArrayList();
        }
        /*THÊM MỘT SP VÀO GIỎ*/
        public void ThemSPVaoGioHang(ArrayList sp)
        {

            string msp; int kt;
            Console.Write(" Nhập mã sản phẩm cần muốn mua: ");
            do
            {
                msp = Console.ReadLine();
                kt = TimKiemMasp(msp, sp);
                if (kt == -1) Console.Write(" --->Không tìm thấy mã sản phẩm này, hãy nhập lại: ");
            } while (kt == -1);
            Console.Write(" Nhập số lượng cần mua: ");
            int slg; bool kiemtra = false;
            do
            {
                slg = int.Parse(Console.ReadLine());
                int y = TimKiemMasp(msp, sp);
                SanPham sp1 = (SanPham)sp[y];
                if (slg > sp1.Soluong)
                    Console.Write(" --->Sản phẩm {0} chỉ còn {1} quyển, vui lòng nhập ít hơn hoặc nhập 0 nếu không mua: ", sp1.Tensp, sp1.Soluong);
                else kiemtra = true;
            } while (kiemtra == false);
            if (slg != 0)
            {
                ArrayList tmp = new ArrayList();
                tmp.Add(((SanPham)sp[TimKiemMasp(msp, sp)]).Clone());
                tmp.Add(slg);
                Danhsachsanpham.Add(tmp);
                Console.WriteLine(" --->Đã thêm !");
            }
            else Console.WriteLine(" --->Chưa thêm!");
        }

        public int TimKiemMasp(string x, ArrayList sp) //tìm kiếm mã sản phẩm
        {
            for (int i = 0; i < sp.Count; i++)
            {
                if (((SanPham)sp[i]).Masp.Equals(x))
                    return i;
            }
            return -1;
        }
        /*XÓA TOÀN BỘ MỘT SP KHỎI GIỎ*/
        public void XoaSPKhoiGioHang()
        {
            Console.Write(" Nhập mã sản phẩm bạn muốn xóa khỏi giỏ hàng, hoặc nhập 0 nếu không muốn xóa: ");
            string x = Console.ReadLine(); int j = 0;
            if (x != "0")
            {
                for (int i = 0; i < Danhsachsanpham.Count; i++)
                {
                    ArrayList tmp = (ArrayList)Danhsachsanpham[i];
                    if (((SanPham)tmp[0]).Masp.Equals(x))
                    {
                        Danhsachsanpham.RemoveAt(i);
                        j++;
                        break;
                    }
                }
                if (j != 0) Console.WriteLine(" --->Đã xóa"); else Console.WriteLine("Mã sản phẩm không tồn tại trong giỏ hàng");
            }
        }
        /*CẬP NHẬT GIỎ*/
        public void CapNhatGioHang(ArrayList sp)
        {
            string msp; bool kt = false;
            do
            {
                Console.Write(" Nhập mã sản phẩm cần muốn cập nhật: ");
                msp = Console.ReadLine();
                for (int i = 0; i < Danhsachsanpham.Count; i++)
                {
                    ArrayList tmp = (ArrayList)Danhsachsanpham[i];
                    if (((SanPham)tmp[0]).Masp.Equals(msp)) kt = true;
                }
                if (kt == false) Console.Write(" --->Không tìm thấy mã sản phẩm này trong giỏ, hãy nhập lại: ");
            } while (kt == false);

            int slg; bool kiemtra = false;
            do
            {
                int y = TimKiemMasp(msp, sp);
                SanPham sp1 = (SanPham)sp[y];
                Console.WriteLine(" --->Sản phẩm {0} chỉ còn {1} quyển, vui lòng cập nhật không lớn hơn số lượng này: ", sp1.Tensp, sp1.Soluong);
                Console.Write("Nhập số lượng muốn cập nhật: ");
                slg = int.Parse(Console.ReadLine());
                if (slg > sp1.Soluong)
                    Console.WriteLine(" --->Sản phẩm {0} chỉ còn {1} quyển, vui lòng cập nhật không lớn hơn số lượng này: ", sp1.Tensp, sp1.Soluong);
                else kiemtra = true;
            } while (kiemtra == false);

            for (int i = 0; i < Danhsachsanpham.Count; i++)
            {
                ArrayList tmp = (ArrayList)Danhsachsanpham[i];
                if (((SanPham)tmp[0]).Masp.Equals(msp))
                {
                    tmp[1] = slg;
                    break;
                }
            }
            Console.WriteLine(" --->Đã cập nhật");
        }
        /*XÓA TOÀN BỘ GIỎ HÀNG*/
        public void XoaGioHang()
        {
            Console.Write("  Bạn muốn xóa giỏ hàng?  0.Không,tôi ấn nhầm | 1.Vâng, xóa nó đi! ");
            string st = Console.ReadLine();
            switch (st)
            {
                case "0": break;
                case "1":
                    Danhsachsanpham.Clear();
                    Console.WriteLine("---> Đã xóa giỏ hàng"); break;
                default: break;
            }
        }
        /*XEM GIỎ HÀNG*/
        public void XemGioHang()
        {
            if (Danhsachsanpham.Count == 0) Console.WriteLine(" --->Giỏ hàng của bạn đang rỗng :( ");
            else
            {
                Console.WriteLine("  -------------------------------GIỎ HÀNG CỦA BẠN----------------------------------");
                int tongtien = 0;
                for (int i = 0; i < Danhsachsanpham.Count; i++)
                {
                    ArrayList tmp = (ArrayList)Danhsachsanpham[i];
                    Console.WriteLine("  Mã sản phẩm: " + ((SanPham)tmp[0]).Masp + " Tên sản phẩm: " + ((SanPham)tmp[0]).Tensp + " Số lượng: " + tmp[1] + " Giá bán: " + ((SanPham)tmp[0]).Giaban);
                    tongtien = tongtien + (int)tmp[1] * ((SanPham)tmp[0]).Giaban;
                }
                Console.WriteLine("------------------------------------------------------------------------------------");
            }

        }
        /*IN HÓA ĐƠN*/
        public void XuatHoaDon(KhachHang kh, ref ArrayList sp)
        {
            if (Danhsachsanpham.Count == 0) Console.WriteLine(" --->Chưa có gì trong giỏ để mua :( ");
            else
            {
                DateTime now = DateTime.Now;
                int rd = new Random().Next(1000, 9999); //chỉ mang tính chất mô phỏng
                Console.WriteLine(" --------------------------------HÓA ĐƠN CỦA BẠN------------------------------------");
                Console.WriteLine("  Mã hóa đơn: " + rd);
                Console.WriteLine("  Ngày xuất: " + now);
                Console.WriteLine("  " + kh.ToString());
                int thanhtien = 0;
                for (int i = 0; i < Danhsachsanpham.Count; i++)
                {
                    ArrayList tmp = (ArrayList)Danhsachsanpham[i];
                    Console.WriteLine("  " + ((SanPham)tmp[0]).Tensp + "  |  " + tmp[1] + "  |  " + ((SanPham)tmp[0]).Giaban);
                    thanhtien = thanhtien + (int)tmp[1] * ((SanPham)tmp[0]).Giaban;
                }
                Console.WriteLine("  Thành tiền: " + thanhtien + "VND");
                Console.WriteLine("  Mua hàng thành công, đơn hàng của bạn sẽ được giao trong 2 ngày tới");
                GhiDuLieuHD(Danhsachsanpham, kh, now, rd, thanhtien);
            }
            //cập nhật lại số lượng trên gian hàng khi đã xác nhận mua hàng
            for (int i = 0; i < Danhsachsanpham.Count; i++)
            {
                ArrayList tmp = (ArrayList)Danhsachsanpham[i];
                string masanpham = ((SanPham)tmp[0]).Masp;
                int slhd = (int)tmp[1];
                for (int j = 0; j < sp.Count; j++)
                {
                    if (((SanPham)sp[j]).Masp == masanpham)
                    {
                        int slg = ((SanPham)sp[j]).Soluong;
                        ((SanPham)sp[j]).Soluong = slg - slhd;
                        break;
                    }
                }
            }
        }
        /*GHI ĐÈ DỮ LIỆU VÀO HÓA ĐƠN*/
        public void GhiDuLieuHD(ArrayList dssp, KhachHang kh, DateTime now, int rd, int thanhtien)
        {
            string filepath = @"C:\Users\PC_LENOVO\Desktop\HD.txt";
            System.IO.FileStream sfs = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(sfs);
            sw.WriteLine("  Mã hóa đơn: " + rd);
            sw.WriteLine("  Ngày xuất: " + now);
            sw.WriteLine("  " + kh.ToString());
            for (int i = 0; i < dssp.Count; i++)
            {
                ArrayList tmp = (ArrayList)dssp[i];
                sw.WriteLine("  " + ((SanPham)tmp[0]).Tensp + "  |  " + tmp[1] + "  |  " + ((SanPham)tmp[0]).Giaban);
            }
            sw.WriteLine("  Thành tiền: " + thanhtien + "VND");
            sw.WriteLine("--------------------------------------------------------");
            sw.Flush();
            sw.Close();
        }
    }
}