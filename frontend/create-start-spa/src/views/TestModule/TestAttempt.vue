<template>
  <div class="attempt-wrapper" v-if="questions.length">
    <h2>Làm bài Test</h2>
    <form @submit.prevent="submit">
      <div v-for="(q, idx) in questions" :key="q.id" class="question-block">
        <div class="question-content">
          <span class="q-order">Câu {{ idx + 1 }}.</span>
          <span v-html="q.content"></span>
        </div>
        <div v-if="q.questionType === 'writting'">
          <div class="writing-block">
            <textarea v-model="answers[q.id]" rows="5" placeholder="Viết đoạn văn trả lời..." class="answer-input"></textarea>
            <button type="button" class="btn-save" @click="saveWritingAnswer(q.id)">Lưu câu trả lời</button>
          </div>
        </div>
        <div v-else-if="q.questionType === 'multiplechoice'">
          <div v-for="opt in getOptions(q)" :key="opt" class="mc-option">
            <input
              type="radio"
              :name="'q_' + q.id"
              :value="opt"
              v-model="answers[q.id]"
              :id="'q_' + q.id + '_' + opt"
            />
            <label :for="'q_' + q.id + '_' + opt">{{ opt }}</label>
          </div>
        </div>
        <div v-else-if="q.questionType === 'fillinblank'">
          <input v-model="answers[q.id]" class="answer-input" placeholder="Điền đáp án..." />
        </div>
        <div v-else-if="q.questionType === 'truefalse'">
          <div class="mc-option">
            <input type="radio" :name="'q_' + q.id" value="T" v-model="answers[q.id]" :id="'q_' + q.id + '_T'" />
            <label :for="'q_' + q.id + '_T'">True</label>
            <input type="radio" :name="'q_' + q.id" value="F" v-model="answers[q.id]" :id="'q_' + q.id + '_F'" />
            <label :for="'q_' + q.id + '_F'">False</label>
            <input type="radio" :name="'q_' + q.id" value="NG" v-model="answers[q.id]" :id="'q_' + q.id + '_NG'" />
            <label :for="'q_' + q.id + '_NG'">Not Given</label>
          </div>
        </div>
      </div>
      <button class="btn-submit" type="submit">Nộp bài</button>
    </form>
  </div>
  <div v-else>
    <p>Đang tải câu hỏi...</p>
  </div>
</template>

<script>
import testService from "@/services/testService";

export default {
  name: "TestAttempt",
  props: ["testId"],
  data() {
    return {
      questions: [],
      answers: {},
    };
  },
  async mounted() {
    // Lấy userId từ localStorage, nếu chưa có thì gọi API lấy user
    let user = JSON.parse(localStorage.getItem("user") || "null");
    if (!user || !user.userId) {
      const token = localStorage.getItem("token");
      if (token) {
        // Giả sử backend có API /api/Users/me trả về user hiện tại, nếu không thì decode userId từ token hoặc lấy từ username
        // Nếu không có /me, bạn cần biết userId hoặc username
        // Ví dụ: lấy userId từ token hoặc từ username đã lưu
        // Ở đây giả sử bạn đã biết userId (ví dụ lưu ở localStorage khi đăng nhập)
        // Nếu không, bạn cần sửa backend để có API /me hoặc trả về userId khi login
        // Ví dụ tạm thời lấy userId từ token (nếu token là JWT chuẩn)
        // Nếu không có, bạn cần yêu cầu backend hỗ trợ
      }
    }
    // Nếu user đã có userId, không cần gọi lại nữa
    this.questions = await testService.getQuestionsByTest(this.testId || this.$route.params.testId);
  },
  methods: {
    getOptions(q) {
      return q.options || ["A", "B", "C", "D"];
    },
    async saveWritingAnswer(questionId) {
      const userId = this.getUserId();
      try {
        await testService.saveWritingAnswer({
          userId,
          questionId,
          textAnswer: this.answers[questionId]
        });
        // Always show a small popup/toast on success
        if (this.$toast && this.$toast.success) {
          this.$toast.success("Đã lưu câu trả lời!", { timeout: 1800, position: "top-right" });
        } else {
          window.alert("Đã lưu câu trả lời!");
        }
      } catch (e) {
        if (this.$toast && this.$toast.error) {
          this.$toast.error("Lưu thất bại!");
        } else {
          window.alert("Lưu thất bại!");
        }
      }
    },
    getUserId() {
      // Lấy userId từ localStorage hoặc JWT nếu có
      const user = JSON.parse(localStorage.getItem("userID") || "null");
      return user?.userId || 0;
    },
    submit() {
      // Sau khi nộp bài, chuyển hướng sang trang chat DeepSeek
      this.$router.push({ name: "ChatDeepSeek" });
    },
  },
};
</script>

<style scoped>
.attempt-wrapper {
  max-width: 900px;
  margin: 2rem auto;
  background: #fff;
  border-radius: 18px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.08);
  padding: 2rem;
}
.question-block {
  margin-bottom: 2rem;
  padding-bottom: 1.2rem;
  border-bottom: 1px solid #e0e0e0;
}
.question-content {
  font-weight: 500;
  margin-bottom: 0.7rem;
  font-size: 1.15rem;
}
.q-order {
  color: #00bcd4;
  font-weight: bold;
  margin-right: 0.5rem;
}
.answer-input {
  width: 100%;
  border: 1.5px solid #bdbdbd;
  border-radius: 6px;
  padding: 0.7rem;
  font-size: 1.1rem;
  margin-bottom: 0.7rem;
  margin-top: 0.2rem;
}
.mc-option {
  margin-bottom: 0.5rem;
  display: flex;
  align-items: center;
  gap: 0.7rem;
}
.btn-submit {
  background: #00bcd4;
  color: #fff;
  border: none;
  border-radius: 8px;
  padding: 0.9rem 2.2rem;
  font-size: 1.2rem;
  font-weight: 600;
  cursor: pointer;
  margin-top: 1.5rem;
  transition: background 0.2s;
}
.btn-submit:hover {
  background: #0097a7;
}
.writing-block {
  position: relative;
}
.btn-save {
  background: #4caf50;
  color: #fff;
  border: none;
  border-radius: 6px;
  padding: 0.6rem 1.2rem;
  font-size: 1rem;
  font-weight: 500;
  cursor: pointer;
  margin-top: 0.5rem;
  transition: background 0.2s;
}
.btn-save:hover {
  background: #43a047;
}
</style>
