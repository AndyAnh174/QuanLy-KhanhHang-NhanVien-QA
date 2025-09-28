using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_KhanhHang_NhanVien
{
    public partial class FormKhachHang : Form
    {
        private DataAccess dataAccess;

        public FormKhachHang()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            this.Text = $"Khach Hang - {Session.HoTen}";
            LoadData();
        }

        private void LoadData()
        {
            LoadThongTinTaiKhoan();
            LoadLichSuGiaoDich();
            LoadVoucherKhaDung();
        }

        private void LoadThongTinTaiKhoan()
        {
            try
            {
                string query = @"SELECT n.HOTEN, n.EMAIL, n.SODIENTHOAI, n.DIACHI, n.NGAYSINH,
                                k.DIEMTICHLUY, h.TENHANG, h.UUDI, h.DIEMTOITHIEU
                                FROM NGUOIDUNG n
                                INNER JOIN KHACHHANG k ON n.MAND = k.MAND
                                LEFT JOIN HANGTHANHVIEN h ON k.MAHANG = h.MAHANG
                                WHERE n.MAND = @MaND";
                
                var parameters = new[] { new System.Data.SqlClient.SqlParameter("@MaND", Session.MaND) };
                DataTable dt = dataAccess.ExecuteQuery(query, parameters);
                
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblHoTen.Text = $"Ho ten: {row["HOTEN"]}";
                    lblEmail.Text = $"Email: {row["EMAIL"]}";
                    lblSoDienThoai.Text = $"So dien thoai: {row["SODIENTHOAI"]}";
                    lblDiaChi.Text = $"Dia chi: {row["DIACHI"]}";
                    lblNgaySinh.Text = $"Ngay sinh: {Convert.ToDateTime(row["NGAYSINH"]).ToString("dd/MM/yyyy")}";
                    lblDiemTichLuy.Text = $"Diem tich luy: {row["DIEMTICHLUY"]}";
                    lblHangThanhVien.Text = $"Hang thanh vien: {row["TENHANG"]}";
                    lblUuDai.Text = $"Uu dai: {row["UUDI"]}";
                    
                    // Tinh diem can de len hang tiep theo
                    int diemHienTai = Convert.ToInt32(row["DIEMTICHLUY"]);
                    string queryHangTiepTheo = @"SELECT TOP 1 TENHANG, DIEMTOITHIEU 
                                               FROM HANGTHANHVIEN 
                                               WHERE DIEMTOITHIEU > @DiemHienTai 
                                               ORDER BY DIEMTOITHIEU";
                    var paramHang = new[] { new System.Data.SqlClient.SqlParameter("@DiemHienTai", diemHienTai) };
                    DataTable dtHang = dataAccess.ExecuteQuery(queryHangTiepTheo, paramHang);
                    
                    if (dtHang.Rows.Count > 0)
                    {
                        int diemCanThem = Convert.ToInt32(dtHang.Rows[0]["DIEMTOITHIEU"]) - diemHienTai;
                        lblHangTiepTheo.Text = $"De len {dtHang.Rows[0]["TENHANG"]}, can them: {diemCanThem} diem";
                    }
                    else
                    {
                        lblHangTiepTheo.Text = "Ban da dat hang cao nhat!";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai thong tin tai khoan: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLichSuGiaoDich()
        {
            try
            {
                string query = @"SELECT hd.MAHD, hd.NGAYTHANHTOAN, hd.THANHTIEN, 
                                hd.MAVOUCHER, hd.TIENGIAM, hd.TRANGTHAI, hd.GHICHU
                                FROM HOADONCHITIET hd
                                WHERE hd.MAND_KH = @MaND
                                ORDER BY hd.NGAYTHANHTOAN DESC";
                
                var parameters = new[] { new System.Data.SqlClient.SqlParameter("@MaND", Session.MaND) };
                DataTable dt = dataAccess.ExecuteQuery(query, parameters);
                dgvLichSuGiaoDich.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai lich su giao dich: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVoucherKhaDung()
        {
            try
            {
                string query = @"SELECT v.MAVOUCHER, v.TENVOUCHER, v.MOTA, v.GIATRIGIAM, 
                                v.LOAIGIAM, v.NGAYBATDAU, v.NGAYKETTHUC, v.DIEMTOITHIEU,
                                h.TENHANG as HANG_AP_DUNG
                                FROM VOUCHER v
                                LEFT JOIN HANGTHANHVIEN h ON v.MAHANG_APDUNG = h.MAHANG
                                WHERE v.TRANGTHAI = 1 
                                AND GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC
                                AND (v.DASUDUNG < v.SOLUONG OR v.SOLUONG = 0)
                                ORDER BY v.NGAYTAO DESC";
                
                DataTable dt = dataAccess.ExecuteQuery(query);
                dgvVoucher.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai voucher: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            FormTaoHoaDon formTao = new FormTaoHoaDon();
            if (formTao.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnGuiTinNhan_Click(object sender, EventArgs e)
        {
            FormGuiTinNhan formGui = new FormGuiTinNhan();
            if (formGui.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Gui tin nhan thanh cong! Nhan vien se phan hoi trong thoi gian som nhat.", 
                               "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();
        }

        private void FormKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}