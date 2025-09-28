namespace QuanLy_KhanhHang_NhanVien
{
    partial class FormNhanVien
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl1;
        private TabPage tabKhachHang;
        private TabPage tabVoucher;
        private TabPage tabGiaoDich;
        private TabPage tabTuongTac;
        
        // Tab Khach Hang
        private DataGridView dgvKhachHang;
        private TextBox txtTimKiem;
        private Button btnTimKiem;
        private Button btnSuaKhachHang;
        private Button btnXoaKhachHang;
        
        // Tab Voucher
        private DataGridView dgvVoucher;
        private Button btnThemVoucher;
        
        // Tab Giao Dich
        private DataGridView dgvGiaoDich;
        
        // Tab Tuong Tac
        private DataGridView dgvTuongTac;
        private Button btnPhanHoi;
        
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
            this.tabKhachHang = new TabPage();
            this.tabVoucher = new TabPage();
            this.tabGiaoDich = new TabPage();
            this.tabTuongTac = new TabPage();
            
            // Khach Hang tab controls
            this.dgvKhachHang = new DataGridView();
            this.txtTimKiem = new TextBox();
            this.btnTimKiem = new Button();
            this.btnSuaKhachHang = new Button();
            this.btnXoaKhachHang = new Button();
            
            // Voucher tab controls
            this.dgvVoucher = new DataGridView();
            this.btnThemVoucher = new Button();
            
            // Giao Dich tab controls
            this.dgvGiaoDich = new DataGridView();
            
            // Tuong Tac tab controls
            this.dgvTuongTac = new DataGridView();
            this.btnPhanHoi = new Button();
            
            this.btnDangXuat = new Button();

            this.tabControl1.SuspendLayout();
            this.tabKhachHang.SuspendLayout();
            this.tabVoucher.SuspendLayout();
            this.tabGiaoDich.SuspendLayout();
            this.tabTuongTac.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTuongTac)).BeginInit();
            this.SuspendLayout();

            // tabControl1
            this.tabControl1.Controls.Add(this.tabKhachHang);
            this.tabControl1.Controls.Add(this.tabVoucher);
            this.tabControl1.Controls.Add(this.tabGiaoDich);
            this.tabControl1.Controls.Add(this.tabTuongTac);
            this.tabControl1.Location = new Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(960, 500);
            this.tabControl1.TabIndex = 0;

            // tabKhachHang
            this.tabKhachHang.Controls.Add(this.btnXoaKhachHang);
            this.tabKhachHang.Controls.Add(this.btnSuaKhachHang);
            this.tabKhachHang.Controls.Add(this.btnTimKiem);
            this.tabKhachHang.Controls.Add(this.txtTimKiem);
            this.tabKhachHang.Controls.Add(this.dgvKhachHang);
            this.tabKhachHang.Location = new Point(4, 24);
            this.tabKhachHang.Name = "tabKhachHang";
            this.tabKhachHang.Padding = new Padding(3);
            this.tabKhachHang.Size = new Size(952, 472);
            this.tabKhachHang.TabIndex = 0;
            this.tabKhachHang.Text = "Quan ly Khach hang";
            this.tabKhachHang.UseVisualStyleBackColor = true;

            // txtTimKiem
            this.txtTimKiem.Location = new Point(6, 15);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Nhap ten khach hang can tim...";
            this.txtTimKiem.Size = new Size(300, 23);
            this.txtTimKiem.TabIndex = 0;

            // btnTimKiem
            this.btnTimKiem.Location = new Point(320, 14);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new Size(75, 25);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tim kiem";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new EventHandler(this.btnTimKiem_Click);

            // btnSuaKhachHang
            this.btnSuaKhachHang.Location = new Point(410, 14);
            this.btnSuaKhachHang.Name = "btnSuaKhachHang";
            this.btnSuaKhachHang.Size = new Size(75, 25);
            this.btnSuaKhachHang.TabIndex = 2;
            this.btnSuaKhachHang.Text = "Sua";
            this.btnSuaKhachHang.UseVisualStyleBackColor = true;
            this.btnSuaKhachHang.Click += new EventHandler(this.btnSuaKhachHang_Click);

            // btnXoaKhachHang
            this.btnXoaKhachHang.Location = new Point(500, 14);
            this.btnXoaKhachHang.Name = "btnXoaKhachHang";
            this.btnXoaKhachHang.Size = new Size(75, 25);
            this.btnXoaKhachHang.TabIndex = 3;
            this.btnXoaKhachHang.Text = "Xoa";
            this.btnXoaKhachHang.UseVisualStyleBackColor = true;
            this.btnXoaKhachHang.Click += new EventHandler(this.btnXoaKhachHang_Click);

            // dgvKhachHang
            this.dgvKhachHang.AllowUserToAddRows = false;
            this.dgvKhachHang.AllowUserToDeleteRows = false;
            this.dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhachHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhachHang.Location = new Point(6, 50);
            this.dgvKhachHang.MultiSelect = false;
            this.dgvKhachHang.Name = "dgvKhachHang";
            this.dgvKhachHang.ReadOnly = true;
            this.dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhachHang.Size = new Size(940, 416);
            this.dgvKhachHang.TabIndex = 4;

            // tabVoucher
            this.tabVoucher.Controls.Add(this.btnThemVoucher);
            this.tabVoucher.Controls.Add(this.dgvVoucher);
            this.tabVoucher.Location = new Point(4, 24);
            this.tabVoucher.Name = "tabVoucher";
            this.tabVoucher.Padding = new Padding(3);
            this.tabVoucher.Size = new Size(952, 472);
            this.tabVoucher.TabIndex = 1;
            this.tabVoucher.Text = "Quan ly Voucher";
            this.tabVoucher.UseVisualStyleBackColor = true;

            // btnThemVoucher
            this.btnThemVoucher.Location = new Point(6, 15);
            this.btnThemVoucher.Name = "btnThemVoucher";
            this.btnThemVoucher.Size = new Size(100, 25);
            this.btnThemVoucher.TabIndex = 0;
            this.btnThemVoucher.Text = "Them voucher";
            this.btnThemVoucher.UseVisualStyleBackColor = true;
            this.btnThemVoucher.Click += new EventHandler(this.btnThemVoucher_Click);

            // dgvVoucher
            this.dgvVoucher.AllowUserToAddRows = false;
            this.dgvVoucher.AllowUserToDeleteRows = false;
            this.dgvVoucher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVoucher.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoucher.Location = new Point(6, 50);
            this.dgvVoucher.MultiSelect = false;
            this.dgvVoucher.Name = "dgvVoucher";
            this.dgvVoucher.ReadOnly = true;
            this.dgvVoucher.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvVoucher.Size = new Size(940, 416);
            this.dgvVoucher.TabIndex = 1;

            // tabGiaoDich
            this.tabGiaoDich.Controls.Add(this.dgvGiaoDich);
            this.tabGiaoDich.Location = new Point(4, 24);
            this.tabGiaoDich.Name = "tabGiaoDich";
            this.tabGiaoDich.Size = new Size(952, 472);
            this.tabGiaoDich.TabIndex = 2;
            this.tabGiaoDich.Text = "Lich su Giao dich";
            this.tabGiaoDich.UseVisualStyleBackColor = true;

            // dgvGiaoDich
            this.dgvGiaoDich.AllowUserToAddRows = false;
            this.dgvGiaoDich.AllowUserToDeleteRows = false;
            this.dgvGiaoDich.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGiaoDich.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiaoDich.Location = new Point(6, 6);
            this.dgvGiaoDich.MultiSelect = false;
            this.dgvGiaoDich.Name = "dgvGiaoDich";
            this.dgvGiaoDich.ReadOnly = true;
            this.dgvGiaoDich.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvGiaoDich.Size = new Size(940, 460);
            this.dgvGiaoDich.TabIndex = 0;

            // tabTuongTac
            this.tabTuongTac.Controls.Add(this.btnPhanHoi);
            this.tabTuongTac.Controls.Add(this.dgvTuongTac);
            this.tabTuongTac.Location = new Point(4, 24);
            this.tabTuongTac.Name = "tabTuongTac";
            this.tabTuongTac.Size = new Size(952, 472);
            this.tabTuongTac.TabIndex = 3;
            this.tabTuongTac.Text = "Tuong tac Khach hang";
            this.tabTuongTac.UseVisualStyleBackColor = true;

            // btnPhanHoi
            this.btnPhanHoi.Location = new Point(6, 15);
            this.btnPhanHoi.Name = "btnPhanHoi";
            this.btnPhanHoi.Size = new Size(75, 25);
            this.btnPhanHoi.TabIndex = 0;
            this.btnPhanHoi.Text = "Phan hoi";
            this.btnPhanHoi.UseVisualStyleBackColor = true;
            this.btnPhanHoi.Click += new EventHandler(this.btnPhanHoi_Click);

            // dgvTuongTac
            this.dgvTuongTac.AllowUserToAddRows = false;
            this.dgvTuongTac.AllowUserToDeleteRows = false;
            this.dgvTuongTac.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTuongTac.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTuongTac.Location = new Point(6, 50);
            this.dgvTuongTac.MultiSelect = false;
            this.dgvTuongTac.Name = "dgvTuongTac";
            this.dgvTuongTac.ReadOnly = true;
            this.dgvTuongTac.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTuongTac.Size = new Size(940, 416);
            this.dgvTuongTac.TabIndex = 1;

            // btnDangXuat
            this.btnDangXuat.Location = new Point(897, 530);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new Size(75, 25);
            this.btnDangXuat.TabIndex = 1;
            this.btnDangXuat.Text = "Dang xuat";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new EventHandler(this.btnDangXuat_Click);

            // FormNhanVien
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(984, 561);
            this.Controls.Add(this.btnDangXuat);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormNhanVien";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quan ly Nhan vien";
            this.WindowState = FormWindowState.Maximized;
            this.FormClosing += new FormClosingEventHandler(this.FormNhanVien_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabKhachHang.ResumeLayout(false);
            this.tabKhachHang.PerformLayout();
            this.tabVoucher.ResumeLayout(false);
            this.tabGiaoDich.ResumeLayout(false);
            this.tabTuongTac.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTuongTac)).EndInit();
            this.ResumeLayout(false);
        }
    }
}