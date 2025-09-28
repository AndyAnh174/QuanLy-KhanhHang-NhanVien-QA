using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_KhanhHang_NhanVien
{
    public partial class FormThemKhachHang : Form
    {
        private DataAccess dataAccess;

        public FormThemKhachHang()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            GenerateMaKH();
        }

        private void GenerateMaKH()
        {
            txtMaND.Text = "KH" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) || 
                string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ||
                string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui long nhap day du thong tin bat buoc!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kiem tra ten dang nhap da ton tai chua
                string checkQuery = "SELECT COUNT(*) FROM TAIKHOAN WHERE TENDANGNHAP = @TenDangNhap";
                var checkParams = new[] { new System.Data.SqlClient.SqlParameter("@TenDangNhap", txtTenDangNhap.Text) };
                int count = Convert.ToInt32(dataAccess.ExecuteScalar(checkQuery, checkParams));
                
                if (count > 0)
                {
                    MessageBox.Show("Ten dang nhap da ton tai!", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string gioiTinh = rdoNam.Checked ? "Nam" : (rdoNu.Checked ? "Nu" : "Khac");

                // Them nguoi dung
                string insertND = @"INSERT INTO NGUOIDUNG (MAND, HOTEN, NGAYSINH, GIOITINH, EMAIL, SODIENTHOAI, DIACHI, CMND)
                                   VALUES (@MaND, @HoTen, @NgaySinh, @GioiTinh, @Email, @SoDienThoai, @DiaChi, @CMND)";
                
                var ndParams = new[] {
                    new System.Data.SqlClient.SqlParameter("@MaND", txtMaND.Text),
                    new System.Data.SqlClient.SqlParameter("@HoTen", txtHoTen.Text),
                    new System.Data.SqlClient.SqlParameter("@NgaySinh", dtpNgaySinh.Value),
                    new System.Data.SqlClient.SqlParameter("@GioiTinh", gioiTinh),
                    new System.Data.SqlClient.SqlParameter("@Email", txtEmail.Text),
                    new System.Data.SqlClient.SqlParameter("@SoDienThoai", txtSoDienThoai.Text),
                    new System.Data.SqlClient.SqlParameter("@DiaChi", txtDiaChi.Text),
                    new System.Data.SqlClient.SqlParameter("@CMND", txtCMND.Text)
                };
                dataAccess.ExecuteNonQuery(insertND, ndParams);

                // Them tai khoan
                string insertTK = @"INSERT INTO TAIKHOAN (MAND, TENDANGNHAP, MATKHAU, VAITRO)
                                   VALUES (@MaND, @TenDangNhap, @MatKhau, 'KHACHHANG')";
                
                var tkParams = new[] {
                    new System.Data.SqlClient.SqlParameter("@MaND", txtMaND.Text),
                    new System.Data.SqlClient.SqlParameter("@TenDangNhap", txtTenDangNhap.Text),
                    new System.Data.SqlClient.SqlParameter("@MatKhau", txtMatKhau.Text)
                };
                dataAccess.ExecuteNonQuery(insertTK, tkParams);

                // Them khach hang
                string insertKH = @"INSERT INTO KHACHHANG (MAND, MAHANG, MAND_QL, DIEMTICHLUY)
                                   VALUES (@MaND, 'BRONZE', @MaNVQL, 0)";
                
                var khParams = new[] {
                    new System.Data.SqlClient.SqlParameter("@MaND", txtMaND.Text),
                    new System.Data.SqlClient.SqlParameter("@MaNVQL", Session.MaND) // Nhan vien dang dang nhap lam quan ly
                };
                dataAccess.ExecuteNonQuery(insertKH, khParams);

                MessageBox.Show("Them khach hang thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi them khach hang: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}