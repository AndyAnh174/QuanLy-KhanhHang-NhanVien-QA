# MÔ TẢ CHỨC NĂNG HỆ THỐNG QUẢN LÝ KHÁCH HÀNG

## 🔐 Đăng nhập hệ thống
Khi đăng nhập, tùy vào account nhân viên hay khách hàng sẽ hiển thị giao diện tương ứng.

---

## 👨‍💼 **1. CHỨC NĂNG CHO NHÂN VIÊN**

### 📋 **Quản lý thông tin khách hàng**
- ✅ Chỉnh sửa thông tin có sẵn (cần xây function)
- ✅ Xem danh sách khách hàng, tìm kiếm khách hàng dựa theo tên
- ✅ Xóa khách hàng, sửa lại mật khẩu

### 🎫 **Quản lý voucher**
- ✅ Thêm voucher mới (điền các thông tin cần có: ngày hiệu lực, ngày kết thúc, giá trị giảm...)
- ✅ Có thể đặt điều kiện cho voucher dựa trên hạng thành viên
  - *Ví dụ: voucher giảm 25% chỉ áp dụng cho thành viên hạng vàng*
  - *Ví dụ: voucher 5% cho tất cả thành viên*

### 💰 **Quản lý giao dịch**
- ✅ Xem lại lịch sử giao dịch của tất cả
- ✅ Mỗi khi hóa đơn được thanh toán thì sẽ thêm 1 bản ghi vào quản lý giao dịch
- ✅ **Thông tin hiển thị:**
  - Mã giao dịch
  - Tên khách hàng
  - Thành tiền
  - Voucher sử dụng
  - Hạng thành viên của khách hàng
  - Ngày tháng giờ giao dịch

### 💬 **Tương tác khách hàng**
- ✅ Nhân viên sẽ nhận tin nhắn từ khách hàng
- ✅ **Thông tin tin nhắn:**
  - Tên khách hàng
  - Nội dung
  - Ngày giờ
- ✅ Nhân viên sẽ nhấn vào để phản hồi lại khách hàng

---

## 👤 **2. CHỨC NĂNG CHO KHÁCH HÀNG**

### 🛒 **Chi tiết hóa đơn**
- ✅ Mỗi hóa đơn có 1 tác động để phát sinh hóa đơn
- ✅ Có thể tạo giỏ hàng (gồm sản phẩm điện tử như iPhone, Laptop...)
- ✅ Lưu giỏ hàng để khi nhấn thanh toán thì hóa đơn sẽ được phát sinh
- ✅ Áp voucher, giá trị hóa đơn sẽ được giảm

### 📊 **Theo dõi lịch sử giao dịch**
- ✅ Khách hàng có thể xem lại giao dịch của mình đã thanh toán
- ✅ **Thông tin hiển thị:**
  - Thông tin khách hàng
  - Voucher áp dụng
  - Ngày giờ thanh toán
  - Sản phẩm đã mua
  - Thành tiền

### 👤 **Xem thông tin tài khoản**
- ✅ Xem lại phần họ tên, tuổi...
- ✅ Xem hạng thành viên

### 🏆 **Hệ thống tích lũy điểm**
- ✅ **Quy đổi:** 0.1% giá trị hóa đơn thành điểm
- ✅ **Ví dụ:** hóa đơn 5.000.000 VND tích lũy được 500 điểm

#### **Các mức hạng thành viên (ĐÃ CẬP NHẬT):**
| Điểm | Hạng | Ưu đãi |
|------|------|---------|
| 0 điểm | 🥉 Đồng | Không có ưu đãi đặc biệt |
| 750 điểm | 🥈 Bạc | Giảm giá 5% cho tất cả sản phẩm |
| 1500 điểm | 🥇 Vàng | Giảm giá 10% + miễn phí vận chuyển |
| 2000 điểm | 💎 Kim cương | Giảm giá 15% + ưu đãi đặc biệt |

- ✅ Khách hàng sẽ biết mình đang ở hạng nào và cần thêm bao nhiêu điểm để lên hạng tiếp theo

### 📞 **Liên hệ tư vấn**
- ✅ Khách hàng có thể gửi câu hỏi thắc mắc
- ✅ Tin nhắn được gửi qua chức năng tương tác khách hàng và nhân viên sẽ phản hồi

---

## 🎯 **TỔNG KẾT**
Hệ thống cung cấp đầy đủ chức năng cho cả nhân viên và khách hàng với giao diện phân quyền rõ ràng, đảm bảo trải nghiệm người dùng tối ưu.

### 📈 **Lưu ý về điểm tích lũy:**
- **Tỷ lệ tích điểm**: 0.1% (1 triệu VND = 100 điểm)
- **Mức điểm đã được tăng** để tạo động lực mua sắm lâu dài
- **Hạng cao hơn** = **Ưu đãi tốt hơn** = **Khuyến khích khách hàng trung thành**