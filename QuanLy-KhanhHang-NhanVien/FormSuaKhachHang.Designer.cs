namespace QuanLy_KhanhHang_NhanVien
{
    partial class FormSuaKhachHang
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtHoTen;
        private TextBox txtEmail;
        private TextBox txtSoDienThoai;
        private TextBox txtDiaChi;
        private DateTimePicker dtpNgaySinh;
        private RadioButton rdoNam;
        private RadioButton rdoNu;
        private RadioButton rdoKhac;
        private Button btnLuu;
        private Button btnHuy;
        private Label lblHoTen;
        private Label lblEmail;
        private Label lblSoDienThoai;
        private Label lblDiaChi;
        private Label lblNgaySinh;
        private Label lblGioiTinh;

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
            this.lblHoTen = new Label();
            this.lblEmail = new Label();
            this.lblSoDienThoai = new Label();
            this.lblDiaChi = new Label();
            this.lblNgaySinh = new Label();
            this.lblGioiTinh = new Label();
            this.txtHoTen = new TextBox();
            this.txtEmail = new TextBox();
            this.txtSoDienThoai = new TextBox();
            this.txtDiaChi = new TextBox();
            this.dtpNgaySinh = new DateTimePicker();
            this.rdoNam = new RadioButton();
            this.rdoNu = new RadioButton();
            this.rdoKhac = new RadioButton();
            this.btnLuu = new Button();
            this.btnHuy = new Button();
            this.SuspendLayout();

            // lblHoTen
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new Point(20, 20);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new Size(50, 15);
            this.lblHoTen.TabIndex = 0;
            this.lblHoTen.Text = "Ho ten:";

            // txtHoTen
            this.txtHoTen.Location = new Point(120, 17);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new Size(200, 23);
            this.txtHoTen.TabIndex = 1;

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new Point(20, 60);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(39, 15);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email:";

            // txtEmail
            this.txtEmail.Location = new Point(120, 57);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(200, 23);
            this.txtEmail.TabIndex = 3;

            // lblSoDienThoai
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Location = new Point(20, 100);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new Size(85, 15);
            this.lblSoDienThoai.TabIndex = 4;
            this.lblSoDienThoai.Text = "So dien thoai:";

            // txtSoDienThoai
            this.txtSoDienThoai.Location = new Point(120, 97);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new Size(200, 23);
            this.txtSoDienThoai.TabIndex = 5;

            // lblDiaChi
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new Point(20, 140);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new Size(48, 15);
            this.lblDiaChi.TabIndex = 6;
            this.lblDiaChi.Text = "Dia chi:";

            // txtDiaChi
            this.txtDiaChi.Location = new Point(120, 137);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new Size(200, 50);
            this.txtDiaChi.TabIndex = 7;

            // lblNgaySinh
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Location = new Point(20, 210);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new Size(64, 15);
            this.lblNgaySinh.TabIndex = 8;
            this.lblNgaySinh.Text = "Ngay sinh:";

            // dtpNgaySinh
            this.dtpNgaySinh.Location = new Point(120, 207);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new Size(200, 23);
            this.dtpNgaySinh.TabIndex = 9;

            // lblGioiTinh
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Location = new Point(20, 250);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new Size(58, 15);
            this.lblGioiTinh.TabIndex = 10;
            this.lblGioiTinh.Text = "Gioi tinh:";

            // rdoNam
            this.rdoNam.AutoSize = true;
            this.rdoNam.Location = new Point(120, 248);
            this.rdoNam.Name = "rdoNam";
            this.rdoNam.Size = new Size(51, 19);
            this.rdoNam.TabIndex = 11;
            this.rdoNam.TabStop = true;
            this.rdoNam.Text = "Nam";
            this.rdoNam.UseVisualStyleBackColor = true;

            // rdoNu
            this.rdoNu.AutoSize = true;
            this.rdoNu.Location = new Point(180, 248);
            this.rdoNu.Name = "rdoNu";
            this.rdoNu.Size = new Size(41, 19);
            this.rdoNu.TabIndex = 12;
            this.rdoNu.TabStop = true;
            this.rdoNu.Text = "Nu";
            this.rdoNu.UseVisualStyleBackColor = true;

            // rdoKhac
            this.rdoKhac.AutoSize = true;
            this.rdoKhac.Location = new Point(230, 248);
            this.rdoKhac.Name = "rdoKhac";
            this.rdoKhac.Size = new Size(50, 19);
            this.rdoKhac.TabIndex = 13;
            this.rdoKhac.TabStop = true;
            this.rdoKhac.Text = "Khac";
            this.rdoKhac.UseVisualStyleBackColor = true;

            // btnLuu
            this.btnLuu.BackColor = Color.Green;
            this.btnLuu.ForeColor = Color.White;
            this.btnLuu.Location = new Point(120, 300);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new Size(75, 30);
            this.btnLuu.TabIndex = 14;
            this.btnLuu.Text = "Luu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new EventHandler(this.btnLuu_Click);

            // btnHuy
            this.btnHuy.BackColor = Color.Gray;
            this.btnHuy.ForeColor = Color.White;
            this.btnHuy.Location = new Point(210, 300);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new Size(75, 30);
            this.btnHuy.TabIndex = 15;
            this.btnHuy.Text = "Huy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new EventHandler(this.btnHuy_Click);

            // FormSuaKhachHang
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(350, 350);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.rdoKhac);
            this.Controls.Add(this.rdoNu);
            this.Controls.Add(this.rdoNam);
            this.Controls.Add(this.lblGioiTinh);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.lblNgaySinh);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.lblDiaChi);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.lblSoDienThoai);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblHoTen);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormSuaKhachHang";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Sua thong tin khach hang";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}