<template>
  <div class="course-detail-container" v-if="course">
    <h1>{{ course.name || course.title }}</h1>
    <img :src="image" alt="Course Image" class="course-image" />
    <p><strong>Author:</strong> {{ course.author || "Unknown" }}</p>
    <p>
      <strong>Description:</strong> {{ course.description || "No description" }}
    </p>
    <div class="rating">
      <span
        v-for="star in 5"
        :key="star"
        class="star"
        :class="{ filled: star <= (course.rating || 5) }"
        >★</span
      >
    </div>
    <!-- Thêm các thông tin khác nếu cần -->
  </div>
  <div v-else>
    <p>Loading...</p>
  </div>
</template>

<script>
import CourseService from "@/services/CourseService";

export default {
  name: "CourseDetail",
  data() {
    return {
      course: null,
      image: "/assets/courses1.jpg",
    };
  },
  async created() {
    const id = this.$route.params.id;
    try {
      this.course = await CourseService.getCourseDetail(id);
    } catch (err) {
      this.course = null;
    }
  },
};
</script>

<style scoped>
.course-detail-container {
  max-width: 700px;
  margin: 2rem auto;
  padding: 2rem;
  background: #fff;
  border-radius: 16px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.08);
  text-align: center;
}
.course-image {
  width: 300px;
  height: 180px;
  object-fit: cover;
  border-radius: 8px;
  margin-bottom: 1rem;
}
.rating {
  color: #ffc107;
  margin-top: 1rem;
}
.star {
  font-size: 1.3rem;
}
.star.filled {
  color: #ffc107;
}
</style>
