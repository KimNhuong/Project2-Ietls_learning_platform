<template>
  <transition name="fade-slide">
    <div class="hero-section">
      <div class="hero-overlay"></div>
      <div class="hero-content">
        <p class="hero-subtitle">BEST ONLINE COURSES</p>
        <h1 class="hero-title">
          Get Educated Online<br />
          From Your Home
        </h1>
        <p class="hero-desc">
          Welcome to your ultimate IELTS learning hub! Whether you're aiming for
          academic or general training, our expert resources, practice tests,
          and personalized study plans are designed to boost your skills and
          help you succeed. Start your journey to a higher band score today!
        </p>
        <div class="hero-buttons">
          <button class="btn btn-primary" @click="readMore">Read More</button>
          <button class="btn btn-secondary" @click="joinNow">Join Now</button>
        </div>
      </div>
      <img
        class="hero-bg-img"
        src="@/assets/classroom.jpg"
        alt="Hero Background"
      />
      <div class="hero-arrows">
        <button class="arrow-btn" @click="prevSlide">&#60;</button>
        <button class="arrow-btn" @click="nextSlide">&#62;</button>
      </div>
    </div>
  </transition>
</template>

<script>
export default {
  name: "HomePage",
  methods: {
    readMore() {
      // Điều hướng hoặc scroll tới phần giới thiệu
      // this.$router.push("/about");
    },
    joinNow() {
      this.$router.push("/register");
    },
    prevSlide() {
      // Placeholder cho hiệu ứng slider nếu muốn
    },
    nextSlide() {
      // Placeholder cho hiệu ứng slider nếu muốn
    },
  },
};
</script>

<style scoped>
.hero-section {
  position: relative;
  width: 100vw;
  min-height: calc(
    100vh - 80px
  ); /* Đảm bảo chiếm gần hết chiều cao, trừ chiều cao footer */
  display: flex;
  align-items: center;
  justify-content: flex-start;
  overflow: hidden;
  background: #222;
  flex: 1 0 auto;
  animation: fadeInUp 0.7s;
}

.hero-bg-img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  object-fit: cover;
  z-index: 1;
}

.hero-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(30, 60, 100, 0.55);
  z-index: 2;
}

.hero-content {
  position: relative;
  z-index: 3;
  margin-left: 7vw;
  max-width: 600px;
  color: #fff;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

.hero-subtitle {
  color: #00bcd4;
  font-weight: bold;
  letter-spacing: 2px;
  margin-bottom: 1rem;
  font-size: 1.1rem;
}

.hero-title {
  font-size: 3rem;
  font-weight: bold;
  margin-bottom: 1.2rem;
  line-height: 1.1;
}

.hero-desc {
  font-size: 1.1rem;
  margin-bottom: 2rem;
  color: #e0e0e0;
}

.hero-buttons {
  display: flex;
  gap: 1.2rem;
}

.btn {
  padding: 0.8rem 2.2rem;
  font-size: 1.1rem;
  border-radius: 4px;
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
  color: #222;
  border: 1px solid #ccc;
}

.btn-secondary:hover {
  background: #f0f0f0;
}

.hero-arrows {
  position: absolute;
  right: 2vw;
  top: 50%;
  transform: translateY(-50%);
  z-index: 4;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.arrow-btn {
  background: rgba(255, 255, 255, 0.2);
  color: #fff;
  border: 1px solid #fff;
  border-radius: 50%;
  width: 44px;
  height: 44px;
  font-size: 1.5rem;
  cursor: pointer;
  transition: background 0.2s;
}

.arrow-btn:hover {
  background: #00bcd4;
  color: #fff;
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
