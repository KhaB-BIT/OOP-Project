using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace QUANLYCUAHANGBANSACH
{
    class HoaDon
    {
        private string mahoadon;
        private string ngayxuat;
        private string makh;
        private int thanhtien;
        private ArrayList dssp;

        public string Mahoadon { get => mahoadon; set => mahoadon = value; }
        public string Makh { get => makh; set => makh = value; }
        public int Thanhtien { get => thanhtien; set => thanhtien = value; }

        public HoaDon (string mahoadon,string ngayxuat,string makh,int thanhtien)
        {
            this.mahoadon = mahoadon;
            this.ngayxuat = ngayxuat;
            this.makh = makh;
            this.thanhtien = thanhtien;
        }
    }
}
