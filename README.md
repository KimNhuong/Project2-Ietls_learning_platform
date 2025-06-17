# 📘 IELTS Learning Platform

Một hệ thống học IELTS toàn diện, giúp người học luyện tập hiệu quả thông qua các bài test, từ vựng, và tính năng quản lý của admin. Dự án bao gồm frontend bằng **Vue.js**, backend bằng **ASP.NET Core**, kết nối với cơ sở dữ liệu **MySQL** và tích hợp AI chấm điểm tự động bằng **DeepSeek**.

---

## 🚀 Tính năng chính

### 👤 Người dùng
- Đăng ký / Đăng nhập
- Làm bài kiểm tra theo kỹ năng
- Tra cứu từ vựng và lưu flashcard
- Xem lại kết quả sau mỗi bài test
- **Chấm điểm và phản hồi tự động (DeepSeek tích hợp cho phần Writing & Speaking)**

### 🔐 Quản trị viên
- Quản lý ngân hàng câu hỏi (thêm/sửa/xóa)
- Tạo và chỉnh sửa bài test
- Quản lý tài khoản người dùng

---

## ⚙️ Công nghệ sử dụng

| Thành phần    | Công nghệ              |
|---------------|------------------------|
| Frontend      | Vue.js (Composition API, Pinia, Vue Router) |
| Backend       | ASP.NET Core Web API (.NET 8) |
| Cơ sở dữ liệu | MySQL                  |
| ORM           | Entity Framework Core  |
| Authentication| JWT (JSON Web Token)   |
| AI Service    | DeepSeek API           |
| Dev Tools     | Postman, Swagger, Git  |

---

## 💠 Cài đặt & chạy dự án

### 🔹 1. Clone source code
```bash
git clone https://github.com/yourusername/ielts-learning-platform.git
cd ielts-learning-platform
```

---

### 🔹 2. Cài đặt Backend (.NET)

```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

#### ⚠️ Yêu cầu:
- .NET SDK >= 8.0
- MySQL chạy sẵn (port mặc định 3306)
- File cấu hình: `appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=ieltsdb;user=root;password=yourpassword"
}
```

> Để tích hợp DeepSeek, cần cấu hình `DeepSeekApiKey` trong biến môi trường hoặc `appsettings.json`.

---

### 🔹 3. Cài đặt Frontend (Vue.js)

```bash
cd frontend
npm install
npm run dev
```

#### ⚠️ Cấu hình endpoint API
Trong file `.env` hoặc `src/config/index.js`:
```js
VITE_API_URL=http://localhost:5000/api
```

---

## 📂 Cấu trúc thư mục

```
.
├── backend
│   ├── Controllers/
│   ├── DTOs/
│   ├── Services/
│   ├── Models/
│   └── Program.cs
│
├── frontend
│   ├── src/
│   │   ├── views/
│   │   ├── components/
│   │   └── stores/
│   └── vite.config.js
```

---

## 📈 Định hướng phát triển

- 🔮 Tích hợp AI Chatbot hỗ trợ học tập
- 🌐 Đăng nhập bằng Google/Facebook
- 📊 Phân tích hiệu suất học tập và đề xuất lộ trình
- ✨ Nâng cao mô hình AI DeepSeek để phản hồi cá nhân hóa hơn

---

## 🤝 Đóng góp

Pull requests luôn được chào đón! Nếu bạn có ý tưởng hoặc phát hiện bug, đừng ngần ngại tạo issue hoặc PR.

---

## 📝 Giấy phép

Dự án được phát triển cho mục đích học tập. Bạn có thể sử dụng, tùy biến và chia sẻ với ghi chú nguồn gốc phù hợp.

---
