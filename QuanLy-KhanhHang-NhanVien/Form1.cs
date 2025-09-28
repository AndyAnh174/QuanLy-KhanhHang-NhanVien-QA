using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_KhanhHang_NhanVien
{
    public partial class Form1 : Form
    {
        private DataAccess dataAccess;

        public Form1()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            this.Text = "Dang Nhap He Thong";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui long nhap day du thong tin!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable result = dataAccess.DangNhap(tenDangNhap, matKhau);
                
                if (result.Rows.Count > 0)
                {
                    // Luu thong tin vao session
                    DataRow user = result.Rows[0];
                    Session.MaND = user["MAND"].ToString();
                    Session.HoTen = user["HOTEN"].ToString();
                    Session.VaiTro = user["VAITRO"].ToString();
                    Session.Email = user["EMAIL"].ToString();
                    Session.SoDienThoai = user["SODIENTHOAI"].ToString();
                    Session.TrangThai = Convert.ToBoolean(user["TRANGTHAI"]);

                    MessageBox.Show($"Xin chao {Session.HoTen}!", "Dang nhap thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mo form tuong ung theo vai tro
                    if (Session.IsNhanVien || Session.IsAdmin)
                    {
                        FormNhanVien formNV = new FormNhanVien();
                        formNV.Show();
                    }
                    else if (Session.IsKhachHang)
                    {
                        FormKhachHang formKH = new FormKhachHang();
                        formKH.Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Ten dang nhap hoac mat khau khong dung!", "Loi dang nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi ket noi: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
