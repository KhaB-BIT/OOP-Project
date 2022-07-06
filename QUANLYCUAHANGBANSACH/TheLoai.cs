namespace QUANLYCUAHANGBANSACH
{
    class TheLoai
    {
        private string theloai, mota, sachnoibat;


        public string Theloai { get => theloai; set => theloai = value; }
        public string Mota { get => mota; set => mota = value; }
        public string Sachnoibat { get => sachnoibat; set => sachnoibat = value; }

        public TheLoai(string theloai, string mota, string sachnoibat)
        {
            this.theloai = theloai;
            this.mota = mota;
            this.sachnoibat = sachnoibat;
        }
        public override string ToString()
        {
            return "Thể loại: " +theloai + "| Mô tả: " + mota + "| Sách nổi bật: " + sachnoibat;
        }
    }
}
