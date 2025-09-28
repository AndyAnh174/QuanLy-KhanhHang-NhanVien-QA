using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_KhanhHang_NhanVien
{
    public partial class FormTaoHoaDon : Form
    {
        private DataAccess dataAccess;

        public FormTaoHoaDon()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            GenerateMaHD();
            
            // Them event de tinh toan khi nhap ma voucher
            txtMaVoucher.TextChanged += TxtMaVoucher_TextChanged;
            numTongTien.ValueChanged += NumTongTien_ValueChanged;
        }

        private void GenerateMaHD()
        {
            txtMaHD.Text = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void TxtMaVoucher_TextChanged(object sender, EventArgs e)
        {
            TinhToanTienGiam();
        }

        private void NumTongTien_ValueChanged(object sender, EventArgs e)
        {
            TinhToanTienGiam();
        }

        private void TinhToanTienGiam()
        {
            if (numTongTien.Value <= 0) return;

            decimal tongTien = numTongTien.Value;
            decimal tienGiam = 0;
            decimal thanhTien = tongTien;
            
            string maVoucher = txtMaVoucher.Text.Trim();
            
            if (!string.IsNullOrEmpty(maVoucher))
            {
                try
                {
                    // Kiem tra voucher hop le va tinh tien giam
                    string queryVoucher = @"SELECT v.GIATRIGIAM, v.LOAIGIAM, v.DIEMTOITHIEU, 
                                           v.MAHANG_APDUNG, v.TRANGTHAI, v.DASUDUNG, v.SOLUONG,
                                           v.NGAYBATDAU, v.NGAYKETTHUC, v.TENVOUCHER,
                                           k.DIEMTICHLUY, k.MAHANG as HANG_KHACHHANG
                                           FROM VOUCHER v
                                           CROSS JOIN KHACHHANG k
                                           WHERE v.MAVOUCHER = @MaVoucher 
                                           AND k.MAND = @MaKH";
                    
                    var parameters = new[] {
                        new System.Data.SqlClient.SqlParameter("@MaVoucher", maVoucher),
                        new System.Data.SqlClient.SqlParameter("@MaKH", Session.MaND)
                    };
                    
                    DataTable dtVoucher = dataAccess.ExecuteQuery(queryVoucher, parameters);
                    
                    if (dtVoucher.Rows.Count > 0)
                    {
                        DataRow voucherRow = dtVoucher.Rows[0];
                        decimal giaTriGiam = Convert.ToDecimal(voucherRow["GIATRIGIAM"]);
                        string loaiGiam = voucherRow["LOAIGIAM"].ToString();
                        int diemToiThieu = Convert.ToInt32(voucherRow["DIEMTOITHIEU"]);
                        int diemKhachHang = Convert.ToInt32(voucherRow["DIEMTICHLUY"]);
                        string hangVoucher = voucherRow["MAHANG_APDUNG"]?.ToString();
                        string hangKhachHang = voucherRow["HANG_KHACHHANG"].ToString();
                        int trangThai = Convert.ToInt32(voucherRow["TRANGTHAI"]);
                        int daSuDung = Convert.ToInt32(voucherRow["DASUDUNG"]);
                        int soLuong = Convert.ToInt32(voucherRow["SOLUONG"]);
                        DateTime ngayBatDau = Convert.ToDateTime(voucherRow["NGAYBATDAU"]);
                        DateTime ngayKetThuc = Convert.ToDateTime(voucherRow["NGAYKETTHUC"]);
                        string tenVoucher = voucherRow["TENVOUCHER"].ToString();
                        
                        // Reset background color
                        txtMaVoucher.BackColor = System.Drawing.SystemColors.Window;
                        
                        // Kiem tra tung dieu kien mot cach chi tiet
                        bool coTheApDung = true;
                        string lyDoKhongApDung = "";
                        
                        // 1. Kiem tra trang thai voucher
                        if (trangThai != 1)
                        {
                            coTheApDung = false;
                            lyDoKhongApDung = "Voucher da bi vo hieu hoa";
                        }
                        // 2. Kiem tra han su dung
                        else if (DateTime.Now < ngayBatDau)
                        {
                            coTheApDung = false;
                            lyDoKhongApDung = $"Voucher chua den han su dung (Bat dau: {ngayBatDau.ToString("dd/MM/yyyy")})";
                        }
                        else if (DateTime.Now > ngayKetThuc)
                        {
                            coTheApDung = false;
                            lyDoKhongApDung = $"Voucher da het han (Het han: {ngayKetThuc.ToString("dd/MM/yyyy")})";
                        }
                        // 3. Kiem tra so luong
                        else if (soLuong > 0 && daSuDung >= soLuong)
                        {
                            coTheApDung = false;
                            lyDoKhongApDung = $"Voucher da het luot su dung ({daSuDung}/{soLuong})";
                        }
                        // 4. Kiem tra diem toi thieu
                        else if (diemKhachHang < diemToiThieu)
                        {
                            coTheApDung = false;
                            lyDoKhongApDung = $"Khong du diem! Can: {diemToiThieu}, Co: {diemKhachHang}";
                        }
                        // 5. Kiem tra hang thanh vien
                        else if (!string.IsNullOrEmpty(hangVoucher) && hangVoucher != hangKhachHang)
                        {
                            coTheApDung = false;
                            lyDoKhongApDung = $"Voucher chi danh cho hang {hangVoucher}, ban dang o hang {hangKhachHang}";
                        }
                        
                        if (coTheApDung)
                        {
                            // Tinh tien giam
                            if (loaiGiam == "Phan tram")
                            {
                                tienGiam = Math.Round(tongTien * giaTriGiam / 100, 0);
                                // Gioi han tien giam khong vuot qua tong tien
                                if (tienGiam > tongTien) tienGiam = tongTien;
                            }
                            else
                            {
                                tienGiam = giaTriGiam;
                                // Gioi han tien giam khong vuot qua tong tien
                                if (tienGiam > tongTien) tienGiam = tongTien;
                            }
                            
                            thanhTien = tongTien - tienGiam;
                            
                            // Hien thi thong tin voucher
                            string thongBaoVoucher = $"Ap dung voucher thanh cong!\n{tenVoucher}\n";
                            thongBaoVoucher += $"Loai giam: {loaiGiam}\n";
                            thongBaoVoucher += $"Gia tri: {giaTriGiam:N0}{(loaiGiam == "Phan tram" ? "%" : " VND")}\n";
                            thongBaoVoucher += $"Tien giam: {tienGiam:N0} VND\n";
                            thongBaoVoucher += $"Thanh tien: {thanhTien:N0} VND";
                            
                            // Cap nhat ghi chu
                            txtGhiChu.Text = thongBaoVoucher;
                            
                            // Mau xanh nhe khi hop le
                            txtMaVoucher.BackColor = System.Drawing.Color.LightGreen;
                        }
                        else
                        {
                            // Mau hong khi khong hop le
                            txtMaVoucher.BackColor = System.Drawing.Color.LightPink;
                            txtGhiChu.Text = $"Voucher khong hop le!\n{lyDoKhongApDung}";
                        }
                    }
                    else
                    {
                        // Khong tim thay voucher
                        txtMaVoucher.BackColor = System.Drawing.Color.LightPink;
                        txtGhiChu.Text = "Ma voucher khong ton tai!";
                    }
                }
                catch (Exception ex)
                {
                    txtMaVoucher.BackColor = System.Drawing.Color.LightPink;
                    txtGhiChu.Text = $"Loi kiem tra voucher:\n{ex.Message}";
                }
            }
            else
            {
                // Khong co voucher
                txtGhiChu.Text = "";
                txtMaVoucher.BackColor = System.Drawing.SystemColors.Window;
            }
            
            // Cap nhat hien thi tong ket
            this.Text = $"Tao hoa don - Tong: {tongTien:N0} - Giam: {tienGiam:N0} - Thanh toan: {thanhTien:N0}";
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            if (numTongTien.Value <= 0)
            {
                MessageBox.Show("Vui long nhap tong tien hop le!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiem tra lai voucher truoc khi tao hoa don
            string maVoucher = string.IsNullOrWhiteSpace(txtMaVoucher.Text) ? null : txtMaVoucher.Text.Trim();
            
            if (!string.IsNullOrEmpty(maVoucher))
            {
                // Neu background la mau hong thi voucher khong hop le
                if (txtMaVoucher.BackColor == System.Drawing.Color.LightPink)
                {
                    MessageBox.Show("Ma voucher khong hop le! Vui long kiem tra lai.", "Loi", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Double check voucher truoc khi tao hoa don
                try
                {
                    string queryCheck = @"SELECT COUNT(*) FROM VOUCHER v 
                                         INNER JOIN KHACHHANG k ON 1=1
                                         WHERE v.MAVOUCHER = @MaVoucher 
                                         AND k.MAND = @MaKH
                                         AND v.TRANGTHAI = 1
                                         AND GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC  
                                         AND (v.DASUDUNG < v.SOLUONG OR v.SOLUONG = 0)
                                         AND k.DIEMTICHLUY >= v.DIEMTOITHIEU
                                         AND (v.MAHANG_APDUNG IS NULL OR v.MAHANG_APDUNG = k.MAHANG)";
                    
                    var checkParams = new[] {
                        new System.Data.SqlClient.SqlParameter("@MaVoucher", maVoucher),
                        new System.Data.SqlClient.SqlParameter("@MaKH", Session.MaND)
                    };
                    
                    int validCount = Convert.ToInt32(dataAccess.ExecuteScalar(queryCheck, checkParams));
                    
                    if (validCount == 0)
                    {
                        MessageBox.Show("Voucher khong hop le hoac da thay doi! Vui long kiem tra lai.", "Loi", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TinhToanTienGiam(); // Refresh lai thong tin
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Loi kiem tra voucher: {ex.Message}", "Loi", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                // Su dung trigger se tu dong tinh toan
                string query = @"INSERT INTO HOADONCHITIET (MAHD, MAND_KH, TONGTIEN, MAVOUCHER, SOLUONG, TRANGTHAI, GHICHU)
                                VALUES (@MaHD, @MaKH, @TongTien, @MaVoucher, @SoLuong, 'Da thanh toan', @GhiChu)";
                
                var parameters = new[] {
                    new System.Data.SqlClient.SqlParameter("@MaHD", txtMaHD.Text),
                    new System.Data.SqlClient.SqlParameter("@MaKH", Session.MaND),
                    new System.Data.SqlClient.SqlParameter("@TongTien", numTongTien.Value),
                    new System.Data.SqlClient.SqlParameter("@MaVoucher", (object)maVoucher ?? System.DBNull.Value),
                    new System.Data.SqlClient.SqlParameter("@SoLuong", 1),
                    new System.Data.SqlClient.SqlParameter("@GhiChu", txtGhiChu.Text ?? "")
                };

                dataAccess.ExecuteNonQuery(query, parameters);

                // Cap nhat so luong da su dung cua voucher (neu co)
                if (!string.IsNullOrEmpty(maVoucher))
                {
                    string updateVoucherQuery = "UPDATE VOUCHER SET DASUDUNG = DASUDUNG + 1 WHERE MAVOUCHER = @MaVoucher";
                    var voucherParams = new[] { new System.Data.SqlClient.SqlParameter("@MaVoucher", maVoucher) };
                    dataAccess.ExecuteNonQuery(updateVoucherQuery, voucherParams);
                }

                // Hien thi thong bao thanh cong voi chi tiet
                string thongBao = "Tao hoa don thanh cong!\n\n";
                thongBao += $"Ma hoa don: {txtMaHD.Text}\n";
                thongBao += $"Tong tien: {numTongTien.Value:N0} VND\n";
                
                if (!string.IsNullOrEmpty(maVoucher))
                {
                    thongBao += $"Voucher: {maVoucher}\n";
                    
                    // Lay thong tin tien giam tu database
                    string queryResult = "SELECT TIENGIAM, THANHTIEN FROM HOADONCHITIET WHERE MAHD = @MaHD";
                    var resultParams = new[] { new System.Data.SqlClient.SqlParameter("@MaHD", txtMaHD.Text) };
                    DataTable dtResult = dataAccess.ExecuteQuery(queryResult, resultParams);
                    
                    if (dtResult.Rows.Count > 0)
                    {
                        decimal tienGiamThucTe = Convert.ToDecimal(dtResult.Rows[0]["TIENGIAM"]);
                        decimal thanhTienThucTe = Convert.ToDecimal(dtResult.Rows[0]["THANHTIEN"]);
                        thongBao += $"Tien giam: {tienGiamThucTe:N0} VND\n";
                        thongBao += $"Thanh toan: {thanhTienThucTe:N0} VND\n";
                    }
                }
                
                thongBao += "\nDiem tich luy da duoc cap nhat!";
                
                MessageBox.Show(thongBao, "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tao hoa don: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}