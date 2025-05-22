<template>
  <div class="login-page">
    <!-- Left Section: Login Form -->
    <div class="login-left">
      <div class="login-card">
        <h2>Log in</h2>
        <button class="btn btn-google">
          <img src="@/assets/google-icon.png" alt="Google Icon" />
          Log in with Google
        </button>
        <p class="or-divider">or</p>
        <form @submit.prevent="handleLogin">
          <div class="form-group">
            <label for="username">Username</label>
            <input
              type="text"
              id="username"
              v-model="username"
              placeholder="Enter your username"
              required
            />
          </div>
          <div class="form-group">
            <label for="password">Password</label>
            <input
              type="password"
              id="password"
              v-model="password"
              placeholder="Enter your password"
              required
            />
          </div>
          <a href="#" class="forgot-password" @click.prevent="forgotPassword">
            Forgot password?
          </a>
          <button type="submit" class="btn btn-login">Log in</button>
        </form>
        <p class="signup-link">
          Not a member yet?
          <a href="#" @click.prevent="createAccount">Sign up for free</a>
        </p>
      </div>
    </div>

    <!-- Right Section: Superhero Image -->
    <div class="login-right">
      <img src="@/assets/superhero.png" alt="Superhero Illustration" />
    </div>
  </div>
</template>

<script>
import authService from "@/services/authService";

export default {
  name: "LoginPage",
  data() {
    return {
      username: "",
      password: "",
    };
  },
  methods: {
    async handleLogin() {
      if (this.username && this.password) {
        try {
          const token = await authService.login(this.username, this.password);
          localStorage.setItem("token", token);
          this.$router.push("/");
          // Sau khi chuyển về HomePage, có thể phát sự kiện hoặc reload để cập nhật giao diện
          // window.location.reload();
        } catch (err) {
          alert(
            "Login failed: " + (err.response?.data?.message || err.message)
          );
        }
      } else {
        alert("Please fill in both fields.");
      }
    },
    forgotPassword() {
      alert("Redirecting to forgot password page...");
    },
    createAccount() {
      this.$router.push("/register");
    },
  },
};
</script>

<style scoped>
/* Main Page Layout */
.login-page {
  display: flex;
  height: 100vh;
  background-color: #1f1f1f;
  color: white;
  font-family: Arial, sans-serif;
}

/* Left Section: Login Form */
.login-left {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.login-card {
  background-color: white;
  color: black;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 400px;
  text-align: center;
}

.login-card h2 {
  margin-bottom: 1.5rem;
  font-size: 1.8rem;
}

.btn-google {
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: white;
  color: black;
  border: 1px solid #ccc;
  padding: 0.75rem;
  width: 100%;
  border-radius: 4px;
  font-size: 1rem;
  margin-bottom: 1rem;
  cursor: pointer;
}

.btn-google img {
  width: 20px;
  margin-right: 0.5rem;
}

.or-divider {
  margin: 1rem 0;
  font-size: 0.9rem;
  color: #666;
}

.form-group {
  margin-bottom: 1rem;
  text-align: left;
}

.form-group label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: bold;
}

.form-group input {
  width: 100%;
  padding: 0.75rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 1rem;
}

.forgot-password {
  display: block;
  text-align: right;
  margin-bottom: 1rem;
  font-size: 0.9rem;
  color: #007bff;
  text-decoration: none;
}

.forgot-password:hover {
  text-decoration: underline;
}

.btn-login {
  width: 100%;
  padding: 0.75rem;
  background-color: #dc3545;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  cursor: pointer;
}

.btn-login:hover {
  background-color: #c82333;
}

.signup-link {
  margin-top: 1rem;
  font-size: 0.9rem;
}

.signup-link a {
  color: #007bff;
  text-decoration: none;
}

.signup-link a:hover {
  text-decoration: underline;
}

/* Right Section: Superhero Image */
.login-right {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.login-right img {
  max-width: 80%;
  height: auto;
}
</style>
