using System;
using System.Windows.Forms;

namespace QuanLy_KhanhHang_NhanVien
{
    public partial class FormPhanHoi : Form
    {
        private DataAccess dataAccess;
        private int maTT;

        public FormPhanHoi(int maTT)
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            this.maTT = maTT;
            LoadThongTin();
        }

        private void LoadThongTin()
        {
            try
            {
                string query = @"SELECT tt.NOIDUNG, kh_nd.HOTEN
                                FROM TUONGTACKHACHHANG tt
                                INNER JOIN KHACHHANG kh ON tt.MAND_KH = kh.MAND
                                INNER JOIN NGUOIDUNG kh_nd ON kh.MAND = kh_nd.MAND
                                WHERE tt.MATT = @MaTT";
                
                var parameters = new[] { new System.Data.SqlClient.SqlParameter("@MaTT", maTT) };
                var dt = dataAccess.ExecuteQuery(query, parameters);
                
                if (dt.Rows.Count > 0)
                {
                    lblKhachHang.Text = $"Khach hang: {dt.Rows[0]["HOTEN"]}";
                    txtNoiDungCu.Text = dt.Rows[0]["NOIDUNG"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhanHoi.Text))
            {
                MessageBox.Show("Vui long nhap noi dung phan hoi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = @"UPDATE TUONGTACKHACHHANG 
                                SET KETQUA = @PhanHoi, TRANGTHAI = 'Da xu ly'
                                WHERE MATT = @MaTT";
                
                var parameters = new[] {
                    new System.Data.SqlClient.SqlParameter("@PhanHoi", txtPhanHoi.Text),
                    new System.Data.SqlClient.SqlParameter("@MaTT", maTT)
                };

                dataAccess.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Phan hoi thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi phan hoi: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}