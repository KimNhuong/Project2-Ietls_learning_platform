<template>
  <nav
    class="navbar navbar-expand-lg"
    :class="{
      'navbar-dark bg-dark': useDarkNavbar,
      'navbar-light bg-light': !useDarkNavbar,
    }"
  >
    <div class="container-fluid">
      <a class="navbar-brand" href="#"><b class="brand-text">HUSTER</b></a>
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <router-link
            class="nav-link"
            to="/"
            exact
            :class="{ active: $route.path === '/' }"
          >
            Home
          </router-link>
        </li>
        <li class="nav-item">
          <router-link
            class="nav-link"
            to="/courses"
            :class="{ active: $route.path === '/courses' }"
          >
            Courses
          </router-link>
        </li>
        <li class="nav-item">
          <router-link
            class="nav-link"
            to="/dictionary"
            :class="{ active: $route.path === '/dictionary' }"
          >
            Dictionary
          </router-link>
        </li>
        <li class="nav-item">
          <router-link
            class="nav-link"
            to="/flashcard"
            :class="{ active: $route.path === '/flashcard' }"
          >
            FlashCard
          </router-link>
        </li>
      </ul>
      <form class="d-flex" style="position: relative">
        <button
          v-if="!isLoggedIn"
          class="btn btn-primary user-button"
          @click.prevent="$emit('login')"
        >
          <i class="fas fa-user-circle"></i> Log in as a Student
        </button>
        <div
          v-else
          class="user-dropdown"
          @mouseenter="showDropdown = true"
          @mouseleave="showDropdown = false"
        >
          <button class="btn btn-user-icon" type="button">
            <img src="@/assets/UserLogo.png" alt="User" class="user-logo-img" />
          </button>
          <div v-if="showDropdown" class="dropdown-menu show">
            <a class="dropdown-item" href="#" @click.prevent="goToProfile"
              >Thông tin cá nhân</a
            >
            <a class="dropdown-item" href="#" @click.prevent="goToMyCourses"
              >Các khóa học đã mua</a
            >
            <a class="dropdown-item" href="#" @click.prevent="goToProgress"
              >Tiến độ của tôi</a
            >
            <a
              class="dropdown-item"
              href="#"
              @click.prevent="goToChangePassword"
              >Đổi mật khẩu</a
            >
            <div class="dropdown-divider"></div>
            <a class="dropdown-item" href="#" @click.prevent="logout"
              >Đăng xuất</a
            >
          </div>
        </div>
      </form>
    </div>
  </nav>
</template>

<script>
export default {
  props: ["useDarkNavbar"],
  data() {
    return {
      showDropdown: false,
    };
  },
  computed: {
    isLoggedIn() {
      return !!localStorage.getItem("token");
    },
  },
  methods: {
    goToProfile() {
      this.$router.push("/profile");
    },
    goToMyCourses() {
      this.$router.push("/my-courses");
    },
    goToProgress() {
      this.$router.push("/my-progress");
    },
    goToChangePassword() {
      this.$router.push("/change-password");
    },
    logout() {
      localStorage.removeItem("token");
      this.$router.push("/");
      window.location.reload();
    },
  },
};
</script>

<style scoped>
.navbar {
  width: 100%;
  background-color: #1f1f1f;
  padding: 1rem 2rem;
  display: flex;
  align-items: center;
  justify-content: space-between;
  position: relative;
  top: 0;
  left: 0;
  z-index: 1000;
}

.navbar-brand {
  font-size: 1.5rem;
  font-weight: bold;
  color: #6a0dad;
}

.navbar-nav .nav-link {
  color: white;
  margin-right: 1rem;
  font-size: 1rem;
}

.navbar-nav .nav-link.active {
  color: #6a0dad;
  font-weight: bold;
}

.user-button {
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 0.5rem 1rem;
  font-size: 1rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.user-button:hover {
  background-color: #0056b3;
}

.user-button.btn-success {
  background-color: #28a745;
  border: none;
}

.user-button.btn-success:hover {
  background-color: #218838;
}

.user-dropdown {
  position: relative;
  display: inline-block;
}

.btn-user-icon {
  background: transparent;
  color: #fff;
  border: none;
  font-size: 2rem;
  padding: 0 0.5rem;
  cursor: pointer;
}

.btn-user-icon:focus {
  outline: none;
}

.dropdown-menu {
  position: absolute;
  right: 0;
  top: 110%;
  min-width: 200px;
  background: #222;
  color: #fff;
  border-radius: 8px;
  box-shadow: 0 4px 16px rgba(0, 0, 0, 0.15);
  z-index: 999;
  padding: 0.5rem 0;
  display: block;
}

.dropdown-item {
  padding: 0.75rem 1.5rem;
  color: #fff;
  text-decoration: none;
  display: block;
  cursor: pointer;
  transition: background 0.2s;
}

.dropdown-item:hover {
  background: #333;
}

.dropdown-divider {
  height: 1px;
  background: #444;
  margin: 0.5rem 0;
}

.user-logo-img {
  width: 48px;
  height: 48px;
  border-radius: 50%;
  object-fit: cover;
  background: #fff;
  border: 2px solid #6a0dad;
}
</style>
