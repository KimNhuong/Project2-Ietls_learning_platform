<template>
  <div class="courses-hero-bg">
    <div class="courses-header-section">
      <div class="courses-header-overlay"></div>
      <div class="courses-header-content">
        <h1>Search for anything</h1>
        <div class="search-bar-container">
          <input
            class="search-bar"
            type="text"
            placeholder="Hinted search text"
            v-model="searchKeyword"
            @keyup.enter="handleSearch"
          />
          <button class="search-button" @click="handleSearch">
            <i class="fas fa-search"></i> Find
          </button>
        </div>
      </div>
    </div>

    <transition name="fade-slide">
      <div class="main-content">
        <!-- Filter Section -->
        <div class="filter-section">
          <h3>Ratings</h3>
          <ul class="ratings-list">
            <li>
              <input type="radio" name="rating" value="5" v-model.number="selectedRating" id="rating-5" />
              <label for="rating-5">5 star</label>
            </li>
            <li>
              <input type="radio" name="rating" value="4" v-model.number="selectedRating" id="rating-4" />
              <label for="rating-4">4 stars & up</label>
            </li>
            <li>
              <input type="radio" name="rating" value="3" v-model.number="selectedRating" id="rating-3" />
              <label for="rating-3">3 stars & up</label>
            </li>
            <li>
              <input type="radio" name="rating" value="2" v-model.number="selectedRating" id="rating-2" />
              <label for="rating-2">2 stars & up</label>
            </li>
            <li>
              <input type="radio" name="rating" value="1" v-model.number="selectedRating" id="rating-1" />
              <label for="rating-1">1 star & up</label>
            </li>
            <li>
              <input type="radio" name="rating" value="0" v-model.number="selectedRating" id="rating-all" />
              <label for="rating-all">All</label>
            </li>
          </ul>

          <h3>Levels</h3>
          <ul class="levels-list">
            <li>
              <input type="checkbox" value="B1" v-model="selectedLevels" id="level-b1" />
              <label for="level-b1">B1</label>
            </li>
            <li>
              <input type="checkbox" value="B2" v-model="selectedLevels" id="level-b2" />
              <label for="level-b2">B2</label>
            </li>
            <li>
              <input type="checkbox" value="C1" v-model="selectedLevels" id="level-c1" />
              <label for="level-c1">C1</label>
            </li>
            <li>
              <input type="checkbox" value="C2" v-model="selectedLevels" id="level-c2" />
              <label for="level-c2">C2</label>
            </li>
          </ul>
          <button class="btn btn-filter" @click="applyFilter">Apply</button>
          <button class="btn btn-reset" @click="resetFilter">Reset</button>
        </div>

        <!-- Results Section -->
        <div class="results-section">
          <div class="sort-section">
            <button class="btn sort-button">
              Sort by most popular <i class="fas fa-chevron-down"></i>
            </button>
          </div>
          <h3 class="results-title">{{ courses.length }} results</h3>
          <transition-group name="card-fade" tag="div">
            <div
              class="course-card"
              v-for="course in courses"
              :key="course.id"
              @click="goToCourseDetail(course.id)"
              style="cursor: pointer"
            >
              <img :src="course.imageUrl || '/assets/courses1.jpg'" :alt="course.title" />
              <div class="course-info">
                <h4>{{ course.title }}</h4>
                <p>{{ course.description }}</p>
                <p><strong>Level:</strong> {{ course.level }}</p>
                <p><strong>Creator:</strong> {{ course.creator?.username || 'Unknown' }}</p>
                <div class="rating">
                  <span
                    v-for="star in 5"
                    :key="star"
                    class="star"
                    :class="{ filled: star <= Math.round(course.ratings) }"
                  >★</span>
                  <span style="margin-left:8px; color:#888;">
                    ({{ course.votes || 0 }} votes)
                  </span>
                </div>
              </div>
            </div>
          </transition-group>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
import CourseService from "@/services/CourseService";
import "./Courses.css";

export default {
  name: "CoursesPage",
  data() {
    return {
      courses: [],
      allCourses: [],
      searchKeyword: "",
      selectedLevels: [],
      selectedRating: 0,
      userVotes: {},
    };
  },
  async mounted() {
    this.loadUserVotes();
    await this.loadCourses();
  },
  activated() {
    // Nếu dùng <keep-alive>, sẽ gọi khi quay lại trang này
    this.loadUserVotes();
    this.applyFilter();
  },
  methods: {
    loadUserVotes() {
      const votes = {};
      Object.keys(localStorage).forEach(key => {
        if (key.startsWith("course_votes_")) {
          const courseId = key.replace("course_votes_", "");
          votes[courseId] = Number(localStorage.getItem(key));
        }
      });
      this.userVotes = votes;
    },
    async loadCourses() {
      try {
        const data = await CourseService.getCourses();
        this.courses = data;
        this.allCourses = data;
        this.loadUserVotes(); // Đảm bảo votes luôn mới nhất
        this.applyFilter();
      } catch (err) {
        this.courses = [];
        this.allCourses = [];
      }
    },
    async handleSearch() {
      if (!this.searchKeyword) {
        await this.loadCourses();
        this.applyFilter();
        return;
      }
      try {
        const data = await CourseService.searchCourses(this.searchKeyword);
        this.courses = data;
        this.allCourses = data;
        this.loadUserVotes();
        this.applyFilter();
      } catch (err) {
        this.courses = [];
        this.allCourses = [];
      }
    },
    applyFilter() {
      let filtered = this.allCourses;
      if (this.selectedLevels.length > 0) {
        filtered = filtered.filter(c =>
          this.selectedLevels.includes((c.level || "").toUpperCase())
        );
      }
      if (this.selectedRating > 0) {
        filtered = filtered.filter(c => {
          const userVote = this.userVotes[c.id];
          const avg = userVote
            ? (c.ratings * c.votes + userVote) / (c.votes + 1)
            : c.ratings;
          return Math.round(avg) >= this.selectedRating;
        });
      }
      this.courses = filtered;
    },
    resetFilter() {
      this.selectedLevels = [];
      this.selectedRating = 0;
      this.courses = this.allCourses;
    },
    goToCourseDetail(id) {
      if (id) {
        this.$router.push({ name: "CourseDetail", params: { id: String(id) } });
      }
    },
  },
};
</script>
