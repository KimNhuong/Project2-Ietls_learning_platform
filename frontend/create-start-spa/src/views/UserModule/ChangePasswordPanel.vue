<template>
  <transition name="fade-slide">
    <div class="changepass-panel">
      <h2 class="changepass-title">Đổi mật khẩu</h2>
      <form @submit.prevent="changePassword" class="changepass-form">
        <div class="form-group">
          <label>Mật khẩu cũ</label>
          <input v-model="oldPassword" type="password" required />
        </div>
        <div class="form-group">
          <label>Mật khẩu mới</label>
          <input v-model="newPassword" type="password" required />
        </div>
        <button type="submit" class="btn-changepass">Đổi mật khẩu</button>
      </form>
    </div>
  </transition>
</template>

<script>
import axios from "axios";
export default {
  props: ["userId"],
  data() {
    return {
      oldPassword: "",
      newPassword: "",
    };
  },
  methods: {
    async changePassword() {
      const token = localStorage.getItem("token");
      try {
        await axios.post(
          `http://localhost:5067/api/Auth/change-password/${this.userId}`,
          {
            oldPassword: this.oldPassword,
            newPassword: this.newPassword,
          },
          { headers: { Authorization: `Bearer ${token}` } }
        );
        this.$toast?.success?.("Đổi mật khẩu thành công!") ||
          alert("Đổi mật khẩu thành công!");
        this.oldPassword = "";
        this.newPassword = "";
      } catch (err) {
        this.$toast?.error?.(
          "Đổi mật khẩu thất bại: " +
            (err.response?.data?.message || err.message)
        ) ||
          alert(
            "Đổi mật khẩu thất bại: " +
              (err.response?.data?.message || err.message)
          );
      }
    },
  },
};
</script>

<style scoped>
.changepass-panel {
  background: linear-gradient(135deg, #f8fafc 70%, #ffe0f7 100%);
  border-radius: 18px;
  box-shadow: 0 8px 32px rgba(30, 60, 100, 0.12);
  padding: 2.5rem 2rem 2rem 2rem;
  max-width: 420px;
  margin: 0 auto;
  animation: fadeInUp 0.7s;
  transition: box-shadow 0.3s;
}
.changepass-panel:hover {
  box-shadow: 0 12px 36px rgba(30, 60, 100, 0.18);
}
.changepass-title {
  text-align: center;
  font-size: 1.7rem;
  font-weight: bold;
  color: #dc3545;
  margin-bottom: 2rem;
  letter-spacing: 1px;
}
.changepass-form .form-group {
  margin-bottom: 1.2rem;
}
.changepass-form label {
  font-weight: 600;
  color: #333;
  margin-bottom: 0.4rem;
  display: block;
}
.changepass-form input {
  width: 100%;
  padding: 0.7rem 1rem;
  border: 1.5px solid #d1d5db;
  border-radius: 8px;
  font-size: 1rem;
  background: #f4f4f9;
  transition: border 0.2s, box-shadow 0.2s;
}
.changepass-form input:focus {
  border-color: #dc3545;
  outline: none;
  box-shadow: 0 0 0 2px #ffe0f7;
}
.btn-changepass {
  width: 100%;
  padding: 0.9rem;
  background: linear-gradient(90deg, #dc3545 60%, #6a0dad 100%);
  color: #fff;
  border: none;
  border-radius: 8px;
  font-size: 1.1rem;
  font-weight: 600;
  cursor: pointer;
  margin-top: 0.5rem;
  box-shadow: 0 2px 8px rgba(220, 53, 69, 0.08);
  transition: background 0.2s, box-shadow 0.2s;
}
.btn-changepass:hover {
  background: linear-gradient(90deg, #6a0dad 60%, #dc3545 100%);
  box-shadow: 0 4px 16px rgba(220, 53, 69, 0.13);
}
@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(40px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
.fade-slide-enter-active,
.fade-slide-leave-active {
  transition: opacity 0.5s, transform 0.5s;
}
.fade-slide-enter-from,
.fade-slide-leave-to {
  opacity: 0;
  transform: translateY(40px);
}
@media (max-width: 600px) {
  .changepass-panel {
    padding: 1.2rem 0.5rem;
    max-width: 100%;
  }
}
</style>
