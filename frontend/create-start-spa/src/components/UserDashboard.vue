<template>
  <div class="user-dashboard">
    <div class="dashboard-header">
      <img src="@/assets/UserLogo.png" class="dashboard-avatar" alt="User" />
      <div class="dashboard-username">{{ username }}</div>
    </div>
    <ul class="dashboard-menu">
      <li
        v-for="item in menu"
        :key="item.key"
        :class="{ active: active === item.key }"
        @click="$emit('select', item.key)"
      >
        <i :class="item.icon"></i>
        <span>{{ item.label }}</span>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "UserDashboard",
  props: {
    username: { type: String, default: "User" },
    active: { type: String, default: "profile" },
  },
  data() {
    return {
      menu: [
        { key: "profile", label: "Profile", icon: "fas fa-user" },
        {
          key: "change-password",
          label: "Change Password",
          icon: "fas fa-key",
        },
        { key: "my-courses", label: "My Courses", icon: "fas fa-book" },
        { key: "progress", label: "Progress", icon: "fas fa-chart-line" },
        { key: "logout", label: "Logout", icon: "fas fa-sign-out-alt" },
      ],
    };
  },
};
</script>

<style scoped>
.user-dashboard {
  width: 240px;
  min-height: 400px;
  background: linear-gradient(120deg, #232526 0%, #5e97bb 100%);
  color: #fff;
  border-radius: 18px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.1);
  padding: 2rem 1rem 1rem 1rem;
  display: flex;
  flex-direction: column;
  align-items: center;
  animation: fadeInLeft 0.7s;
  margin-right: 2rem;
  position: sticky;
  top: 100px;
}

.dashboard-header {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 2rem;
}

.dashboard-avatar {
  width: 70px;
  height: 70px;
  border-radius: 50%;
  object-fit: cover;
  border: 3px solid #00bcd4;
  margin-bottom: 0.7rem;
  background: #fff;
}

.dashboard-username {
  font-size: 1.2rem;
  font-weight: bold;
  color: #fff;
  text-shadow: 0 2px 8px rgba(0, 0, 0, 0.12);
}

.dashboard-menu {
  list-style: none;
  padding: 0;
  width: 100%;
}

.dashboard-menu li {
  display: flex;
  align-items: center;
  gap: 1rem;
  padding: 0.9rem 1rem;
  border-radius: 8px;
  margin-bottom: 0.5rem;
  cursor: pointer;
  font-size: 1.08rem;
  transition: background 0.2s, color 0.2s, transform 0.2s;
  background: transparent;
}

.dashboard-menu li.active,
.dashboard-menu li:hover {
  background: #fff;
  color: #00bcd4;
  transform: translateX(6px) scale(1.04);
}

.dashboard-menu li i {
  width: 22px;
  text-align: center;
}
@keyframes fadeInLeft {
  from {
    opacity: 0;
    transform: translateX(-40px);
  }
  to {
    opacity: 1;
    transform: translateX(0);
  }
}
</style>
