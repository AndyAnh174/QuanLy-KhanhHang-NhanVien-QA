using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_KhanhHang_NhanVien
{
    public partial class FormThemVoucher : Form
    {
        private DataAccess dataAccess;

        public FormThemVoucher()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            LoadHangThanhVien();
        }

        private void LoadHangThanhVien()
        {
            try
            {
                string query = "SELECT MAHANG, TENHANG FROM HANGTHANHVIEN ORDER BY DIEMTOITHIEU";
                DataTable dt = dataAccess.ExecuteQuery(query);
                
                cboHangApDung.Items.Add(new ComboBoxItem { Value = "", Text = "Tat ca hang" });
                foreach (DataRow row in dt.Rows)
                {
                    cboHangApDung.Items.Add(new ComboBoxItem 
                    { 
                        Value = row["MAHANG"].ToString(), 
                        Text = row["TENHANG"].ToString() 
                    });
                }
                cboHangApDung.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai hang thanh vien: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaVoucher.Text) || 
                string.IsNullOrWhiteSpace(txtTenVoucher.Text) ||
                numGiaTriGiam.Value <= 0)
            {
                MessageBox.Show("Vui long nhap day du thong tin!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpNgayBatDau.Value >= dtpNgayKetThuc.Value)
            {
                MessageBox.Show("Ngay bat dau phai nho hon ngay ket thuc!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string loaiGiam = rdoPhanTram.Checked ? "Phan tram" : "Tien mat";
                string maHangApDung = ((ComboBoxItem)cboHangApDung.SelectedItem).Value;
                
                string query = @"INSERT INTO VOUCHER (MAVOUCHER, TENVOUCHER, MOTA, GIATRIGIAM, LOAIGIAM, 
                                NGAYBATDAU, NGAYKETTHUC, SOLUONG, DIEMTOITHIEU, MAHANG_APDUNG)
                                VALUES (@MaVoucher, @TenVoucher, @MoTa, @GiaTriGiam, @LoaiGiam,
                                @NgayBatDau, @NgayKetThuc, @SoLuong, @DiemToiThieu, @MaHangApDung)";
                
                var parameters = new[] {
                    new System.Data.SqlClient.SqlParameter("@MaVoucher", txtMaVoucher.Text),
                    new System.Data.SqlClient.SqlParameter("@TenVoucher", txtTenVoucher.Text),
                    new System.Data.SqlClient.SqlParameter("@MoTa", txtMoTa.Text),
                    new System.Data.SqlClient.SqlParameter("@GiaTriGiam", numGiaTriGiam.Value),
                    new System.Data.SqlClient.SqlParameter("@LoaiGiam", loaiGiam),
                    new System.Data.SqlClient.SqlParameter("@NgayBatDau", dtpNgayBatDau.Value),
                    new System.Data.SqlClient.SqlParameter("@NgayKetThuc", dtpNgayKetThuc.Value),
                    new System.Data.SqlClient.SqlParameter("@SoLuong", (int)numSoLuong.Value),
                    new System.Data.SqlClient.SqlParameter("@DiemToiThieu", (int)numDiemToiThieu.Value),
                    new System.Data.SqlClient.SqlParameter("@MaHangApDung", string.IsNullOrEmpty(maHangApDung) ? (object)DBNull.Value : maHangApDung)
                };

                dataAccess.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Them voucher thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi them voucher: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    public class ComboBoxItem
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}