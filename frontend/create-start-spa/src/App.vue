<template>
  <div class="home-background">
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
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s ease;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
