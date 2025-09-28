namespace QuanLy_KhanhHang_NhanVien
{
    partial class FormThemKhachHang
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtMaND;
        private TextBox txtHoTen;
        private TextBox txtEmail;
        private TextBox txtSoDienThoai;
        private TextBox txtDiaChi;
        private TextBox txtCMND;
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private DateTimePicker dtpNgaySinh;
        private RadioButton rdoNam;
        private RadioButton rdoNu;
        private RadioButton rdoKhac;
        private Button btnThem;
        private Button btnHuy;
        private Label lblMaND;
        private Label lblHoTen;
        private Label lblEmail;
        private Label lblSoDienThoai;
        private Label lblDiaChi;
        private Label lblCMND;
        private Label lblTenDangNhap;
        private Label lblMatKhau;
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
            this.txtMaND = new TextBox();
            this.txtHoTen = new TextBox();
            this.txtEmail = new TextBox();
            this.txtSoDienThoai = new TextBox();
            this.txtDiaChi = new TextBox();
            this.txtCMND = new TextBox();
            this.txtTenDangNhap = new TextBox();
            this.txtMatKhau = new TextBox();
            this.dtpNgaySinh = new DateTimePicker();
            this.rdoNam = new RadioButton();
            this.rdoNu = new RadioButton();
            this.rdoKhac = new RadioButton();
            this.btnThem = new Button();
            this.btnHuy = new Button();
            this.lblMaND = new Label();
            this.lblHoTen = new Label();
            this.lblEmail = new Label();
            this.lblSoDienThoai = new Label();
            this.lblDiaChi = new Label();
            this.lblCMND = new Label();
            this.lblTenDangNhap = new Label();
            this.lblMatKhau = new Label();
            this.lblNgaySinh = new Label();
            this.lblGioiTinh = new Label();
            this.SuspendLayout();

            int y = 20, spacing = 35;

            // Ma ND
            this.lblMaND.AutoSize = true;
            this.lblMaND.Location = new Point(20, y);
            this.lblMaND.Size = new Size(50, 15);
            this.lblMaND.Text = "Ma KH:";
            this.txtMaND.Location = new Point(150, y - 3);
            this.txtMaND.ReadOnly = true;
            this.txtMaND.Size = new Size(200, 23);
            y += spacing;

            // Ho ten
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new Point(20, y);
            this.lblHoTen.Size = new Size(50, 15);
            this.lblHoTen.Text = "Ho ten *:";
            this.txtHoTen.Location = new Point(150, y - 3);
            this.txtHoTen.Size = new Size(200, 23);
            y += spacing;

            // Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new Point(20, y);
            this.lblEmail.Size = new Size(50, 15);
            this.lblEmail.Text = "Email:";
            this.txtEmail.Location = new Point(150, y - 3);
            this.txtEmail.Size = new Size(200, 23);
            y += spacing;

            // So dien thoai
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Location = new Point(20, y);
            this.lblSoDienThoai.Size = new Size(100, 15);
            this.lblSoDienThoai.Text = "So dien thoai *:";
            this.txtSoDienThoai.Location = new Point(150, y - 3);
            this.txtSoDienThoai.Size = new Size(200, 23);
            y += spacing;

            // Dia chi
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new Point(20, y);
            this.lblDiaChi.Size = new Size(50, 15);
            this.lblDiaChi.Text = "Dia chi:";
            this.txtDiaChi.Location = new Point(150, y - 3);
            this.txtDiaChi.Size = new Size(200, 50);
            this.txtDiaChi.Multiline = true;
            y += 65;

            // CMND
            this.lblCMND.AutoSize = true;
            this.lblCMND.Location = new Point(20, y);
            this.lblCMND.Size = new Size(50, 15);
            this.lblCMND.Text = "CMND:";
            this.txtCMND.Location = new Point(150, y - 3);
            this.txtCMND.Size = new Size(200, 23);
            y += spacing;

            // Ngay sinh
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Location = new Point(20, y);
            this.lblNgaySinh.Size = new Size(70, 15);
            this.lblNgaySinh.Text = "Ngay sinh:";
            this.dtpNgaySinh.Location = new Point(150, y - 3);
            this.dtpNgaySinh.Size = new Size(200, 23);
            y += spacing;

            // Gioi tinh
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Location = new Point(20, y);
            this.lblGioiTinh.Size = new Size(60, 15);
            this.lblGioiTinh.Text = "Gioi tinh:";

            this.rdoNam.AutoSize = true;
            this.rdoNam.Checked = true;
            this.rdoNam.Location = new Point(150, y);
            this.rdoNam.Size = new Size(51, 19);
            this.rdoNam.Text = "Nam";

            this.rdoNu.AutoSize = true;
            this.rdoNu.Location = new Point(210, y);
            this.rdoNu.Size = new Size(41, 19);
            this.rdoNu.Text = "Nu";

            this.rdoKhac.AutoSize = true;
            this.rdoKhac.Location = new Point(260, y);
            this.rdoKhac.Size = new Size(50, 19);
            this.rdoKhac.Text = "Khac";
            y += spacing;

            // Ten dang nhap
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Location = new Point(20, y);
            this.lblTenDangNhap.Size = new Size(110, 15);
            this.lblTenDangNhap.Text = "Ten dang nhap *:";
            this.txtTenDangNhap.Location = new Point(150, y - 3);
            this.txtTenDangNhap.Size = new Size(200, 23);
            y += spacing;

            // Mat khau
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Location = new Point(20, y);
            this.lblMatKhau.Size = new Size(70, 15);
            this.lblMatKhau.Text = "Mat khau *:";
            this.txtMatKhau.Location = new Point(150, y - 3);
            this.txtMatKhau.Size = new Size(200, 23);
            y += 50;

            // Buttons
            this.btnThem.BackColor = Color.Green;
            this.btnThem.ForeColor = Color.White;
            this.btnThem.Location = new Point(150, y);
            this.btnThem.Size = new Size(75, 30);
            this.btnThem.Text = "Them";
            this.btnThem.Click += new EventHandler(this.btnThem_Click);

            this.btnHuy.BackColor = Color.Gray;
            this.btnHuy.ForeColor = Color.White;
            this.btnHuy.Location = new Point(240, y);
            this.btnHuy.Size = new Size(75, 30);
            this.btnHuy.Text = "Huy";
            this.btnHuy.Click += new EventHandler(this.btnHuy_Click);

            // Form
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(400, y + 70);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.rdoKhac);
            this.Controls.Add(this.rdoNu);
            this.Controls.Add(this.rdoNam);
            this.Controls.Add(this.lblGioiTinh);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.lblNgaySinh);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.lblMatKhau);
            this.Controls.Add(this.txtTenDangNhap);
            this.Controls.Add(this.lblTenDangNhap);
            this.Controls.Add(this.txtCMND);
            this.Controls.Add(this.lblCMND);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.lblDiaChi);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.lblSoDienThoai);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.txtMaND);
            this.Controls.Add(this.lblMaND);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormThemKhachHang";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Them khach hang moi";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}