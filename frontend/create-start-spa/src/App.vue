<template>
  <div id="app" class="app-root">
    <Navbar
      :useDarkNavbar="useDarkNavbar"
      @toggle-navbar="useDarkNavbar = !useDarkNavbar"
      @login="goToLogin"
    />
    <transition name="fade" mode="out-in">
      <router-view />
    </transition>
    <AppFooter />
  </div>
</template>

<script>
import Navbar from "./components/NavigationBar.vue";
import AppFooter from "./components/AppFooter.vue";

export default {
  name: "App",
  components: {
    Navbar,
    AppFooter,
  },
  data() {
    return {
      useDarkNavbar: true,
    };
  },
  methods: {
    goToLogin() {
      this.$router.push("/login");
    },
  },
};
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap");

body,
html,
#app {
  font-family: "Roboto", Arial, Helvetica, sans-serif;
  /* ...existing code or add more global styles if needed... */
  min-height: 100vh;
  margin: 0;
  padding: 0;
}

.app-root {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.app-root > .fade-enter-active,
.app-root > .fade-leave-active {
  flex: 1 0 auto;
}

.app-root > .router-view,
.app-root > .fade,
.app-root > .fade-enter-active,
.app-root > .fade-leave-active {
  flex: 1 0 auto;
}

.AppFooter,
.app-root > .AppFooter,
#app > .main-footer,
.app-root > .main-footer {
  margin-top: auto;
  width: 100%;
  background: #1f1f1f;
  color: #fff;
  text-align: center;
  padding: 1.2rem 0 1rem 0;
  font-size: 1rem;
  letter-spacing: 1px;
  box-shadow: 0 -2px 12px rgba(30, 60, 100, 0.07);
}
</style>
