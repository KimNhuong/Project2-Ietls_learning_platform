<template>
  <div class="profile-hero-bg">
    <div class="profile-hero-overlay"></div>
    <div class="profile-content">
      <div class="profile-card">
        <div class="profile-avatar-section">
          <img
            src="@/assets/UserLogo.png"
            alt="User Avatar"
            class="profile-avatar"
          />
          <h2>{{ user.username }}</h2>
          <p class="profile-email">{{ user.email }}</p>
        </div>
        <form class="profile-form" @submit.prevent="updateProfile">
          <div class="form-group">
            <label for="username">Username</label>
            <input
              id="username"
              type="text"
              v-model="editUser.username"
              disabled
            />
          </div>
          <div class="form-group">
            <label for="email">E-mail</label>
            <input id="email" type="email" v-model="editUser.email" required />
          </div>
          <button class="btn btn-primary" type="submit" :disabled="loading">
            {{ loading ? "Saving..." : "Save Changes" }}
          </button>
        </form>
        <div class="change-password-section">
          <button
            class="btn btn-secondary"
            @click="showChangePassword = !showChangePassword"
          >
            Change Password
          </button>
          <transition name="fade-slide">
            <form
              v-if="showChangePassword"
              class="change-password-form"
              @submit.prevent="changePassword"
            >
              <div class="form-group">
                <label for="oldPassword">Old Password</label>
                <input
                  id="oldPassword"
                  type="password"
                  v-model="passwordForm.oldPassword"
                  required
                />
              </div>
              <div class="form-group">
                <label for="newPassword">New Password</label>
                <input
                  id="newPassword"
                  type="password"
                  v-model="passwordForm.newPassword"
                  required
                />
              </div>
              <button
                class="btn btn-primary"
                type="submit"
                :disabled="loadingPwd"
              >
                {{ loadingPwd ? "Changing..." : "Change Password" }}
              </button>
            </form>
          </transition>
        </div>
        <div v-if="successMsg" class="success-message">{{ successMsg }}</div>
        <div v-if="errorMsg" class="error-message">{{ errorMsg }}</div>
      </div>
    </div>
    <img class="profile-bg-img" src="@/assets/classroom.jpg" alt="Background" />
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "ProfilePage",
  data() {
    return {
      user: {
        username: "",
        email: "",
      },
      editUser: {
        username: "",
        email: "",
      },
      loading: false,
      loadingPwd: false,
      showChangePassword: false,
      passwordForm: {
        oldPassword: "",
        newPassword: "",
      },
      successMsg: "",
      errorMsg: "",
    };
  },
  async mounted() {
    await this.fetchUser();
  },
  methods: {
    async fetchUser() {
      try {
        this.errorMsg = "";
        // Giả sử userId được lưu trong token (decode hoặc lấy từ localStorage)
        const token = localStorage.getItem("token");
        if (!token) {
          this.$router.push("/login");
          return;
        }
        // Decode JWT để lấy userId
        const payload = JSON.parse(atob(token.split(".")[1]));
        const userId =
          payload[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
          ];
        const res = await axios.get(
          `http://localhost:5067/api/Users/${userId}`,
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        );
        this.user = {
          username: res.data.username,
          email: res.data.email,
        };
        this.editUser = { ...this.user };
      } catch (err) {
        this.errorMsg = "Failed to load user info.";
      }
    },
    async updateProfile() {
      this.loading = true;
      this.successMsg = "";
      this.errorMsg = "";
      try {
        const token = localStorage.getItem("token");
        const payload = JSON.parse(atob(token.split(".")[1]));
        const userId =
          payload[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
          ];
        await axios.put(
          `http://localhost:5067/api/Users/${userId}`,
          {
            username: this.editUser.username,
            email: this.editUser.email,
          },
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        );
        this.successMsg = "Profile updated successfully!";
        this.user = { ...this.editUser };
      } catch (err) {
        this.errorMsg =
          err.response?.data?.message || "Failed to update profile.";
      }
      this.loading = false;
    },
    async changePassword() {
      this.loadingPwd = true;
      this.successMsg = "";
      this.errorMsg = "";
      try {
        const token = localStorage.getItem("token");
        const payload = JSON.parse(atob(token.split(".")[1]));
        const userId =
          payload[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
          ];
        await axios.post(
          `http://localhost:5067/api/Auth/change-password/${userId}`,
          {
            oldPassword: this.passwordForm.oldPassword,
            newPassword: this.passwordForm.newPassword,
          },
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        );
        this.successMsg = "Password changed successfully!";
        this.passwordForm.oldPassword = "";
        this.passwordForm.newPassword = "";
        this.showChangePassword = false;
      } catch (err) {
        this.errorMsg =
          err.response?.data?.message || "Failed to change password.";
      }
      this.loadingPwd = false;
    },
  },
};
</script>

