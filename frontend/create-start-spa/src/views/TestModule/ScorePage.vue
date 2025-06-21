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
    <div v-if="manualScores && (manualScores.writing.length || manualScores.speaking.length)">
      <h3>Điểm tự chấm (theo AI feedback)</h3>
      <div v-if="manualScores.writing.length">
        <p><b>Writing:</b> {{ manualScores.writing.join(', ') }} / 9</p>
      </div>
      <div v-if="manualScores.speaking.length">
        <p><b>Speaking:</b> {{ manualScores.speaking.join(', ') }} / 9</p>
      </div>
    </div>
    <div v-if="overallScore !== null">
      <h3>Điểm tổng hợp (Overall) cho lesson</h3>
      <p><b>{{ overallScore }}</b> / 9</p>
    </div>
    <router-link to="/">Quay về trang chủ</router-link>
  </div>
</template>

<script>
export default {
  name: "ScorePage",
  data() {
    return {
      result: null,
      manualScores: { writing: [], speaking: [] }
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
    },
    overallScore() {
      // Tính điểm overall cho lesson: trung bình cộng các điểm writing, speaking, và điểm test nếu có
      let scores = [];
      if (this.manualScores && this.manualScores.writing.length) {
        scores = scores.concat(this.manualScores.writing);
      }
      if (this.manualScores && this.manualScores.speaking.length) {
        scores = scores.concat(this.manualScores.speaking);
      }
      if (this.result && this.result.total) {
        // Quy đổi điểm test sang thang 9
        const percent = this.result.score / this.result.total;
        let testScore = Math.round(percent * 9 * 2) / 2;
        scores.push(testScore);
      }
      if (!scores.length) return null;
      // Trung bình cộng, làm tròn về .0 hoặc .5
      const avg = scores.reduce((a, b) => a + b, 0) / scores.length;
      return Math.round(avg * 2) / 2;
    }
  },
  mounted() {
    const res = localStorage.getItem('testResult');
    if (res) {
      this.result = JSON.parse(res);
    }
    const ms = localStorage.getItem('manualScores');
    if (ms) {
      this.manualScores = JSON.parse(ms);
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
