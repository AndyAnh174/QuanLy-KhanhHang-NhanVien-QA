using System;
using System.Windows.Forms;

namespace QuanLy_KhanhHang_NhanVien
{
    public partial class FormGuiTinNhan : Form
    {
        private DataAccess dataAccess;

        public FormGuiTinNhan()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui long nhap noi dung tin nhan!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lay nhan vien quan ly ngau nhien
                string queryNV = "SELECT TOP 1 MAND FROM NHANVIEN ORDER BY NEWID()";
                var dtNV = dataAccess.ExecuteQuery(queryNV);
                
                if (dtNV.Rows.Count == 0)
                {
                    MessageBox.Show("Khong tim thay nhan vien!", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string maNV = dtNV.Rows[0]["MAND"].ToString();

                string query = @"INSERT INTO TUONGTACKHACHHANG (MAND_KH, MAND_NV, HINHTHUC, NOIDUNG, TRANGTHAI)
                                VALUES (@MaKH, @MaNV, @HinhThuc, @NoiDung, @TrangThai)";
                
                var parameters = new[] {
                    new System.Data.SqlClient.SqlParameter("@MaKH", Session.MaND),
                    new System.Data.SqlClient.SqlParameter("@MaNV", maNV),
                    new System.Data.SqlClient.SqlParameter("@HinhThuc", cboHinhThuc.Text),
                    new System.Data.SqlClient.SqlParameter("@NoiDung", txtNoiDung.Text),
                    new System.Data.SqlClient.SqlParameter("@TrangThai", "Chua xu ly")
                };

                dataAccess.ExecuteNonQuery(query, parameters);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi gui tin nhan: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}