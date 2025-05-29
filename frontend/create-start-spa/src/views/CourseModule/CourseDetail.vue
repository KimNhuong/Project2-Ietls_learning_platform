<template>
  <div class="course-detail-wrapper" v-if="course">
    <div class="course-header">
      <img :src="course.imageUrl || image" class="course-thumb" />
      <div class="course-info">
        <h1>{{ course.title }}</h1>
        <div class="course-rating">
          <span class="rating-number">{{ displayRating.toFixed(1) }}</span>
          <span class="stars" @click.stop>
            <span
              v-for="star in 5"
              :key="star"
              class="star"
              :class="{ filled: star <= Math.round(displayRating), clickable: !userVote }"
              @click="!userVote && handleVote(star)"
              style="cursor: pointer"
            >★</span
            >
          </span>
          <span class="votes">({{ displayVotes }} votes)</span>
          <span v-if="userVote" style="color: #00bcd4; margin-left: 8px;">Bạn đã vote</span>
        </div>
        <div class="course-meta">
          <span>Đăng ngày: <b>{{ formatDate(course.createdAt) }}</b></span>
          <span>Tests taken: <b>{{ course.testsTaken?.toLocaleString() || '0' }}</b></span>
        </div>
      </div>
    </div>
    <div class="course-description">
      <p>{{ course.description }}</p>
    </div>
    <div class="lessons-section">
      <h2>Lessons</h2>
      <div class="lessons-list">
        <div
          class="lesson-card"
          v-for="lesson in lessons"
          :key="lesson.id"
        >
          <div class="lesson-icon">
            <img
              v-if="getLessonSvgUrl(lesson.title)"
              :src="getLessonSvgUrl(lesson.title)"
              class="lesson-svg"
              alt="icon"
            />
          </div>
          <div class="lesson-title">{{ lesson.title }}</div>
          <button class="btn-do-lesson" @click="goToAnswer(lesson.id)">Làm bài</button>
          <div class="lesson-key" @click="goToAnswer(lesson.id)">
            <img :src="KeyIconUrl" class="key-svg" alt="Key" />
          </div>
        </div>
      </div>
      <!-- Full Test Section -->
      <div class="full-test-card">
        <span class="new-label">NEW</span>
        <span class="full-test-title">Full Test</span>
        <div class="progress-bar">
          <div class="progress" :style="{ width: '0%' }"></div>
        </div>
        <button class="btn-full-test">Start</button>
      </div>
    </div>
  </div>
  <div v-else>
    <p>Loading...</p>
  </div>
</template>

<script>
import CourseService from "@/services/CourseService";
import "./Courses.css";

import ListeningIconUrl from "@/assets/headphone.png";
import ReadingIconUrl from "@/assets/reading.png";
import WritingIconUrl from "@/assets/notebook.png";
import SpeakingIconUrl from "@/assets/Mic.png";
import KeyIconUrl from "@/assets/KeyIcon.png";

export default {
  name: "CourseDetail",
  data() {
    return {
      course: null,
      image: "/assets/courses1.jpg",
      lessons: [],
      userVote: null,
      KeyIconUrl,
    };
  },
  computed: {
    displayRating() {
      if (!this.course) return 0;
      if (this.userVote) {
        return ((this.course.ratings * (this.course.votes || 0) + this.userVote) / ((this.course.votes || 0) + 1));
      }
      return this.course.ratings || 0;
    },
    displayVotes() {
      if (!this.course) return 0;
      return (this.course.votes || 0) + (this.userVote ? 1 : 0);
    }
  },
  async mounted() {
    const id = this.$route.params.id;
    try {
      this.course = await CourseService.getCourseDetail(id);
      this.lessons = await CourseService.getLessonsByCourse(id);
      // Lấy vote của user từ localStorage
      const vote = localStorage.getItem(`course_votes_${id}`);
      if (vote) this.userVote = Number(vote);
    } catch (err) {
      this.course = null;
      this.lessons = [];
    }
  },
  methods: {
    formatDate(dateStr) {
      if (!dateStr) return "";
      const d = new Date(dateStr);
      return d.toLocaleDateString();
    },
    getLessonIcon(title) {
      if (!title) return "fas fa-book";
      const t = title.toLowerCase();
      if (t.includes("listen")) return "fas fa-headphones";
      if (t.includes("read")) return "fas fa-book-open";
      if (t.includes("write")) return "fas fa-pen-nib";
      if (t.includes("speak")) return "fas fa-microphone";
      return "fas fa-book";
    },
    handleVote(star) {
      if (this.userVote) return;
      const id = this.$route.params.id;
      localStorage.setItem(`course_votes_${id}`, star);
      this.userVote = star;
    },
    getLessonSvgUrl(title) {
      if (!title) return null;
      const t = title.trim().toLowerCase();
      if (t === "listening") return ListeningIconUrl;
      if (t === "reading") return ReadingIconUrl;
      if (t === "writing") return WritingIconUrl;
      if (t === "speaking") return SpeakingIconUrl;
      return null;
    },
    goToAnswer(lessonId) {
      this.$router.push({ name: "TestPage", params: { lessonId: String(lessonId) } });
    },
  },
};
</script>

