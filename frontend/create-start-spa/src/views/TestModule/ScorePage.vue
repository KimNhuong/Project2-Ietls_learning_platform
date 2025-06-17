<template>
  <div class="score-page">
    <h2>Kết quả bài Test</h2>
    <div v-if="result">
      <p>Bạn đã trả lời đúng <b>{{ result.score }}</b> trên tổng số <b>{{ result.total }}</b> câu hỏi.</p>
      <p>Điểm số: <b>{{ Math.round((result.score / result.total) * 100) }}</b></p>
      <p>Quy đổi IELTS: <b>{{ ieltsScore }}</b></p>
    </div>
    <div v-else>
      <p>Không tìm thấy kết quả. Vui lòng làm lại bài test.</p>
    </div>
    <router-link to="/">Quay về trang chủ</router-link>
  </div>
</template>

<script>
export default {
  name: "ScorePage",
  data() {
    return {
      result: null
    };
  },
  computed: {
    ieltsScore() {
      if (!this.result || !this.result.total) return 0;
      // Quy đổi điểm sang thang IELTS 0-9, làm tròn về .0 hoặc .5
      const percent = this.result.score / this.result.total;
      let raw = percent * 9;
      // Làm tròn về .0 hoặc .5
      return Math.round(raw * 2) / 2;
    }
  },
  mounted() {
    const res = localStorage.getItem('testResult');
    if (res) {
      this.result = JSON.parse(res);
    }
  }
};
</script>

<style scoped>
.score-page {
  max-width: 500px;
  margin: 3rem auto;
  background: #fff;
  border-radius: 16px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.08);
  padding: 2rem 2.5rem;
  text-align: center;
}
h2 {
  color: #00bcd4;
  margin-bottom: 1.5rem;
}
p {
  font-size: 1.2rem;
  margin-bottom: 1rem;
}
b {
  color: #0097a7;
}
</style>
