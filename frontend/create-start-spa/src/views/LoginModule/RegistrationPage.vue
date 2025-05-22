<template>
  <div class="register-page">
    <div class="register-left">
      <div class="register-card">
        <h2>Sign up</h2>
        <form @submit.prevent="handleRegister">
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
            <label for="email">E-mail</label>
            <input
              type="email"
              id="email"
              v-model="email"
              placeholder="Enter your email"
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
          <button type="submit" class="btn btn-register">Sign up</button>
        </form>
        <p class="login-link">
          Already have an account?
          <a href="#" @click.prevent="goToLogin">Log in</a>
        </p>
      </div>
    </div>
    <div class="register-right">
      <img src="@/assets/superhero.png" alt="Superhero Illustration" />
    </div>
  </div>
</template>

<script>
import authService from "@/services/authService";

export default {
  name: "RegistrationPage",
  data() {
    return {
      username: "",
      email: "",
      password: "",
    };
  },
  methods: {
    async handleRegister() {
      if (this.username && this.email && this.password) {
        try {
          await authService.register(this.username, this.email, this.password);
          alert("Registration successful! Please login.");
          this.$router.push("/login");
        } catch (err) {
          alert(
            "Registration failed: " +
              (err.response?.data?.message || err.message)
          );
        }
      } else {
        alert("Please fill in all fields.");
      }
    },
    goToLogin() {
      this.$router.push("/login");
    },
  },
};
</script>

<style scoped>
.register-page {
  display: flex;
  height: 100vh;
  background-color: #1f1f1f;
  color: white;
  font-family: Arial, sans-serif;
}

.register-left {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.register-card {
  background-color: white;
  color: black;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 400px;
  text-align: center;
}

.register-card h2 {
  margin-bottom: 1.5rem;
  font-size: 1.8rem;
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

.btn-register {
  width: 100%;
  padding: 0.75rem;
  background-color: #28a745;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  cursor: pointer;
}

.btn-register:hover {
  background-color: #218838;
}

.login-link {
  margin-top: 1rem;
  font-size: 0.9rem;
}

.login-link a {
  color: #007bff;
  text-decoration: none;
}

.login-link a:hover {
  text-decoration: underline;
}

.register-right {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.register-right img {
  max-width: 80%;
  height: auto;
}
</style>