<style scoped>
.profile-hero-bg {
  min-height: 100vh;
  background: linear-gradient(120deg, #232526 0%, #5e97bb 100%);
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
}

.profile-bg-img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  object-fit: cover;
  z-index: 1;
  opacity: 0.22;
}

.profile-hero-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(30, 60, 100, 0.55);
  z-index: 2;
}

.profile-content {
  position: relative;
  z-index: 3;
  width: 100%;
  max-width: 480px;
  margin: 0 auto;
  padding: 3rem 2rem 2rem 2rem;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.profile-card {
  width: 100%;
  background: rgba(255, 255, 255, 0.97);
  border-radius: 18px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.1);
  padding: 2.5rem 2rem 2rem 2rem;
  display: flex;
  flex-direction: column;
  align-items: center;
  animation: fadeInUp 0.7s;
}

.profile-avatar-section {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 2rem;
}

.profile-avatar {
  width: 90px;
  height: 90px;
  border-radius: 50%;
  object-fit: cover;
  border: 3px solid #00bcd4;
  margin-bottom: 1rem;
  background: #fff;
}

.profile-card h2 {
  margin: 0;
  font-size: 2rem;
  color: #222;
  font-weight: bold;
}

.profile-email {
  color: #007bff;
  margin-bottom: 0.5rem;
}

.profile-form {
  width: 100%;
  margin-bottom: 1.5rem;
}

.form-group {
  margin-bottom: 1.2rem;
  text-align: left;
}

.form-group label {
  display: block;
  margin-bottom: 0.4rem;
  font-weight: bold;
  color: #00bcd4;
}

.form-group input {
  width: 100%;
  padding: 0.7rem;
  border: 1px solid #ccc;
  border-radius: 6px;
  font-size: 1rem;
  background: #f8fafd;
}

.btn {
  padding: 0.7rem 1.5rem;
  font-size: 1rem;
  border-radius: 6px;
  border: none;
  cursor: pointer;
  font-weight: 600;
  transition: background 0.2s, color 0.2s;
}

.btn-primary {
  background: #00bcd4;
  color: #fff;
}

.btn-primary:hover {
  background: #0097a7;
}

.btn-secondary {
  background: #fff;
  color: #00bcd4;
  border: 1px solid #00bcd4;
  margin-bottom: 1rem;
}

.btn-secondary:hover {
  background: #00bcd4;
  color: #fff;
}

.change-password-section {
  width: 100%;
  margin-bottom: 1rem;
}

.change-password-form {
  margin-top: 1rem;
  background: #f8fafd;
  border-radius: 8px;
  padding: 1rem;
  box-shadow: 0 2px 8px rgba(30, 60, 100, 0.08);
}

.success-message {
  color: #28a745;
  margin-top: 1rem;
  text-align: center;
  font-weight: bold;
}

.error-message {
  color: #e53935;
  margin-top: 1rem;
  text-align: center;
  font-weight: bold;
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
</style>
