<template>
  <div class="test-detail-wrapper" v-if="lesson">
    <div class="test-header">
      <div class="test-tags">
        <span v-for="tag in lesson.tags || []" :key="tag" class="test-tag">#{{ tag }}</span>
      </div>
      <h1>{{ lesson.title }}</h1>
      <button class="btn-info">Thông tin đề thi</button>
      <div class="lesson-content" v-if="lesson.content" v-html="lesson.content"></div>
      <div class="test-meta">
        <span> {{ tests.length }} phần thi</span>
      </div>
      <div class="test-note">
        <span style="color: #e53935; font-weight: 500;">
          Chú ý: để được quy đổi sang scaled score (ví dụ trên thang điểm 990 cho TOEIC hoặc 9.0 cho IELTS), vui lòng chọn chế độ làm FULL TEST.
        </span>
      </div>
    </div>
    <div class="test-tabs">
      <button class="tab active">Luyện tập</button>
      <button class="tab">Làm full test</button>
    </div>
    <div class="test-protip">
      <span style="color: #43a047; font-weight: bold;">💡 Pro tips:</span>
      <span>
        Hình thức luyện tập từng phần và chọn mức thời gian phù hợp sẽ giúp bạn tập trung vào giải đúng các câu hỏi thay vì phải chịu áp lực hoàn thành bài thi.
      </span>
    </div>
    <div class="test-select-section">
      <div class="test-select-title">Chọn phần thi bạn muốn làm</div>
      <div v-if="tests.length">
        <div v-for="test in tests" :key="test.id" class="test-select-item">
          <input type="checkbox" :id="'test-' + test.id" />
          <label :for="'test-' + test.id">
            {{ test.title }}
          </label>
          <div class="test-tags">
            <span v-for="tag in test.tags || []" :key="tag" class="test-tag">#{{ tag }}</span>
          </div>
          <button class="btn-do-test" @click="goToAttempt(test.id)">Làm bài</button>
        </div>
      </div>
      <div v-else>
        <i>Không có phần thi nào.</i>
      </div>
    </div>
  </div>
  <div v-else>
    <p>Đang tải thông tin...</p>
  </div>
</template>

<script>
import testService from "@/services/testService";

export default {
  name: "TestPage",
  props: ["lessonId"],
  data() {
    return {
      lesson: null,
      tests: [],
    };
  },
  async mounted() {
    const id = this.lessonId || this.$route.params.lessonId;
    this.lesson = await testService.getLessonDetail(id);
    this.tests = await testService.getTestsByLesson(id);
  },
  methods: {
    goToAttempt(testId) {
      const test = this.tests.find(t => t.id === testId);
      if (test && test.skill && test.skill.toLowerCase() === 'speaking') {
        this.$router.push({ name: "SpeakingTest", params: { testId: String(testId) } });
      } else {
        this.$router.push({ name: "TestAttempt", params: { testId: String(testId) } });
      }
    },
  },
};
</script>

<style scoped>
.test-detail-wrapper {
  max-width: 1200px;
  margin: 2.5rem auto;
  background: #fff;
  border-radius: 24px;
  box-shadow: 0 8px 32px rgba(30, 60, 100, 0.13);
  padding: 3rem 3rem 2.5rem 3rem;
  font-size: 1.25rem;
}
.test-header h1 {
  font-size: 2.6rem;
  font-weight: bold;
  margin-bottom: 0.7rem;
}
.test-tags {
  margin-bottom: 0.7rem;
}
.test-tag {
  display: inline-block;
  background: #e3f2fd;
  color: #1976d2;
  border-radius: 8px;
  padding: 0.3rem 1rem;
  margin-right: 0.7rem;
  font-size: 1.1rem;
}
.btn-info {
  background: #1976d2;
  color: #fff;
  border: none;
  border-radius: 8px;
  padding: 0.5rem 1.7rem;
  font-weight: 500;
  margin-bottom: 1.2rem;
  cursor: pointer;
  font-size: 1.1rem;
}
.test-meta {
  color: #444;
  font-size: 1.2rem;
  margin-bottom: 0.7rem;
}
.test-note {
  margin: 1.2rem 0;
  font-size: 1.1rem;
}
.test-tabs {
  display: flex;
  gap: 1.5rem;
  margin-bottom: 1.2rem;
}
.tab {
  background: #f5f5f5;
  border: none;
  border-radius: 8px 8px 0 0;
  padding: 1rem 2.2rem;
  font-weight: 500;
  cursor: pointer;
  font-size: 1.1rem;
}
.tab.active {
  background: #fff;
  border-bottom: 2px solid #1976d2;
  color: #1976d2;
}
.test-protip {
  background: #e8f5e9;
  border-radius: 12px;
  padding: 1.2rem;
  margin-bottom: 2rem;
  color: #388e3c;
  font-size: 1.1rem;
}
.test-select-section {
  margin-top: 2rem;
}
.test-select-title {
  font-weight: bold;
  margin-bottom: 1.2rem;
  font-size: 1.2rem;
}
.test-select-item {
  display: flex;
  align-items: center;
  gap: 1.2rem;
  margin-bottom: 1rem;
  font-size: 1.1rem;
}
.lesson-content {
  margin: 1.5rem 0 2rem 0;
  font-size: 1.18rem;
  color: #333;
  line-height: 1.7;
  background: #f8fafd;
  border-radius: 10px;
  padding: 1.2rem 1.5rem;
  word-break: break-word;
}
.btn-do-test {
  background: #00bcd4;
  color: #fff;
  border: none;
  border-radius: 6px;
  padding: 0.4rem 1.2rem;
  font-weight: 600;
  cursor: pointer;
  margin-left: 1rem;
  transition: background 0.2s;
}
.btn-do-test:hover {
  background: #0097a7;
}
@media (max-width: 900px) {
  .test-detail-wrapper {
    padding: 1.2rem 0.5rem;
    max-width: 100vw;
    font-size: 1rem;
  }
  .test-header h1 {
    font-size: 1.5rem;
  }
}
</style>