<style scoped>
.course-detail-wrapper {
  max-width: 900px;
  margin: 2rem auto;
  background: #fff;
  border-radius: 18px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.08);
  padding: 2rem;
}
.course-header {
  display: flex;
  align-items: center;
  gap: 2rem;
}
.course-thumb {
  width: 160px;
  height: 160px;
  border-radius: 16px;
  object-fit: cover;
  box-shadow: 0 2px 8px rgba(30, 60, 100, 0.08);
}
.course-info h1 {
  font-size: 2.2rem;
  font-weight: bold;
  margin-bottom: 0.5rem;
}
.course-rating {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  font-size: 1.1rem;
  margin-bottom: 0.5rem;
}
.rating-number {
  color: #e67e22;
  font-weight: bold;
  font-size: 1.2rem;
}
.stars {
  color: #ffc107;
}
.star {
  font-size: 1.2rem;
}
.star.filled {
  color: #ffc107;
}
.votes {
  color: #888;
  font-size: 0.95rem;
}
.course-meta {
  color: #444;
  font-size: 1rem;
  margin-bottom: 0.5rem;
  display: flex;
  gap: 2rem;
}
.course-description {
  margin: 1.5rem 0;
  font-size: 1.1rem;
  color: #333;
}
.lessons-section {
  margin-top: 2rem;
}
.lessons-list {
  display: flex;
  gap: 1.5rem;
  flex-wrap: wrap;
  margin-bottom: 1.5rem;
}
.lesson-card {
  background: #f8fafc;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(30, 60, 100, 0.06);
  padding: 1.2rem 1.5rem;
  min-width: 210px;
  max-width: 220px;
  display: flex;
  flex-direction: column;
  align-items: center;
  position: relative;
  text-align: center;
}
.lesson-icon {
  width: 48px;
  height: 48px;
  margin-bottom: 0.5rem;
  display: flex;
  align-items: center;
  justify-content: center;
}
.lesson-svg {
  width: 40px;
  height: 40px;
  object-fit: contain;
}
.lesson-title {
  font-weight: 600;
  margin-bottom: 0.7rem;
  color: #333;
  min-height: 32px;
  display: flex;
  align-items: center;
  justify-content: center;
}
.btn-do-lesson {
  background: #00bcd4;
  color: #fff;
  border: none;
  border-radius: 6px;
  padding: 0.5rem 1.2rem;
  font-weight: 600;
  cursor: pointer;
  margin-top: 0.5rem;
  transition: background 0.2s;
  width: 100%;
}
.btn-do-lesson:hover {
  background: #0097a7;
}
.lesson-key {
  width: 28px;
  height: 28px;
  margin-top: 0.7rem;
  display: flex;
  align-items: center;
  justify-content: center;
}
.key-svg {
  width: 24px;
  height: 24px;
  object-fit: contain;
}
.full-test-card {
  background: #f4f4f9;
  border-radius: 12px;
  padding: 1.2rem 1.5rem;
  display: flex;
  align-items: center;
  gap: 1.2rem;
  margin-top: 1rem;
  position: relative;
}
.new-label {
  background: #e53935;
  color: #fff;
  font-size: 0.8rem;
  font-weight: bold;
  border-radius: 6px;
  padding: 0.2rem 0.7rem;
  position: absolute;
  left: -1.5rem;
  top: 1rem;
  transform: rotate(-20deg);
}
.full-test-title {
  font-weight: 600;
  font-size: 1.1rem;
  margin-right: 1.2rem;
}
.progress-bar {
  flex: 1;
  height: 12px;
  background: #e0e0e0;
  border-radius: 6px;
  margin: 0 1rem;
  overflow: hidden;
  position: relative;
}
.progress {
  height: 100%;
  background: #00bcd4;
  border-radius: 6px;
  width: 0%;
  transition: width 0.3s;
}
.btn-full-test {
  background: #222;
  color: #fff;
  border: none;
  border-radius: 6px;
  padding: 0.5rem 1.5rem;
  font-weight: 600;
  cursor: pointer;
  margin-left: 1rem;
  transition: background 0.2s;
}
.btn-full-test:hover {
  background: #00bcd4;
}
@media (max-width: 900px) {
  .course-header {
    flex-direction: column;
    align-items: flex-start;
    gap: 1rem;
  }
  .lessons-list {
    flex-direction: column;
    gap: 1rem;
  }
}
</style>
