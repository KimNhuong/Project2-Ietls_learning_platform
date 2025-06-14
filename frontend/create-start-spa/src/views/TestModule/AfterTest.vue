<template>
  <div class="aftertest-wrapper">
    <h2>Bạn đã hoàn thành {{ testTitle }}</h2>
    <div class="aftertest-actions">
      <button class="btn-view-score" @click="viewScore">Xem điểm</button>
      <button v-if="hasNextPart" class="btn-next-part" @click="goToNextPart">Làm tiếp {{ nextPartLabel }}</button>
      <router-link class="btn-back-home" to="/">Về trang chủ</router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "AfterTest",
  data() {
    return {
      testTitle: "bài kiểm tra",
      hasNextPart: false,
      nextPartLabel: "Part 2",
      nextTestId: null,
    };
  },
  async mounted() {
    // Lấy thông tin test hiện tại từ route hoặc localStorage
    const testId = this.$route.query.testId || localStorage.getItem("lastTestId");
    if (testId) {
      // Gọi API lấy thông tin test
      try {
        const res = await fetch(`http://localhost:5067/api/Tests/${testId}`);
        const test = await res.json();
        this.testTitle = test.title || "bài kiểm tra";
        // Kiểm tra có part tiếp theo không (ví dụ: test tiếp theo cùng lesson, skill khác)
        const testsRes = await fetch(`http://localhost:5067/api/Tests/by-lesson/${test.lessonId}`);
        const tests = await testsRes.json();
        const idx = tests.findIndex(t => t.id == testId);
        if (idx !== -1 && idx < tests.length - 1) {
          this.hasNextPart = true;
          this.nextTestId = tests[idx + 1].id;
          this.nextPartLabel = `Part ${idx + 2}`;
        }
      } catch (e) {
        // Không làm gì nếu lỗi, chỉ bỏ qua
      }
    }
  },
  methods: {
    viewScore() {
      // Chuyển sang trang xem điểm (có thể là /score hoặc mở modal tuỳ bạn)
      this.$router.push({ name: "ScorePage", query: { testId: this.$route.query.testId } });
    },
    goToNextPart() {
      if (this.nextTestId) {
        this.$router.push({ name: "TestAttempt", params: { testId: this.nextTestId } });
      }
    },
  },
};
</script>

<style scoped>
.aftertest-wrapper {
  max-width: 700px;
  margin: 3rem auto;
  background: #fff;
  border-radius: 18px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.10);
  padding: 2.5rem 2rem 2rem 2rem;
  text-align: center;
}
.aftertest-actions {
  margin-top: 2.5rem;
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
  align-items: center;
}
.btn-view-score, .btn-next-part, .btn-back-home {
  background: #00bcd4;
  color: #fff;
  border: none;
  border-radius: 8px;
  padding: 0.9rem 2.2rem;
  font-size: 1.2rem;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.2s;
  text-decoration: none;
  display: inline-block;
}
.btn-view-score:hover, .btn-next-part:hover, .btn-back-home:hover {
  background: #0097a7;
}
</style>

