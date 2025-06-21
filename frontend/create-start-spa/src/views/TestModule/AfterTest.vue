<template>
  <div class="aftertest-wrapper">
    <h2>Bạn đã hoàn thành {{ testTitle }}</h2>
    <div v-if="hasWriting">
      <h3>AI Feedback cho bài Writing</h3>
      <div v-if="loadingWritingScores">Đang lấy phản hồi AI cho các bài viết...</div>
      <ul v-else>
        <li v-for="(ws, idx) in writingScores" :key="ws.index" class="feedback-item">
          <b>Bài viết {{ idx + 1 }}:</b>
          <div class="ai-feedback">
            <span class="ai-icon">🤖</span>
            <span v-html="formatFeedback(ws.feedback)"></span>
          </div>
          <div class="manual-score">
            <label>Nhập điểm bạn tự chấm (theo AI gợi ý): </label>
            <input type="number" min="0" max="9" step="0.5" v-model.number="manualWritingScores[idx]" style="width:60px;"> / 9
          </div>
        </li>
      </ul>
      <div class="aftertest-actions">
        <button v-if="hasNextPart" class="btn-next-part" @click="goToNextPart">Làm tiếp {{ nextPartLabel }}</button>
        <button class="btn-view-score" @click="submitManualScores">Hoàn thành & Xem tổng kết</button>
        <router-link class="btn-back-home" to="/">Về trang chủ</router-link>
      </div>
    </div>
    <div v-else-if="hasSpeaking">
      <h3>AI Feedback cho bài Speaking</h3>
      <div v-if="loadingSpeakingScores">Đang lấy phản hồi AI cho các bài nói...</div>
      <ul v-else>
        <li v-for="(ss, idx) in speakingScores" :key="ss.index" class="feedback-item">
          <b>Bài nói {{ idx + 1 }}:</b>
          <div class="ai-feedback">
            <span class="ai-icon">🗣️</span>
            <span v-html="formatFeedback(ss.feedback)"></span>
          </div>
          <div class="manual-score">
            <label>Nhập điểm bạn tự chấm (theo AI gợi ý): </label>
            <input type="number" min="0" max="9" step="0.5" v-model.number="manualSpeakingScores[idx]" style="width:60px;"> / 9
          </div>
        </li>
      </ul>
      <div class="aftertest-actions">
        <button v-if="hasNextPart" class="btn-next-part" @click="goToNextPart">Làm tiếp {{ nextPartLabel }}</button>
        <button class="btn-view-score" @click="submitManualScores">Hoàn thành & Xem tổng kết</button>
        <router-link class="btn-back-home" to="/">Về trang chủ</router-link>
      </div>
    </div>
    <div v-else>
      <div class="aftertest-actions">
        <button class="btn-view-score" @click="viewScore">Xem điểm</button>
        <button v-if="hasNextPart" class="btn-next-part" @click="goToNextPart">Làm tiếp {{ nextPartLabel }}</button>
        <router-link class="btn-back-home" to="/">Về trang chủ</router-link>
      </div>
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
      writingAnswers: [],
      writingScores: [],
      speakingAnswers: [],
      speakingScores: [],
      loadingWritingScores: false,
      loadingSpeakingScores: false,
      hasWriting: false,
      hasSpeaking: false,
      manualWritingScores: [],
      manualSpeakingScores: [],
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
    // Lấy danh sách câu hỏi writing từ localStorage (userAnswers)
    const userAnswers = JSON.parse(localStorage.getItem('userAnswers') || '[]');
    this.hasWriting = userAnswers.some(a => a.questionType === 'writting');
    this.hasSpeaking = userAnswers.some(a => a.questionType === 'speaking');
    this.writingAnswers = userAnswers.filter(a => a.questionType === 'writting').map(a => String(a.answer || ''));
    this.speakingAnswers = userAnswers.filter(a => a.questionType === 'speaking').map(a => String(a.answer || ''));
    // Nếu có writing, lấy AI feedback cho writing
    if (this.hasWriting) {
      this.loadingWritingScores = true;
      this.writingScores = [];
      for (let idx = 0; idx < this.writingAnswers.length; idx++) {
        const textAnswer = this.writingAnswers[idx];
        try {
          const prompt = `Hãy đóng vai trò là giám khảo IELTS writing, hãy nhận xét và góp ý chi tiết cho bài viết sau bằng tiếng Việt:`;
          const promptText = JSON.stringify(prompt + '\n' + textAnswer);
          const res = await fetch('http://localhost:5067/api/Courses/deepseek-demo', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: promptText
          });
          let feedback = '';
          const contentType = res.headers.get('content-type') || '';
          if (contentType.includes('text/plain')) {
            feedback = await res.text();
          } else {
            const data = await res.json();
            feedback = data.result || data || 'Không nhận được phản hồi.';
          }
          this.writingScores.push({ index: idx, feedback });
        } catch (e) {
          this.writingScores.push({ index: idx, feedback: 'Không nhận được phản hồi.' });
        }
      }
      this.loadingWritingScores = false;
    }
    // Nếu có speaking, lấy AI feedback cho speaking (dựa trên text)
    if (this.hasSpeaking) {
      this.loadingSpeakingScores = true;
      this.speakingScores = [];
      for (let idx = 0; idx < this.speakingAnswers.length; idx++) {
        const textAnswer = this.speakingAnswers[idx];
        try {
          const prompt = `Hãy đóng vai trò là giám khảo IELTS speaking, hãy nhận xét và góp ý chi tiết cho phần nói sau bằng tiếng Việt:`;
          const promptText = JSON.stringify(prompt + '\n' + textAnswer);
          const res = await fetch('http://localhost:5067/api/Courses/deepseek-demo', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: promptText
          });
          let feedback = '';
          const contentType = res.headers.get('content-type') || '';
          if (contentType.includes('text/plain')) {
            feedback = await res.text();
          } else {
            const data = await res.json();
            feedback = data.result || data || 'Không nhận được phản hồi.';
          }
          this.speakingScores.push({ index: idx, feedback });
        } catch (e) {
          this.speakingScores.push({ index: idx, feedback: 'Không nhận được phản hồi.' });
        }
      }
      this.loadingSpeakingScores = false;
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
    submitManualScores() {
      // Lưu điểm tự chấm vào localStorage
      const writing = this.manualWritingScores.filter(x => typeof x === 'number');
      const speaking = this.manualSpeakingScores.filter(x => typeof x === 'number');
      const manualScores = { writing, speaking };
      localStorage.setItem('manualScores', JSON.stringify(manualScores));
      // Chuyển sang trang ScorePage
      this.$router.push({ name: "ScorePage" });
    },
    formatFeedback(text) {
      if (!text) return '';
      let html = text
        .replace(/\n/g, '<br>')
        .replace(/(\b[0-9](?:\.[05])?\b)/g, '<span class="score-highlight">$1</span>')
        .replace(/(\b(tốt|chưa tốt|cần cải thiện|ưu điểm|nhược điểm|góp ý|điểm mạnh|điểm yếu)\b)/gi, '<span class="keyword">$1</span>');
      return html;
    }
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
.ai-feedback {
  background: #f5f5f5;
  border-radius: 8px;
  padding: 1rem;
  margin: 0.7rem 0 1.2rem 0;
  text-align: left;
  font-size: 1.08rem;
}
.feedback-item {
  text-align: left;
}
.ai-icon {
  margin-right: 0.5rem;
}
.score-highlight {
  background: #e1f5fe;
  padding: 0.2rem 0.4rem;
  border-radius: 4px;
}
.keyword {
  font-weight: 600;
  color: #d32f2f;
}
.manual-score {
  margin-top: 0.5rem;
  text-align: left;
}
.manual-score label {
  font-size: 0.9rem;
  margin-right: 0.5rem;
}
.manual-score input {
  font-size: 1rem;
  text-align: center;
}
</style>

