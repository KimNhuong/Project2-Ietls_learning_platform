<template>
  <transition name="fade-slide">
    <div class="profile-panel">
      <h2 class="profile-title">Thông tin cá nhân</h2>
      <form @submit.prevent="updateProfile" class="profile-form">
        <div class="form-group">
          <label>Username</label>
          <input v-model="form.username" type="text" disabled />
        </div>
        <div class="form-group">
          <label>Email</label>
          <input v-model="form.email" type="email" disabled />
        </div>
        <div class="form-group">
          <label>Ngày tạo tài khoản</label>
          <input :value="formatDate(form.createdAt)" type="text" disabled />
        </div>
        <button type="submit" class="btn-save" disabled>Lưu thay đổi</button>
      </form>
    </div>
  </transition>
</template>

<script>
export default {
  props: ["user"],
  data() {
    return {
      form: {
        username: "",
        email: "",
        createdAt: "",
      },
    };
  },
  watch: {
    user: {
      immediate: true,
      handler(val) {
        if (val) {
          this.form = {
            username: val.username || "",
            email: val.email || "",
            createdAt: val.createdAt || "",
          };
        }
      },
    },
  },
  methods: {
    // Không cho phép cập nhật thông tin vì backend không hỗ trợ
    updateProfile() {},
    formatDate(dateStr) {
      if (!dateStr) return "";
      const d = new Date(dateStr);
      return d.toLocaleDateString();
    },
  },
};
</script>

<style scoped>
.profile-panel {
  background: linear-gradient(135deg, #f8fafc 70%, #e0e7ff 100%);
  border-radius: 18px;
  box-shadow: 0 8px 32px rgba(30, 60, 100, 0.12);
  padding: 2.5rem 2rem 2rem 2rem;
  max-width: 420px;
  margin: 0 auto;
  animation: fadeInUp 0.7s;
  transition: box-shadow 0.3s;
}
.profile-panel:hover {
  box-shadow: 0 12px 36px rgba(30, 60, 100, 0.18);
}
.profile-title {
  text-align: center;
  font-size: 1.7rem;
  font-weight: bold;
  color: #6a0dad;
  margin-bottom: 2rem;
  letter-spacing: 1px;
}
.profile-form .form-group {
  margin-bottom: 1.2rem;
}
.profile-form label {
  font-weight: 600;
  color: #333;
  margin-bottom: 0.4rem;
  display: block;
}
.profile-form input {
  width: 100%;
  padding: 0.7rem 1rem;
  border: 1.5px solid #d1d5db;
  border-radius: 8px;
  font-size: 1rem;
  background: #f4f4f9;
  transition: border 0.2s, box-shadow 0.2s;
}
.profile-form input:focus {
  border-color: #6a0dad;
  outline: none;
  box-shadow: 0 0 0 2px #e0e7ff;
}
.profile-form input[disabled] {
  background: #e5e7eb;
  color: #888;
}
.btn-save {
  width: 100%;
  padding: 0.9rem;
  background: linear-gradient(90deg, #6a0dad 60%, #00bcd4 100%);
  color: #fff;
  border: none;
  border-radius: 8px;
  font-size: 1.1rem;
  font-weight: 600;
  cursor: pointer;
  margin-top: 0.5rem;
  box-shadow: 0 2px 8px rgba(106, 13, 173, 0.08);
  transition: background 0.2s, box-shadow 0.2s;
}
.btn-save:hover {
  background: linear-gradient(90deg, #00bcd4 60%, #6a0dad 100%);
  box-shadow: 0 4px 16px rgba(106, 13, 173, 0.13);
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
  .profile-panel {
    padding: 1.2rem 0.5rem;
    max-width: 100%;
  }
}
</style>
