<template>
  <div class="user-module-outer">
    <div class="user-module-wrapper">
      <UserDashboard
        :username="user.username"
        :active="activeTab"
        @select="handleSelect"
      />
      <div class="user-content-panel">
        <ProfilePanel
          v-if="activeTab === 'profile'"
          :user="user"
          @update="fetchUser"
        />
        <ChangePasswordPanel
          v-if="activeTab === 'change-password'"
          :user-id="userId"
        />
        <!-- Thêm các panel khác nếu cần -->
      </div>
    </div>
  </div>
</template>

<script>
import UserDashboard from "@/components/UserDashboard.vue";
import ProfilePanel from "./ProfilePanel.vue";
import ChangePasswordPanel from "./ChangePasswordPanel.vue";
import axios from "axios";
// ...import các panel khác nếu có...

export default {
  components: { UserDashboard, ProfilePanel, ChangePasswordPanel },
  data() {
    return {
      user: {},
      userId: null,
      activeTab: "profile",
    };
  },
  created() {
    this.fetchUser();
  },
  methods: {
    async fetchUser() {
      // Lấy userId từ token/localStorage hoặc router, ví dụ:
      const token = localStorage.getItem("token");
      if (!token) {
        this.$router.push("/login");
        return;
      }
      // Giả sử userId lưu trong localStorage (tuỳ backend)
      const userId = localStorage.getItem("userId");
      if (!userId) {
        // Nếu không có userId, có thể giải mã từ token hoặc gọi API profile/me
        // Ở đây giả định đã có userId
        return;
      }
      this.userId = userId;
      try {
        const res = await axios.get(
          `http://localhost:5067/api/Users/${userId}`,
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        );
        const userData = { ...res.data };
        // Xóa các trường không cần thiết
        delete userData.role;
        delete userData.passwordHash;
        delete userData.roleId;
        // Giữ lại userId, username, email, createdAt
        this.user = userData;
      } catch (err) {
        alert("Không thể lấy thông tin người dùng.");
      }
    },
    handleSelect(tab) {
      if (tab === "logout") {
        localStorage.removeItem("token");
        localStorage.removeItem("userId");
        this.$router.push("/login");
        return;
      }
      this.activeTab = tab;
    },
  },
};
</script>

<style scoped>
.user-module-outer {
  flex: 1 0 auto;
  min-height: 0;
  display: flex;
  flex-direction: column;
  height: 100%;
}
.user-module-wrapper {
  display: flex;
  align-items: flex-start;
  flex: 1 0 auto;
  min-height: 70vh;
  margin-top: 2rem;
  margin-bottom: 2rem;
}
.user-content-panel {
  flex: 1;
  background: rgba(255, 255, 255, 0.97);
  border-radius: 18px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.1);
  padding: 2.5rem 2rem 2rem 2rem;
  min-width: 320px;
  animation: fadeInUp 0.7s;
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
</style>
