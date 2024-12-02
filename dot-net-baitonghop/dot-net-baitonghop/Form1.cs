//HoTen:Vũ Xuân Lâm
//MaSV:671605
//Lop:K67CNPMB
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dot_net_baitonghop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Tao chuoi ket noi
        String strCon = @"Data Source=M22;Initial Catalog=QLBanHang;Integrated Security=True";

        //Tao ket noi
        SqlConnection sqlCon = null;

        //ham mo ket noi
        private void MoKetNoi()
        {
            if(sqlCon == null) sqlCon = new SqlConnection(strCon);
            if(sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        //tao bien
        SqlDataAdapter adapder = null;
        DataSet ds = null;

        //Ham hien thi danh sach san pham
        private void HienThiDSSP()
        {
            MoKetNoi();

            //Lenh truy van
            String query = "Select * from tblSanPham";
            adapder = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapder);
            ds = new DataSet();
            adapder.Fill(ds, "tblSanPham");
            dataDSSP.DataSource = ds.Tables["tblSanPham"];

           
        }
        //Ham tim kiem theo ma
        private void TimKiemTheoMa(String ma)
        {
            MoKetNoi();

            //Lenh truy van
            String query = "select * from tblSanPham where MaSpham ='"+ma+"'";
            adapder = new SqlDataAdapter(query, sqlCon);
            ds = new DataSet();
            adapder.Fill(ds, "tblTimKiemMa");
            dataDSSP.DataSource = ds.Tables["tblTimKiemMa"];
        }

        //Hàm xóa dữ liệu
        private void XoaDL()
        {
            txtMa.Clear();
            txtTen.Clear();
            txtSlNhap.Clear();
            txtGiaNhap.Clear();
            txtKieuDang.Clear();

        }
        //Ham tim kiem theo ten
        private void TimKiemTheoTen(String ten)
        {
            MoKetNoi();

            //Lenh truy van
            String query = "select * from tblSanPham where TenSpham like '%"+ten+"%'";
            adapder = new SqlDataAdapter(query, sqlCon);
            ds = new DataSet();
            adapder.Fill(ds, "tblTimKiemTen");
            dataDSSP.DataSource = ds.Tables["tblTimKiemTen"];
        }
        //ham them san pham
        private void ThemSanPham()
        {
            try
            {
                MoKetNoi();

                DataRow row = ds.Tables["tblSanPham"].NewRow();
                row["MaSpham"] = txtMa.Text.Trim();
                row["TenSpham"] = txtTen.Text.Trim();
                row["KieuDang"] = txtKieuDang.Text.Trim();
                //combobox tình trạng
                if (cbTinhTrang.SelectedIndex == 0) row["TinhTrang"] = "Còn hàng";
                else if (cbTinhTrang.SelectedIndex == 1) row["TinhTrang"] = "Hết hàng";

                row["SlNhap"] = txtSlNhap.Text.Trim();
                row["GiaNhap"] = txtGiaNhap.Text.Trim();

                //COmbo box hãng
                if (cbHang.SelectedIndex == 0) row["HangSanXuat"] = "Apple";
                else if (cbHang.SelectedIndex == 1) row["HangSanXuat"] = "SamSung";
                else if (cbHang.SelectedIndex == 2) row["HangSanXuat"] = "LG";
                else if (cbHang.SelectedIndex == 3) row["HangSanXuat"] = "Sony";
                else if (cbHang.SelectedIndex == 4) row["HangSanXuat"] = "Vivo";
                else if (cbHang.SelectedIndex == 5) row["HangSanXuat"] = "Huawei";
                else if (cbHang.SelectedIndex == 6) row["HangSanXuat"] = "Oppo";
                ds.Tables["tblSanPham"].Rows.Add(row);

                int kq = adapder.Update(ds.Tables["tblSanPham"]);

                if (kq > 0)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    HienThiDSSP();
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm không thành công!");
                }

                XoaDL();
                GBCTTCT.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi rồi nè: " +ex.Message);
            }
        }

        //Ham sua san pham 
        private void SuaSanPham()
        {
            try
            {
                if (vt == -1) return;
                MoKetNoi();

                DataRow row = ds.Tables["tblSanPham"].Rows[vt];
                row.BeginEdit();

                row["MaSpham"] = txtMa.Text.Trim();
                row["TenSpham"] = txtTen.Text.Trim();
                row["KieuDang"] = txtKieuDang.Text.Trim();
                //combobox tình trạng
                if (cbTinhTrang.SelectedIndex == 0) row["TinhTrang"] = "Còn hàng";
                else if (cbTinhTrang.SelectedIndex == 1) row["TinhTrang"] = "Hết hàng";

                row["SlNhap"] = txtSlNhap.Text.Trim();
                row["GiaNhap"] = txtGiaNhap.Text.Trim();

                //COmbo box hãng
                if (cbHang.SelectedIndex == 0) row["HangSanXuat"] = "Apple";
                else if (cbHang.SelectedIndex == 1) row["HangSanXuat"] = "SamSung";
                else if (cbHang.SelectedIndex == 2) row["HangSanXuat"] = "LG";
                else if (cbHang.SelectedIndex == 3) row["HangSanXuat"] = "Sony";
                else if (cbHang.SelectedIndex == 4) row["HangSanXuat"] = "Vivo";
                else if (cbHang.SelectedIndex == 5) row["HangSanXuat"] = "Huawei";
                else if (cbHang.SelectedIndex == 6) row["HangSanXuat"] = "Oppo";


                row.EndEdit();
                int kq = adapder.Update(ds.Tables["tblSanPham"]);

                if (kq > 0)
                {
                    MessageBox.Show("Sửa sản phẩm thành công!");
                    HienThiDSSP();
                }
                else
                {
                    MessageBox.Show("Sửa sản phẩm không thành công!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi rồi nè: "+ex.Message);
            }
            XoaDL();
            GBCTTCT.Enabled = false;

        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?","Thoát",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Hien thi
            HienThiDSSP();

            GBCTTCT.Enabled=false;
            btnSua.Enabled=false;
            btnXoa.Enabled=false;

            cbTinhTrang.Items.Add("Còn hàng");
            cbTinhTrang.Items.Add("Hết hàng");


            cbHang.Items.Add("Apple");
            cbHang.Items.Add("SamSung");
            cbHang.Items.Add("LG");
            cbHang.Items.Add("Sony");
            cbHang.Items.Add("Vivo");
            cbHang.Items.Add("Huawei");
            cbHang.Items.Add("Oppo");


        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //Lay ve gia tri tren 2 o nhap
            String ma = txtNhapMa.Text.Trim();
            String ten = txtNhapTen.Text.Trim();

            if (ma != "" && ten == "") TimKiemTheoMa(ma);
            else if (ma == "" && ten != "") TimKiemTheoTen(ten);
            else if (ma != "" && ten != "") TimKiemTheoMa(ma);
            else
            {
                MessageBox.Show("Bạn phải nhập thông tin để tìm kiếm sản phẩm!");
                txtNhapMa.Focus();
            }
        }

        int ChucNang = 0;

        private void btnThem_Click(object sender, EventArgs e)
        {
            GBCTTCT.Enabled= true;
            ChucNang = 1;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ChucNang == 1) ThemSanPham();
            else if (ChucNang == 2) SuaSanPham();
        }

        int vt = -1;
        private void dataDSSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           try
            {
                btnSua.Enabled= true;
                btnXoa.Enabled= true;
                vt = e.RowIndex;
                if (vt == -1) return;

                DataRow row = ds.Tables["tblSanPham"].Rows[vt];

                txtMa.Text = row["MaSpham"].ToString().Trim();
                txtTen.Text = row["TenSpham"].ToString().Trim();
                txtKieuDang.Text = row["KieuDang"].ToString().Trim();
                if (row["TinhTrang"].ToString().Trim() == "Còn hàng") cbTinhTrang.SelectedIndex = 0;
                else cbTinhTrang.SelectedIndex = 1;
                txtSlNhap.Text = row["SlNhap"].ToString().Trim();
                txtGiaNhap.Text = row["GiaNhap"].ToString().Trim();

                if (row["HangSanXuat"].ToString().Trim() == "Apple") cbHang.SelectedIndex = 0;
                else if (row["HangSanXuat"].ToString().Trim() == "SamSung") cbHang.SelectedIndex = 1;
                else if (row["HangSanXuat"].ToString().Trim() == "LG") cbHang.SelectedIndex = 2;
                else if (row["HangSanXuat"].ToString().Trim() == "Sony") cbHang.SelectedIndex = 3;
                else if (row["HangSanXuat"].ToString().Trim() == "Vivo") cbHang.SelectedIndex = 4;
                else if (row["HangSanXuat"].ToString().Trim() == "Huawei") cbHang.SelectedIndex = 5;
                else if (row["HangSanXuat"].ToString().Trim() == "Oppo") cbHang.SelectedIndex = 6;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi rồi nè: " +ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            GBCTTCT.Enabled= true;
            ChucNang = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (vt == -1) return;

                MoKetNoi();
                DataRow row = ds.Tables["tblSanPham"].Rows[vt];
                row.Delete();
                int kq = adapder.Update(ds.Tables["tblSanPham"]);

                if (kq > 0)
                {
                    MessageBox.Show("Xóa sản phẩm thành công!");
                    HienThiDSSP();
                }
                else
                {
                    MessageBox.Show("Xóa sản phẩm không thành công!");
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi rồi nè: "+ex.Message);
            }
            XoaDL();
            GBCTTCT.Enabled= false;
            btnSua.Enabled= false;
            btnXoa.Enabled= false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            XoaDL();
            GBCTTCT.Enabled= false;
            btnXoa.Enabled= false;
            btnSua.Enabled= false;
        }
    }
}
