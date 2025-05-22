<template>
  <div class="courses-page">
    <div class="search-section">
      <h2>Look for any courses</h2>
      <input class="search-bar" type="text" placeholder="Search courses..." />
    </div>

    <div class="courses-container">
      <Card
        v-for="(course, index) in courses"
        :key="index"
        :image="course.image"
        :title="course.title"
        :description="course.description"
      />
    </div>
  </div>
</template>

<script>
import Card from "@/components/Card.vue";

export default {
  name: "CoursesPage",
  components: {
    Card,
  },
  data() {
    return {
      courses: [
        {
          image: "/assets/courses1.jpg",
          title: "IELTS Reading Skills",
          description: "Comprehension, Skimming, Scanning & Practice Tests",
        },
        {
          image: "/assets/courses2.jpg",
          title: "IELTS Speaking Boost",
          description: "Strategies, examples, mock interviews",
        },
        {
          image: "/assets/courses1.jpg",
          title: "IELTS Reading Skills",
          description: "Comprehension, Skimming, Scanning & Practice Tests",
        },
        {
          image: "/assets/courses1.jpg",
          title: "IELTS Reading Skills",
          description: "Comprehension, Skimming, Scanning & Practice Tests",
        },
        {
          image: "/assets/courses1.jpg",
          title: "IELTS Reading Skills",
          description: "Comprehension, Skimming, Scanning & Practice Tests",
        },
        {
          image: "/assets/courses1.jpg",
          title: "IELTS Reading Skills",
          description: "Comprehension, Skimming, Scanning & Practice Tests",
        },
      ],
    };
  },
};
</script>

