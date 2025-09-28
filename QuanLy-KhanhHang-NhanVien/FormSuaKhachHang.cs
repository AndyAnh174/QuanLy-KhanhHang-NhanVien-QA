using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_KhanhHang_NhanVien
{
    public partial class FormSuaKhachHang : Form
    {
        private DataAccess dataAccess;
        private string maND;

        public FormSuaKhachHang(string maND)
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            this.maND = maND;
            LoadThongTin();
        }

        private void LoadThongTin()
        {
            try
            {
                string query = @"SELECT n.HOTEN, n.EMAIL, n.SODIENTHOAI, n.DIACHI, n.NGAYSINH, n.GIOITINH
                                FROM NGUOIDUNG n WHERE n.MAND = @MaND";
                var parameters = new[] { new System.Data.SqlClient.SqlParameter("@MaND", maND) };
                DataTable dt = dataAccess.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtHoTen.Text = row["HOTEN"].ToString();
                    txtEmail.Text = row["EMAIL"].ToString();
                    txtSoDienThoai.Text = row["SODIENTHOAI"].ToString();
                    txtDiaChi.Text = row["DIACHI"].ToString();
                    if (row["NGAYSINH"] != DBNull.Value)
                        dtpNgaySinh.Value = Convert.ToDateTime(row["NGAYSINH"]);
                    
                    string gioiTinh = row["GIOITINH"].ToString();
                    if (gioiTinh == "Nam") rdoNam.Checked = true;
                    else if (gioiTinh == "Nu") rdoNu.Checked = true;
                    else rdoKhac.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai thong tin: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui long nhap day du thong tin bat buoc!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string gioiTinh = rdoNam.Checked ? "Nam" : (rdoNu.Checked ? "Nu" : "Khac");
                
                string query = @"UPDATE NGUOIDUNG SET HOTEN = @HoTen, EMAIL = @Email, 
                                SODIENTHOAI = @SoDienThoai, DIACHI = @DiaChi, 
                                NGAYSINH = @NgaySinh, GIOITINH = @GioiTinh
                                WHERE MAND = @MaND";
                
                var parameters = new[] {
                    new System.Data.SqlClient.SqlParameter("@HoTen", txtHoTen.Text),
                    new System.Data.SqlClient.SqlParameter("@Email", txtEmail.Text),
                    new System.Data.SqlClient.SqlParameter("@SoDienThoai", txtSoDienThoai.Text),
                    new System.Data.SqlClient.SqlParameter("@DiaChi", txtDiaChi.Text),
                    new System.Data.SqlClient.SqlParameter("@NgaySinh", dtpNgaySinh.Value),
                    new System.Data.SqlClient.SqlParameter("@GioiTinh", gioiTinh),
                    new System.Data.SqlClient.SqlParameter("@MaND", maND)
                };

                dataAccess.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Cap nhat thong tin thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi cap nhat: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}