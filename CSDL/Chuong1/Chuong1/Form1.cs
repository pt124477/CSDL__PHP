using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Chuong1
{
    public partial class Form1 : Form
    {
        //Khai báo các đói tượng 
        //1. Khai báo một biến (đối tuong) Dataset 
        DataSet ds = new DataSet();
        //2. Khai bao cac DataTable tuong ung voi cac tuong co chua du lieu
        DataTable tblKhoa = new DataTable("KHOA");
        DataTable tblSinhVien = new DataTable("SINHVIEN");
        DataTable tblKetQua = new DataTable("KETQUA");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Tạo cấu trức cho các DataTABLE
            Tao_cau_truc_cac_bang();
            Moc_noi_quan_he_cac_bang();
            Nhap_lieu_cho_cac_bang();
        }

       

        private void Nhap_lieu_cho_cac_bang()
        {
            Nhap_lieu_tblKhoa();
            Nhap_lieu_tblSinhVien();
            Nhap_lieu_tblKetQua();
        }
        private void Nhap_lieu_tblSinhVien()
        {
            //Nhập liệu cho tblKhoa => Đọc dữ liệu từ tập tin SINHVIEN.txt
            string[] Mang_SV = File.ReadAllLines(@"..\..\..\Data\SINHVIEN.txt");
            foreach (string Chuoi_SV in Mang_SV)
            {
                //Tách Chuoi_khoa thành các thành phần tương ứng với các cột trong tblSinhVien
                string[] Mang_thanh_phan = Chuoi_SV.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                //Tạo một dòng mới có cấu trúc giống cấu trúc của một dòng trong tblSinhVien
                DataRow rsv = tblSinhVien.NewRow();
                // Gắn giá trị cho các cột của dòng mới tạo ra
                for (int i = 0; i < Mang_thanh_phan.Length; i++)
                {
                    rsv[i] = Mang_thanh_phan[i];
                }

                //Thêm dòng vừa tạo vào tblKhoa 
                tblSinhVien.Rows.Add(rsv);
            }
        }
        private void Nhap_lieu_tblKetQua()
        {
            //Nhập liệu cho tblKETQU => Đọc dữ liệu từ tập tin KETQUA.txt
            string[] Mang_kq = File.ReadAllLines(@"..\..\..\Data\KETQUA.txt");
            foreach (string Chuoi_kq in Mang_kq)
            {
                //Tách Chuoi_khoa thành các thành phần tương ứng với tblKetQua
                string[] Mang_thanh_phan = Chuoi_kq.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                //Tạo một dòng mới có cấu trúc giống cấu trúc của một dòng trong tblKetQua
                DataRow rkq = tblKetQua.NewRow();
                // Gắn giá trị cho các cột của dòng mới tọa ra
                for (int i = 0; i < Mang_thanh_phan.Length; i++)
                {
                    rkq[i] = Mang_thanh_phan[i];
                }

                //Thêm dòng vừa tạo vào tblKhoa 
                tblKetQua.Rows.Add(rkq);
            }
        }

        private void Nhap_lieu_tblKhoa()
        {
            //Nhập liệu cho tblKhoa => Đọc dữ liệu từ tập tin KHOA.txt
            string[] Mang_khoa = File.ReadAllLines(@"..\..\..\Data\KHOA.txt");
            foreach (string Chuoi_khoa in Mang_khoa)
            {
                //Tách Chuoi_khoa thành các thành phần tương ứng với MaKH và TenKH
                string[] Mang_thanh_phan = Chuoi_khoa.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                //Tạo một dòng mới có cấu trúc giống cấu trúc của một dòng trong tblKhoa
                DataRow rkh = tblKhoa.NewRow();
                // Gắn giá trị cho các cột của dòng mới tọa ra
                rkh[0] = Mang_thanh_phan[0];
                rkh[1] = Mang_thanh_phan[1];

                //Thêm dòng vừa tạo vào tblKhoa 
                tblKhoa.Rows.Add(rkh);
            }
        }

        private void Moc_noi_quan_he_cac_bang()
        {
            // Tạo quan hệ giữa tblKhoa va tblSinhVien
            ds.Relations.Add("FK_KH_SV", ds.Tables["KHOA"].Columns["MaKH"], ds.Tables["SINHVIEN"].Columns["MaKH"], true);

            //Tao quan he giua tblSinhVien va tblKetQua
            ds.Relations.Add("FK_SV_KQ", ds.Tables["SINHVIEN"].Columns["MaSV"], ds.Tables["KETQUA"].Columns["MaSV"], true);

            //Loại bỏ Cascade delete trong các quan hệ 
            ds.Relations["FK_KH_SV"].ChildKeyConstraint.DeleteRule = Rule.None;
            ds.Relations["FK_SV_KQ"].ChildKeyConstraint.DeleteRule = Rule.None;



        }

        private void Tao_cau_truc_cac_bang()
        {
            //Tao cau truc cho DataTABLE  tuong ung voi bang KHOA
            tblKhoa.Columns.Add("MaKH", typeof(string));
            tblKhoa.Columns.Add("TenKH", typeof(string));
            //Tao khoa chinh cho tblKhoa
            tblKhoa.PrimaryKey = new DataColumn[] { tblKhoa.Columns["MaKH"]};

            //Tao cau truc cho DataTABLE  tuong ung voi bang SINHVIEN
            tblSinhVien.Columns.Add("MaSV", typeof(string));
            tblSinhVien.Columns.Add("HoSV", typeof(string));
            tblSinhVien.Columns.Add("TenSV", typeof(string));
            tblSinhVien.Columns.Add("Phai", typeof(Boolean));
            tblSinhVien.Columns.Add("NgaySinh", typeof(DateTime));
            tblSinhVien.Columns.Add("NoiSinh", typeof(string));
            tblSinhVien.Columns.Add("MaKH", typeof(string));
            tblSinhVien.Columns.Add("HocBong", typeof(double));
            //Tạo khóa chính cho tblSINHVIEN
            tblSinhVien.PrimaryKey = new DataColumn[] { tblSinhVien.Columns["MaSV"] };

            //Tao cau truc cho DataTABLE  tuong ung voi bang KETQUA
            tblKetQua.Columns.Add("MaSV", typeof(string));
            tblKetQua.Columns.Add("MaMH", typeof(string));
            tblKetQua.Columns.Add("Diem", typeof(Single));
            //Tao khoa chinh cho tblKetQua
            tblKetQua.PrimaryKey = new DataColumn[] { tblKetQua.Columns["MaSV"], tblKetQua.Columns["MaMH"] };

            //Thêm các DataTable vào Dataset 
            //ds.Tables.Add(tblKhoa);
            //ds.Tables.Add(tblSinhVien);
            //ds.Tables.Add(tblKetQua);

            // Thêm đồng thời nhiều cái DataTable vào Dataset
            ds.Tables.AddRange(new DataTable[] { tblKhoa, tblSinhVien, tblKetQua });

        }
    }
}
//Thành phần DataTatle
//1. Dùng để lưu trữ dữ liệu của một Table trong bộ nhớ
//2. Tạo một đối tượng DataTable: new DataTable("<Tên DataTable>");
//3. Tạo ra các cột (DataColoum): <Biến DataTable>.Colums.Add("<Tên cột>",<kiểu dữ liệu>)
//4. Tạo khóa chính cho DataTable => PrimaryKey => là mảng các DataColumn
//5. Thêm các DataTable vào Dataset
//6. Móc nối quan hệ giữa  các DataTable
