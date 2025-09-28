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
            LoadTinNhanCuaToi();
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
                string query = @"SELECT hd.MAHD, hd.NGAYTHANHTOAN, hd.TIENTRUOCGIAM, hd.TIENGIAM, 
                                hd.THANHTIEN, hd.MAVOUCHER, hd.TRANGTHAI, hd.GHICHU
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
                // Lay thong tin khach hang de kiem tra dieu kien voucher
                string queryKH = @"SELECT k.DIEMTICHLUY, k.MAHANG, h.TENHANG FROM KHACHHANG k
                                  LEFT JOIN HANGTHANHVIEN h ON k.MAHANG = h.MAHANG  
                                  WHERE k.MAND = @MaND";
                var paramKH = new[] { new System.Data.SqlClient.SqlParameter("@MaND", Session.MaND) };
                DataTable dtKH = dataAccess.ExecuteQuery(queryKH, paramKH);
                
                if (dtKH.Rows.Count == 0) return;
                
                int diemHienTai = Convert.ToInt32(dtKH.Rows[0]["DIEMTICHLUY"]);
                string hangHienTai = dtKH.Rows[0]["MAHANG"].ToString();
                string tenHang = dtKH.Rows[0]["TENHANG"].ToString();

                // Query voucher voi dieu kien kiem tra day du
                string query = @"SELECT v.MAVOUCHER, v.TENVOUCHER, v.MOTA, v.GIATRIGIAM, 
                                v.LOAIGIAM, v.NGAYBATDAU, v.NGAYKETTHUC, v.DIEMTOITHIEU,
                                CASE 
                                    WHEN v.MAHANG_APDUNG IS NULL THEN 'Tat ca hang'
                                    ELSE h.TENHANG 
                                END as HANG_AP_DUNG,
                                (v.SOLUONG - v.DASUDUNG) as CON_LAI,
                                CASE 
                                    WHEN v.DIEMTOITHIEU <= @DiemHienTai THEN 'Du diem'
                                    ELSE 'Thieu ' + CAST((v.DIEMTOITHIEU - @DiemHienTai) AS VARCHAR) + ' diem'
                                END as TRANG_THAI_DIEM,
                                CASE 
                                    WHEN v.MAHANG_APDUNG IS NULL OR v.MAHANG_APDUNG = @HangHienTai THEN 'Phu hop'
                                    ELSE 'Khong phu hop'
                                END as TRANG_THAI_HANG
                                FROM VOUCHER v
                                LEFT JOIN HANGTHANHVIEN h ON v.MAHANG_APDUNG = h.MAHANG
                                WHERE v.TRANGTHAI = 1 
                                AND GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC
                                AND (v.DASUDUNG < v.SOLUONG OR v.SOLUONG = 0)
                                AND v.DIEMTOITHIEU <= @DiemHienTai
                                AND (v.MAHANG_APDUNG IS NULL OR v.MAHANG_APDUNG = @HangHienTai)
                                ORDER BY v.GIATRIGIAM DESC, v.NGAYTAO DESC";
                
                var parameters = new[] { 
                    new System.Data.SqlClient.SqlParameter("@DiemHienTai", diemHienTai),
                    new System.Data.SqlClient.SqlParameter("@HangHienTai", hangHienTai)
                };
                
                DataTable dt = dataAccess.ExecuteQuery(query, parameters);
                dgvVoucher.DataSource = dt;
                
                // Thong bao chi tiet cho debug
                string thongBaoDebug = $"Thong tin voucher:\n";
                thongBaoDebug += $"- Hang hien tai: {tenHang} ({hangHienTai})\n";
                thongBaoDebug += $"- Diem hien tai: {diemHienTai}\n";
                thongBaoDebug += $"- So voucher kha dung: {dt.Rows.Count}";
                
                // Hien thi tat ca voucher (ke ca khong dung duoc) cho debug
                string queryAllVouchers = @"SELECT COUNT(*) as TONG_VOUCHER,
                                          SUM(CASE WHEN v.DIEMTOITHIEU <= @DiemHienTai THEN 1 ELSE 0 END) as DU_DIEM,
                                          SUM(CASE WHEN v.MAHANG_APDUNG IS NULL OR v.MAHANG_APDUNG = @HangHienTai THEN 1 ELSE 0 END) as DUNG_HANG,
                                          SUM(CASE WHEN GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC THEN 1 ELSE 0 END) as CON_HAN
                                          FROM VOUCHER v 
                                          WHERE v.TRANGTHAI = 1";
                
                DataTable dtStats = dataAccess.ExecuteQuery(queryAllVouchers, parameters);
                if (dtStats.Rows.Count > 0)
                {
                    DataRow statsRow = dtStats.Rows[0];
                    thongBaoDebug += $"\n\nThong ke voucher (debug):\n";
                    thongBaoDebug += $"- Tong voucher: {statsRow["TONG_VOUCHER"]}\n";
                    thongBaoDebug += $"- Du diem: {statsRow["DU_DIEM"]}\n"; 
                    thongBaoDebug += $"- Dung hang: {statsRow["DUNG_HANG"]}\n";
                    thongBaoDebug += $"- Con han: {statsRow["CON_HAN"]}";
                }
                
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(thongBaoDebug, "Debug Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai voucher: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTinNhanCuaToi()
        {
            try
            {
                string query = @"SELECT tt.MATT, nv_nd.HOTEN as TEN_NHAN_VIEN, tt.NOIDUNG, 
                                tt.KETQUA, tt.NGAYTUONGTAC, tt.TRANGTHAI
                                FROM TUONGTACKHACHHANG tt
                                INNER JOIN NHANVIEN nv ON tt.MAND_NV = nv.MAND
                                INNER JOIN NGUOIDUNG nv_nd ON nv.MAND = nv_nd.MAND
                                WHERE tt.MAND_KH = @MaND
                                ORDER BY tt.NGAYTUONGTAC DESC";
                
                var parameters = new[] { new System.Data.SqlClient.SqlParameter("@MaND", Session.MaND) };
                DataTable dt = dataAccess.ExecuteQuery(query, parameters);
                dgvTinNhan.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tai tin nhan: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                LoadTinNhanCuaToi();
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            Session.Clear();
            this.Hide();
            Form1 loginForm = new Form1();
            loginForm.Show();
        }

        private void FormKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}