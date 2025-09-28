namespace QuanLy_KhanhHang_NhanVien
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private Button btnDangNhap;
        private Button btnThoat;
        private Label lblTenDangNhap;
        private Label lblMatKhau;
        private Label lblTitle;

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
            this.lblTitle = new Label();
            this.lblTenDangNhap = new Label();
            this.lblMatKhau = new Label();
            this.txtTenDangNhap = new TextBox();
            this.txtMatKhau = new TextBox();
            this.btnDangNhap = new Button();
            this.btnThoat = new Button();
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTitle.ForeColor = Color.Blue;
            this.lblTitle.Location = new Point(80, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(240, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DANG NHAP HE THONG";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            
            // lblTenDangNhap
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Location = new Point(50, 90);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new Size(97, 15);
            this.lblTenDangNhap.TabIndex = 1;
            this.lblTenDangNhap.Text = "Ten dang nhap:";
            
            // txtTenDangNhap
            this.txtTenDangNhap.Location = new Point(160, 87);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new Size(200, 23);
            this.txtTenDangNhap.TabIndex = 2;
            
            // lblMatKhau
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Location = new Point(50, 130);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new Size(61, 15);
            this.lblMatKhau.TabIndex = 3;
            this.lblMatKhau.Text = "Mat khau:";
            
            // txtMatKhau
            this.txtMatKhau.Location = new Point(160, 127);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new Size(200, 23);
            this.txtMatKhau.TabIndex = 4;
            this.txtMatKhau.UseSystemPasswordChar = true;
            
            // btnDangNhap
            this.btnDangNhap.BackColor = Color.DodgerBlue;
            this.btnDangNhap.ForeColor = Color.White;
            this.btnDangNhap.Location = new Point(110, 180);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new Size(100, 35);
            this.btnDangNhap.TabIndex = 5;
            this.btnDangNhap.Text = "Dang nhap";
            this.btnDangNhap.UseVisualStyleBackColor = false;
            this.btnDangNhap.Click += new EventHandler(this.btnDangNhap_Click);
            
            // btnThoat
            this.btnThoat.BackColor = Color.Gray;
            this.btnThoat.ForeColor = Color.White;
            this.btnThoat.Location = new Point(230, 180);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new Size(100, 35);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoat";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new EventHandler(this.btnThoat_Click);
            
            // Form1
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(400, 250);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.lblMatKhau);
            this.Controls.Add(this.txtTenDangNhap);
            this.Controls.Add(this.lblTenDangNhap);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Dang Nhap";
            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
