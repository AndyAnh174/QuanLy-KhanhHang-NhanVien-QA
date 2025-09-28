namespace QuanLy_KhanhHang_NhanVien
{
    partial class FormKhachHang
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl1;
        private TabPage tabThongTin;
        private TabPage tabLichSu;
        private TabPage tabVoucher;
        
        // Tab Thong Tin
        private GroupBox groupThongTin;
        private Label lblHoTen;
        private Label lblEmail;
        private Label lblSoDienThoai;
        private Label lblDiaChi;
        private Label lblNgaySinh;
        private Label lblDiemTichLuy;
        private Label lblHangThanhVien;
        private Label lblUuDai;
        private Label lblHangTiepTheo;
        private Button btnTaoHoaDon;
        private Button btnGuiTinNhan;
        
        // Tab Lich Su
        private DataGridView dgvLichSuGiaoDich;
        
        // Tab Voucher
        private DataGridView dgvVoucher;
        
        private Button btnDangXuat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new TabControl();
            this.tabThongTin = new TabPage();
            this.tabLichSu = new TabPage();
            this.tabVoucher = new TabPage();
            
            this.groupThongTin = new GroupBox();
            this.lblHoTen = new Label();
            this.lblEmail = new Label();
            this.lblSoDienThoai = new Label();
            this.lblDiaChi = new Label();
            this.lblNgaySinh = new Label();
            this.lblDiemTichLuy = new Label();
            this.lblHangThanhVien = new Label();
            this.lblUuDai = new Label();
            this.lblHangTiepTheo = new Label();
            this.btnTaoHoaDon = new Button();
            this.btnGuiTinNhan = new Button();
            
            this.dgvLichSuGiaoDich = new DataGridView();
            this.dgvVoucher = new DataGridView();
            
            this.btnDangXuat = new Button();

            this.tabControl1.SuspendLayout();
            this.tabThongTin.SuspendLayout();
            this.tabLichSu.SuspendLayout();
            this.tabVoucher.SuspendLayout();
            this.groupThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuGiaoDich)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).BeginInit();
            this.SuspendLayout();

            // tabControl1
            this.tabControl1.Controls.Add(this.tabThongTin);
            this.tabControl1.Controls.Add(this.tabLichSu);
            this.tabControl1.Controls.Add(this.tabVoucher);
            this.tabControl1.Location = new Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(960, 500);
            this.tabControl1.TabIndex = 0;

            // tabThongTin
            this.tabThongTin.Controls.Add(this.btnGuiTinNhan);
            this.tabThongTin.Controls.Add(this.btnTaoHoaDon);
            this.tabThongTin.Controls.Add(this.groupThongTin);
            this.tabThongTin.Location = new Point(4, 24);
            this.tabThongTin.Name = "tabThongTin";
            this.tabThongTin.Padding = new Padding(3);
            this.tabThongTin.Size = new Size(952, 472);
            this.tabThongTin.TabIndex = 0;
            this.tabThongTin.Text = "Thong tin tai khoan";
            this.tabThongTin.UseVisualStyleBackColor = true;

            // groupThongTin
            this.groupThongTin.Controls.Add(this.lblHangTiepTheo);
            this.groupThongTin.Controls.Add(this.lblUuDai);
            this.groupThongTin.Controls.Add(this.lblHangThanhVien);
            this.groupThongTin.Controls.Add(this.lblDiemTichLuy);
            this.groupThongTin.Controls.Add(this.lblNgaySinh);
            this.groupThongTin.Controls.Add(this.lblDiaChi);
            this.groupThongTin.Controls.Add(this.lblSoDienThoai);
            this.groupThongTin.Controls.Add(this.lblEmail);
            this.groupThongTin.Controls.Add(this.lblHoTen);
            this.groupThongTin.Location = new Point(20, 20);
            this.groupThongTin.Name = "groupThongTin";
            this.groupThongTin.Size = new Size(600, 300);
            this.groupThongTin.TabIndex = 0;
            this.groupThongTin.TabStop = false;
            this.groupThongTin.Text = "Thong tin ca nhan";

            // lblHoTen
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblHoTen.Location = new Point(20, 30);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new Size(55, 17);
            this.lblHoTen.TabIndex = 0;
            this.lblHoTen.Text = "Ho ten:";

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblEmail.Location = new Point(20, 60);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(46, 17);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Email:";

            // lblSoDienThoai
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblSoDienThoai.Location = new Point(20, 90);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new Size(102, 17);
            this.lblSoDienThoai.TabIndex = 2;
            this.lblSoDienThoai.Text = "So dien thoai:";

            // lblDiaChi
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblDiaChi.Location = new Point(20, 120);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new Size(57, 17);
            this.lblDiaChi.TabIndex = 3;
            this.lblDiaChi.Text = "Dia chi:";

            // lblNgaySinh
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblNgaySinh.Location = new Point(20, 150);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new Size(77, 17);
            this.lblNgaySinh.TabIndex = 4;
            this.lblNgaySinh.Text = "Ngay sinh:";

            // lblDiemTichLuy
            this.lblDiemTichLuy.AutoSize = true;
            this.lblDiemTichLuy.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblDiemTichLuy.ForeColor = Color.Blue;
            this.lblDiemTichLuy.Location = new Point(20, 190);
            this.lblDiemTichLuy.Name = "lblDiemTichLuy";
            this.lblDiemTichLuy.Size = new Size(110, 17);
            this.lblDiemTichLuy.TabIndex = 5;
            this.lblDiemTichLuy.Text = "Diem tich luy:";

            // lblHangThanhVien
            this.lblHangThanhVien.AutoSize = true;
            this.lblHangThanhVien.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblHangThanhVien.ForeColor = Color.Green;
            this.lblHangThanhVien.Location = new Point(20, 220);
            this.lblHangThanhVien.Name = "lblHangThanhVien";
            this.lblHangThanhVien.Size = new Size(134, 17);
            this.lblHangThanhVien.TabIndex = 6;
            this.lblHangThanhVien.Text = "Hang thanh vien:";

            // lblUuDai
            this.lblUuDai.AutoSize = true;
            this.lblUuDai.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Italic, GraphicsUnit.Point);
            this.lblUuDai.ForeColor = Color.DarkGreen;
            this.lblUuDai.Location = new Point(20, 250);
            this.lblUuDai.Name = "lblUuDai";
            this.lblUuDai.Size = new Size(50, 15);
            this.lblUuDai.TabIndex = 7;
            this.lblUuDai.Text = "Uu dai:";

            // lblHangTiepTheo
            this.lblHangTiepTheo.AutoSize = true;
            this.lblHangTiepTheo.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Italic, GraphicsUnit.Point);
            this.lblHangTiepTheo.ForeColor = Color.Orange;
            this.lblHangTiepTheo.Location = new Point(20, 275);
            this.lblHangTiepTheo.Name = "lblHangTiepTheo";
            this.lblHangTiepTheo.Size = new Size(100, 15);
            this.lblHangTiepTheo.TabIndex = 8;
            this.lblHangTiepTheo.Text = "Hang tiep theo:";

            // btnTaoHoaDon
            this.btnTaoHoaDon.BackColor = Color.Green;
            this.btnTaoHoaDon.ForeColor = Color.White;
            this.btnTaoHoaDon.Location = new Point(650, 50);
            this.btnTaoHoaDon.Name = "btnTaoHoaDon";
            this.btnTaoHoaDon.Size = new Size(120, 40);
            this.btnTaoHoaDon.TabIndex = 1;
            this.btnTaoHoaDon.Text = "Tao hoa don";
            this.btnTaoHoaDon.UseVisualStyleBackColor = false;
            this.btnTaoHoaDon.Click += new EventHandler(this.btnTaoHoaDon_Click);

            // btnGuiTinNhan
            this.btnGuiTinNhan.BackColor = Color.Blue;
            this.btnGuiTinNhan.ForeColor = Color.White;
            this.btnGuiTinNhan.Location = new Point(650, 110);
            this.btnGuiTinNhan.Name = "btnGuiTinNhan";
            this.btnGuiTinNhan.Size = new Size(120, 40);
            this.btnGuiTinNhan.TabIndex = 2;
            this.btnGuiTinNhan.Text = "Lien he tu van";
            this.btnGuiTinNhan.UseVisualStyleBackColor = false;
            this.btnGuiTinNhan.Click += new EventHandler(this.btnGuiTinNhan_Click);

            // tabLichSu
            this.tabLichSu.Controls.Add(this.dgvLichSuGiaoDich);
            this.tabLichSu.Location = new Point(4, 24);
            this.tabLichSu.Name = "tabLichSu";
            this.tabLichSu.Padding = new Padding(3);
            this.tabLichSu.Size = new Size(952, 472);
            this.tabLichSu.TabIndex = 1;
            this.tabLichSu.Text = "Lich su giao dich";
            this.tabLichSu.UseVisualStyleBackColor = true;

            // dgvLichSuGiaoDich
            this.dgvLichSuGiaoDich.AllowUserToAddRows = false;
            this.dgvLichSuGiaoDich.AllowUserToDeleteRows = false;
            this.dgvLichSuGiaoDich.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichSuGiaoDich.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSuGiaoDich.Location = new Point(6, 6);
            this.dgvLichSuGiaoDich.MultiSelect = false;
            this.dgvLichSuGiaoDich.Name = "dgvLichSuGiaoDich";
            this.dgvLichSuGiaoDich.ReadOnly = true;
            this.dgvLichSuGiaoDich.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSuGiaoDich.Size = new Size(940, 460);
            this.dgvLichSuGiaoDich.TabIndex = 0;

            // tabVoucher
            this.tabVoucher.Controls.Add(this.dgvVoucher);
            this.tabVoucher.Location = new Point(4, 24);
            this.tabVoucher.Name = "tabVoucher";
            this.tabVoucher.Size = new Size(952, 472);
            this.tabVoucher.TabIndex = 2;
            this.tabVoucher.Text = "Voucher kha dung";
            this.tabVoucher.UseVisualStyleBackColor = true;

            // dgvVoucher
            this.dgvVoucher.AllowUserToAddRows = false;
            this.dgvVoucher.AllowUserToDeleteRows = false;
            this.dgvVoucher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVoucher.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoucher.Location = new Point(6, 6);
            this.dgvVoucher.MultiSelect = false;
            this.dgvVoucher.Name = "dgvVoucher";
            this.dgvVoucher.ReadOnly = true;
            this.dgvVoucher.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvVoucher.Size = new Size(940, 460);
            this.dgvVoucher.TabIndex = 0;

            // btnDangXuat
            this.btnDangXuat.Location = new Point(897, 530);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new Size(75, 25);
            this.btnDangXuat.TabIndex = 1;
            this.btnDangXuat.Text = "Dang xuat";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new EventHandler(this.btnDangXuat_Click);

            // FormKhachHang
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(984, 561);
            this.Controls.Add(this.btnDangXuat);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormKhachHang";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Khach hang";
            this.WindowState = FormWindowState.Maximized;
            this.FormClosing += new FormClosingEventHandler(this.FormKhachHang_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabThongTin.ResumeLayout(false);
            this.tabLichSu.ResumeLayout(false);
            this.tabVoucher.ResumeLayout(false);
            this.groupThongTin.ResumeLayout(false);
            this.groupThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuGiaoDich)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).EndInit();
            this.ResumeLayout(false);
        }
    }
}