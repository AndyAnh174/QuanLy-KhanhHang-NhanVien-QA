using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_KhanhHang_NhanVien
{
    public partial class FormNhanVien : Form
    {
        private DataAccess dataAccess;

        public FormNhanVien()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            this.Text = $"Quan Ly - {Session.HoTen} ({Session.VaiTro})";
            LoadData();
        }

        private void LoadData()
        {
            LoadKhachHang();
            LoadVoucher();
            LoadGiaoDich();
            LoadTuongTac();
        }

        private void LoadKhachHang()
        {
            try
            {
                DataTable dt = dataAccess.TimKiemKhachHang();
                dgvKhachHang.DataSource = dt;
                
                // FIX: ?n c?t TRANGTHAI kh?i DataGridView
                if (dgvKhachHang.Columns.Contains("TRANGTHAI"))
                {
                    dgvKhachHang.Columns["TRANGTHAI"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai du lieu khach hang: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVoucher()
        {
            try
            {
                string query = "SELECT * FROM VOUCHER WHERE TRANGTHAI = 1 ORDER BY NGAYTAO DESC";
                DataTable dt = dataAccess.ExecuteQuery(query);
                dgvVoucher.DataSource = dt;
                
                // FIX: ?n c?t TRANGTHAI kh?i DataGridView voucher n?u c?n
                if (dgvVoucher.Columns.Contains("TRANGTHAI"))
                {
                    dgvVoucher.Columns["TRANGTHAI"].Visible = false;
                }
                if (dgvVoucher.Columns.Contains("DASUDUNG"))
                {
                    dgvVoucher.Columns["DASUDUNG"].HeaderText = "?ã s? d?ng";
                }
                if (dgvVoucher.Columns.Contains("SOLUONG"))
                {
                    dgvVoucher.Columns["SOLUONG"].HeaderText = "T?ng s? l??ng";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai du lieu voucher: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGiaoDich()
        {
            try
            {
                string query = @"SELECT lg.MAGD, kh_nd.HOTEN as TEN_KHACH_HANG, lg.SOTIEN as THANH_TIEN, 
                                hd.MAVOUCHER, htv.TENHANG as HANG_THANH_VIEN, lg.NGAYGD
                                FROM LICHSUGIAODICH lg
                                LEFT JOIN KHACHHANG kh ON lg.MAKH = kh.MAND
                                LEFT JOIN NGUOIDUNG kh_nd ON kh.MAND = kh_nd.MAND
                                LEFT JOIN HOADONCHITIET hd ON lg.MAHD = hd.MAHD
                                LEFT JOIN HANGTHANHVIEN htv ON kh.MAHANG = htv.MAHANG
                                ORDER BY lg.NGAYGD DESC";
                DataTable dt = dataAccess.ExecuteQuery(query);
                dgvGiaoDich.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai du lieu giao dich: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTuongTac()
        {
            try
            {
                string query = @"SELECT tt.MATT, kh_nd.HOTEN as TEN_KHACH_HANG, tt.NOIDUNG, 
                                tt.NGAYTUONGTAC, tt.TRANGTHAI
                                FROM TUONGTACKHACHHANG tt
                                INNER JOIN KHACHHANG kh ON tt.MAND_KH = kh.MAND
                                INNER JOIN NGUOIDUNG kh_nd ON kh.MAND = kh_nd.MAND
                                WHERE kh.TRANGTHAI = 1
                                ORDER BY tt.NGAYTUONGTAC DESC";
                DataTable dt = dataAccess.ExecuteQuery(query);
                dgvTuongTac.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai du lieu tuong tac: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tab Khach Hang
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                DataTable dt = dataAccess.TimKiemKhachHang(string.IsNullOrEmpty(keyword) ? null : keyword);
                dgvKhachHang.DataSource = dt;
                
                // FIX: ?n c?t TRANGTHAI sau khi tìm ki?m
                if (dgvKhachHang.Columns.Contains("TRANGTHAI"))
                {
                    dgvKhachHang.Columns["TRANGTHAI"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tim kiem: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            FormThemKhachHang formThem = new FormThemKhachHang();
            if (formThem.ShowDialog() == DialogResult.OK)
            {
                LoadKhachHang();
            }
        }

        private void btnSuaKhachHang_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow != null)
            {
                string maND = dgvKhachHang.CurrentRow.Cells["MAND"].Value.ToString();
                FormSuaKhachHang formSua = new FormSuaKhachHang(maND);
                if (formSua.ShowDialog() == DialogResult.OK)
                {
                    LoadKhachHang();
                }
            }
            else
            {
                MessageBox.Show("Vui long chon khach hang can sua!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoaKhachHang_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow != null)
            {
                string maND = dgvKhachHang.CurrentRow.Cells["MAND"].Value.ToString();
                string tenKH = dgvKhachHang.CurrentRow.Cells["HOTEN"].Value.ToString();

                if (MessageBox.Show($"Ban co chac chan muon xoa khach hang {tenKH}?", "Xac nhan", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Xoa khach hang trong bang KHACHHANG truoc
                        string deleteKHQuery = "UPDATE KHACHHANG SET TRANGTHAI = 0 WHERE MAND = @MaND";
                        var parameters1 = new[] { new System.Data.SqlClient.SqlParameter("@MaND", maND) };
                        dataAccess.ExecuteNonQuery(deleteKHQuery, parameters1);

                        // Sau do xoa tai khoan trong bang TAIKHOAN
                        string deleteTKQuery = "UPDATE TAIKHOAN SET TRANGTHAI = 0 WHERE MAND = @MaND";
                        var parameters2 = new[] { new System.Data.SqlClient.SqlParameter("@MaND", maND) };
                        dataAccess.ExecuteNonQuery(deleteTKQuery, parameters2);

                        // Cuoi cùng xoa nguoi dung
                        string deleteNDQuery = "UPDATE NGUOIDUNG SET TRANGTHAI = 0 WHERE MAND = @MaND";
                        var parameters3 = new[] { new System.Data.SqlClient.SqlParameter("@MaND", maND) };
                        dataAccess.ExecuteNonQuery(deleteNDQuery, parameters3);

                        MessageBox.Show("Xoa khach hang thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadKhachHang();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Loi xoa khach hang: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui long chon khach hang can xoa!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Tab Voucher
        private void btnThemVoucher_Click(object sender, EventArgs e)
        {
            FormThemVoucher formThem = new FormThemVoucher();
            if (formThem.ShowDialog() == DialogResult.OK)
            {
                LoadVoucher();
            }
        }

        private void btnSuaVoucher_Click(object sender, EventArgs e)
        {
            if (dgvVoucher.CurrentRow != null)
            {
                string maVoucher = dgvVoucher.CurrentRow.Cells["MAVOUCHER"].Value.ToString();
                FormSuaVoucher formSua = new FormSuaVoucher(maVoucher);
                if (formSua.ShowDialog() == DialogResult.OK)
                {
                    LoadVoucher();
                }
            }
            else
            {
                MessageBox.Show("Vui long chon voucher can sua!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoaVoucher_Click(object sender, EventArgs e)
        {
            if (dgvVoucher.CurrentRow != null)
            {
                string maVoucher = dgvVoucher.CurrentRow.Cells["MAVOUCHER"].Value.ToString();
                string tenVoucher = dgvVoucher.CurrentRow.Cells["TENVOUCHER"].Value.ToString();

                if (MessageBox.Show($"Ban co chac chan muon xoa voucher {tenVoucher}?", "Xac nhan", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string query = "UPDATE VOUCHER SET TRANGTHAI = 0 WHERE MAVOUCHER = @MaVoucher";
                        var parameters = new[] { new System.Data.SqlClient.SqlParameter("@MaVoucher", maVoucher) };
                        dataAccess.ExecuteNonQuery(query, parameters);
                        MessageBox.Show("Xoa voucher thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadVoucher();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Loi xoa voucher: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui long chon voucher can xoa!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Tab Tuong Tac
        private void btnPhanHoi_Click(object sender, EventArgs e)
        {
            if (dgvTuongTac.CurrentRow != null)
            {
                int maTT = Convert.ToInt32(dgvTuongTac.CurrentRow.Cells["MATT"].Value);
                FormPhanHoi formPhanHoi = new FormPhanHoi(maTT);
                if (formPhanHoi.ShowDialog() == DialogResult.OK)
                {
                    LoadTuongTac();
                }
            }
            else
            {
                MessageBox.Show("Vui long chon tin nhan can phan hoi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            Session.Clear();
            this.Hide();
            Form1 loginForm = new Form1();
            loginForm.Show();
        }

        private void FormNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}