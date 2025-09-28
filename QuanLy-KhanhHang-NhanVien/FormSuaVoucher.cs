using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_KhanhHang_NhanVien
{
    public partial class FormSuaVoucher : Form
    {
        private DataAccess dataAccess;
        private string maVoucher;

        public FormSuaVoucher(string maVoucher)
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            this.maVoucher = maVoucher;
            LoadHangThanhVien();
            LoadThongTin();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai hang thanh vien: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongTin()
        {
            try
            {
                string query = "SELECT * FROM VOUCHER WHERE MAVOUCHER = @MaVoucher";
                var parameters = new[] { new System.Data.SqlClient.SqlParameter("@MaVoucher", maVoucher) };
                DataTable dt = dataAccess.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtMaVoucher.Text = row["MAVOUCHER"].ToString();
                    txtTenVoucher.Text = row["TENVOUCHER"].ToString();
                    txtMoTa.Text = row["MOTA"].ToString();
                    numGiaTriGiam.Value = Convert.ToDecimal(row["GIATRIGIAM"]);
                    
                    string loaiGiam = row["LOAIGIAM"].ToString();
                    if (loaiGiam == "Phan tram")
                        rdoPhanTram.Checked = true;
                    else
                        rdoTienMat.Checked = true;

                    dtpNgayBatDau.Value = Convert.ToDateTime(row["NGAYBATDAU"]);
                    dtpNgayKetThuc.Value = Convert.ToDateTime(row["NGAYKETTHUC"]);
                    numSoLuong.Value = Convert.ToDecimal(row["SOLUONG"]);
                    numDiemToiThieu.Value = Convert.ToDecimal(row["DIEMTOITHIEU"]);

                    string maHangApDung = row["MAHANG_APDUNG"]?.ToString();
                    for (int i = 0; i < cboHangApDung.Items.Count; i++)
                    {
                        ComboBoxItem item = (ComboBoxItem)cboHangApDung.Items[i];
                        if (item.Value == maHangApDung)
                        {
                            cboHangApDung.SelectedIndex = i;
                            break;
                        }
                    }

                    if (cboHangApDung.SelectedIndex == -1)
                        cboHangApDung.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai thong tin voucher: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenVoucher.Text) || numGiaTriGiam.Value <= 0)
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
                
                string query = @"UPDATE VOUCHER SET TENVOUCHER = @TenVoucher, MOTA = @MoTa, 
                                GIATRIGIAM = @GiaTriGiam, LOAIGIAM = @LoaiGiam,
                                NGAYBATDAU = @NgayBatDau, NGAYKETTHUC = @NgayKetThuc, 
                                SOLUONG = @SoLuong, DIEMTOITHIEU = @DiemToiThieu, 
                                MAHANG_APDUNG = @MaHangApDung
                                WHERE MAVOUCHER = @MaVoucher";
                
                var parameters = new[] {
                    new System.Data.SqlClient.SqlParameter("@TenVoucher", txtTenVoucher.Text),
                    new System.Data.SqlClient.SqlParameter("@MoTa", txtMoTa.Text),
                    new System.Data.SqlClient.SqlParameter("@GiaTriGiam", numGiaTriGiam.Value),
                    new System.Data.SqlClient.SqlParameter("@LoaiGiam", loaiGiam),
                    new System.Data.SqlClient.SqlParameter("@NgayBatDau", dtpNgayBatDau.Value),
                    new System.Data.SqlClient.SqlParameter("@NgayKetThuc", dtpNgayKetThuc.Value),
                    new System.Data.SqlClient.SqlParameter("@SoLuong", (int)numSoLuong.Value),
                    new System.Data.SqlClient.SqlParameter("@DiemToiThieu", (int)numDiemToiThieu.Value),
                    new System.Data.SqlClient.SqlParameter("@MaHangApDung", string.IsNullOrEmpty(maHangApDung) ? (object)DBNull.Value : maHangApDung),
                    new System.Data.SqlClient.SqlParameter("@MaVoucher", txtMaVoucher.Text)
                };

                dataAccess.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Cap nhat voucher thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi cap nhat voucher: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}