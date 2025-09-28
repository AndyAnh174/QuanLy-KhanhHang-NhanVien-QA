namespace QuanLy_KhanhHang_NhanVien
{
    partial class FormThemVoucher
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtMaVoucher;
        private TextBox txtTenVoucher;
        private TextBox txtMoTa;
        private NumericUpDown numGiaTriGiam;
        private RadioButton rdoPhanTram;
        private RadioButton rdoTienMat;
        private DateTimePicker dtpNgayBatDau;
        private DateTimePicker dtpNgayKetThuc;
        private NumericUpDown numSoLuong;
        private NumericUpDown numDiemToiThieu;
        private ComboBox cboHangApDung;
        private Button btnThem;
        private Button btnHuy;
        private Label lblMaVoucher;
        private Label lblTenVoucher;
        private Label lblMoTa;
        private Label lblGiaTriGiam;
        private Label lblLoaiGiam;
        private Label lblNgayBatDau;
        private Label lblNgayKetThuc;
        private Label lblSoLuong;
        private Label lblDiemToiThieu;
        private Label lblHangApDung;

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
            this.txtMaVoucher = new TextBox();
            this.txtTenVoucher = new TextBox();
            this.txtMoTa = new TextBox();
            this.numGiaTriGiam = new NumericUpDown();
            this.rdoPhanTram = new RadioButton();
            this.rdoTienMat = new RadioButton();
            this.dtpNgayBatDau = new DateTimePicker();
            this.dtpNgayKetThuc = new DateTimePicker();
            this.numSoLuong = new NumericUpDown();
            this.numDiemToiThieu = new NumericUpDown();
            this.cboHangApDung = new ComboBox();
            this.btnThem = new Button();
            this.btnHuy = new Button();
            this.lblMaVoucher = new Label();
            this.lblTenVoucher = new Label();
            this.lblMoTa = new Label();
            this.lblGiaTriGiam = new Label();
            this.lblLoaiGiam = new Label();
            this.lblNgayBatDau = new Label();
            this.lblNgayKetThuc = new Label();
            this.lblSoLuong = new Label();
            this.lblDiemToiThieu = new Label();
            this.lblHangApDung = new Label();
            
            ((System.ComponentModel.ISupportInitialize)(this.numGiaTriGiam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiemToiThieu)).BeginInit();
            this.SuspendLayout();

            // lblMaVoucher
            this.lblMaVoucher.AutoSize = true;
            this.lblMaVoucher.Location = new Point(20, 20);
            this.lblMaVoucher.Name = "lblMaVoucher";
            this.lblMaVoucher.Size = new Size(79, 15);
            this.lblMaVoucher.TabIndex = 0;
            this.lblMaVoucher.Text = "Ma voucher:";

            // txtMaVoucher
            this.txtMaVoucher.Location = new Point(140, 17);
            this.txtMaVoucher.Name = "txtMaVoucher";
            this.txtMaVoucher.Size = new Size(200, 23);
            this.txtMaVoucher.TabIndex = 1;

            // lblTenVoucher
            this.lblTenVoucher.AutoSize = true;
            this.lblTenVoucher.Location = new Point(20, 55);
            this.lblTenVoucher.Name = "lblTenVoucher";
            this.lblTenVoucher.Size = new Size(81, 15);
            this.lblTenVoucher.TabIndex = 2;
            this.lblTenVoucher.Text = "Ten voucher:";

            // txtTenVoucher
            this.txtTenVoucher.Location = new Point(140, 52);
            this.txtTenVoucher.Name = "txtTenVoucher";
            this.txtTenVoucher.Size = new Size(200, 23);
            this.txtTenVoucher.TabIndex = 3;

            // lblMoTa
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new Point(20, 90);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new Size(42, 15);
            this.lblMoTa.TabIndex = 4;
            this.lblMoTa.Text = "Mo ta:";

            // txtMoTa
            this.txtMoTa.Location = new Point(140, 87);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new Size(200, 23);
            this.txtMoTa.TabIndex = 5;

            // lblGiaTriGiam
            this.lblGiaTriGiam.AutoSize = true;
            this.lblGiaTriGiam.Location = new Point(20, 125);
            this.lblGiaTriGiam.Name = "lblGiaTriGiam";
            this.lblGiaTriGiam.Size = new Size(78, 15);
            this.lblGiaTriGiam.TabIndex = 6;
            this.lblGiaTriGiam.Text = "Gia tri giam:";

            // numGiaTriGiam
            this.numGiaTriGiam.Location = new Point(140, 122);
            this.numGiaTriGiam.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            this.numGiaTriGiam.Name = "numGiaTriGiam";
            this.numGiaTriGiam.Size = new Size(100, 23);
            this.numGiaTriGiam.TabIndex = 7;

            // lblLoaiGiam
            this.lblLoaiGiam.AutoSize = true;
            this.lblLoaiGiam.Location = new Point(20, 160);
            this.lblLoaiGiam.Name = "lblLoaiGiam";
            this.lblLoaiGiam.Size = new Size(64, 15);
            this.lblLoaiGiam.TabIndex = 8;
            this.lblLoaiGiam.Text = "Loai giam:";

            // rdoTienMat
            this.rdoTienMat.AutoSize = true;
            this.rdoTienMat.Checked = true;
            this.rdoTienMat.Location = new Point(140, 158);
            this.rdoTienMat.Name = "rdoTienMat";
            this.rdoTienMat.Size = new Size(72, 19);
            this.rdoTienMat.TabIndex = 9;
            this.rdoTienMat.TabStop = true;
            this.rdoTienMat.Text = "Tien mat";
            this.rdoTienMat.UseVisualStyleBackColor = true;

            // rdoPhanTram
            this.rdoPhanTram.AutoSize = true;
            this.rdoPhanTram.Location = new Point(220, 158);
            this.rdoPhanTram.Name = "rdoPhanTram";
            this.rdoPhanTram.Size = new Size(84, 19);
            this.rdoPhanTram.TabIndex = 10;
            this.rdoPhanTram.Text = "Phan tram";
            this.rdoPhanTram.UseVisualStyleBackColor = true;

            // lblNgayBatDau
            this.lblNgayBatDau.AutoSize = true;
            this.lblNgayBatDau.Location = new Point(20, 195);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new Size(87, 15);
            this.lblNgayBatDau.TabIndex = 11;
            this.lblNgayBatDau.Text = "Ngay bat dau:";

            // dtpNgayBatDau
            this.dtpNgayBatDau.Location = new Point(140, 192);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new Size(200, 23);
            this.dtpNgayBatDau.TabIndex = 12;

            // lblNgayKetThuc
            this.lblNgayKetThuc.AutoSize = true;
            this.lblNgayKetThuc.Location = new Point(20, 230);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new Size(92, 15);
            this.lblNgayKetThuc.TabIndex = 13;
            this.lblNgayKetThuc.Text = "Ngay ket thuc:";

            // dtpNgayKetThuc
            this.dtpNgayKetThuc.Location = new Point(140, 227);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new Size(200, 23);
            this.dtpNgayKetThuc.TabIndex = 14;

            // lblSoLuong
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new Point(20, 265);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new Size(62, 15);
            this.lblSoLuong.TabIndex = 15;
            this.lblSoLuong.Text = "So luong:";

            // numSoLuong
            this.numSoLuong.Location = new Point(140, 262);
            this.numSoLuong.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.numSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new Size(100, 23);
            this.numSoLuong.TabIndex = 16;
            this.numSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // lblDiemToiThieu
            this.lblDiemToiThieu.AutoSize = true;
            this.lblDiemToiThieu.Location = new Point(20, 300);
            this.lblDiemToiThieu.Name = "lblDiemToiThieu";
            this.lblDiemToiThieu.Size = new Size(95, 15);
            this.lblDiemToiThieu.TabIndex = 17;
            this.lblDiemToiThieu.Text = "Diem toi thieu:";

            // numDiemToiThieu
            this.numDiemToiThieu.Location = new Point(140, 297);
            this.numDiemToiThieu.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.numDiemToiThieu.Name = "numDiemToiThieu";
            this.numDiemToiThieu.Size = new Size(100, 23);
            this.numDiemToiThieu.TabIndex = 18;

            // lblHangApDung
            this.lblHangApDung.AutoSize = true;
            this.lblHangApDung.Location = new Point(20, 335);
            this.lblHangApDung.Name = "lblHangApDung";
            this.lblHangApDung.Size = new Size(92, 15);
            this.lblHangApDung.TabIndex = 19;
            this.lblHangApDung.Text = "Hang ap dung:";

            // cboHangApDung
            this.cboHangApDung.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboHangApDung.Location = new Point(140, 332);
            this.cboHangApDung.Name = "cboHangApDung";
            this.cboHangApDung.Size = new Size(200, 23);
            this.cboHangApDung.TabIndex = 20;

            // btnThem
            this.btnThem.BackColor = Color.Green;
            this.btnThem.ForeColor = Color.White;
            this.btnThem.Location = new Point(140, 380);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new Size(75, 30);
            this.btnThem.TabIndex = 21;
            this.btnThem.Text = "Them";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new EventHandler(this.btnThem_Click);

            // btnHuy
            this.btnHuy.BackColor = Color.Gray;
            this.btnHuy.ForeColor = Color.White;
            this.btnHuy.Location = new Point(230, 380);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new Size(75, 30);
            this.btnHuy.TabIndex = 22;
            this.btnHuy.Text = "Huy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new EventHandler(this.btnHuy_Click);

            // FormThemVoucher
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(380, 430);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.cboHangApDung);
            this.Controls.Add(this.lblHangApDung);
            this.Controls.Add(this.numDiemToiThieu);
            this.Controls.Add(this.lblDiemToiThieu);
            this.Controls.Add(this.numSoLuong);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.dtpNgayKetThuc);
            this.Controls.Add(this.lblNgayKetThuc);
            this.Controls.Add(this.dtpNgayBatDau);
            this.Controls.Add(this.lblNgayBatDau);
            this.Controls.Add(this.rdoPhanTram);
            this.Controls.Add(this.rdoTienMat);
            this.Controls.Add(this.lblLoaiGiam);
            this.Controls.Add(this.numGiaTriGiam);
            this.Controls.Add(this.lblGiaTriGiam);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.txtTenVoucher);
            this.Controls.Add(this.lblTenVoucher);
            this.Controls.Add(this.txtMaVoucher);
            this.Controls.Add(this.lblMaVoucher);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormThemVoucher";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Them voucher moi";
            ((System.ComponentModel.ISupportInitialize)(this.numGiaTriGiam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiemToiThieu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}