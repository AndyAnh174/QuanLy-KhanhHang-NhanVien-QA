namespace QuanLy_KhanhHang_NhanVien
{
    partial class FormTaoHoaDon
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtMaHD;
        private NumericUpDown numTongTien;
        private TextBox txtMaVoucher;
        private TextBox txtGhiChu;
        private Button btnTao;
        private Button btnHuy;
        private Label lblMaHD;
        private Label lblTongTien;
        private Label lblVoucher;
        private Label lblGhiChu;

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
            this.txtMaHD = new TextBox();
            this.numTongTien = new NumericUpDown();
            this.txtMaVoucher = new TextBox();
            this.txtGhiChu = new TextBox();
            this.btnTao = new Button();
            this.btnHuy = new Button();
            this.lblMaHD = new Label();
            this.lblTongTien = new Label();
            this.lblVoucher = new Label();
            this.lblGhiChu = new Label();
            
            ((System.ComponentModel.ISupportInitialize)(this.numTongTien)).BeginInit();
            this.SuspendLayout();

            // lblMaHD
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Location = new Point(20, 20);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new Size(80, 15);
            this.lblMaHD.TabIndex = 0;
            this.lblMaHD.Text = "Ma hoa don:";

            // txtMaHD
            this.txtMaHD.Location = new Point(120, 17);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.ReadOnly = true;
            this.txtMaHD.Size = new Size(200, 23);
            this.txtMaHD.TabIndex = 1;

            // lblTongTien
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new Point(20, 55);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new Size(63, 15);
            this.lblTongTien.TabIndex = 2;
            this.lblTongTien.Text = "Tong tien:";

            // numTongTien
            this.numTongTien.DecimalPlaces = 0;
            this.numTongTien.Location = new Point(120, 52);
            this.numTongTien.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            this.numTongTien.Name = "numTongTien";
            this.numTongTien.Size = new Size(150, 23);
            this.numTongTien.TabIndex = 3;

            // lblVoucher
            this.lblVoucher.AutoSize = true;
            this.lblVoucher.Location = new Point(20, 90);
            this.lblVoucher.Name = "lblVoucher";
            this.lblVoucher.Size = new Size(78, 15);
            this.lblVoucher.TabIndex = 4;
            this.lblVoucher.Text = "Ma voucher:";

            // txtMaVoucher
            this.txtMaVoucher.Location = new Point(120, 87);
            this.txtMaVoucher.Name = "txtMaVoucher";
            this.txtMaVoucher.PlaceholderText = "Nhap ma voucher (neu co)";
            this.txtMaVoucher.Size = new Size(200, 23);
            this.txtMaVoucher.TabIndex = 5;

            // lblGhiChu
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new Point(20, 125);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new Size(51, 15);
            this.lblGhiChu.TabIndex = 6;
            this.lblGhiChu.Text = "Ghi chu:";

            // txtGhiChu
            this.txtGhiChu.Location = new Point(120, 122);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new Size(200, 50);
            this.txtGhiChu.TabIndex = 7;

            // btnTao
            this.btnTao.BackColor = Color.Green;
            this.btnTao.ForeColor = Color.White;
            this.btnTao.Location = new Point(120, 190);
            this.btnTao.Name = "btnTao";
            this.btnTao.Size = new Size(75, 30);
            this.btnTao.TabIndex = 8;
            this.btnTao.Text = "Tao";
            this.btnTao.UseVisualStyleBackColor = false;
            this.btnTao.Click += new EventHandler(this.btnTao_Click);

            // btnHuy
            this.btnHuy.BackColor = Color.Gray;
            this.btnHuy.ForeColor = Color.White;
            this.btnHuy.Location = new Point(210, 190);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new Size(75, 30);
            this.btnHuy.TabIndex = 9;
            this.btnHuy.Text = "Huy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new EventHandler(this.btnHuy_Click);

            // FormTaoHoaDon
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(350, 240);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnTao);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.txtMaVoucher);
            this.Controls.Add(this.lblVoucher);
            this.Controls.Add(this.numTongTien);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.txtMaHD);
            this.Controls.Add(this.lblMaHD);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTaoHoaDon";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Tao hoa don moi";
            ((System.ComponentModel.ISupportInitialize)(this.numTongTien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}