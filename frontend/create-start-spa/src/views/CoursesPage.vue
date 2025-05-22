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
          />
          <button class="search-button">
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
            <li><input type="radio" name="rating" /> 5 star</li>
            <li><input type="radio" name="rating" /> 4 stars & up</li>
            <li><input type="radio" name="rating" /> 3 stars & up</li>
            <li><input type="radio" name="rating" /> 2 stars & up</li>
            <li><input type="radio" name="rating" /> 1 star & up</li>
          </ul>

          <h3>Levels</h3>
          <ul class="levels-list">
            <li><input type="checkbox" /> All levels</li>
            <li><input type="checkbox" /> Beginner</li>
            <li><input type="checkbox" /> Intermediate</li>
            <li><input type="checkbox" /> Expert</li>
          </ul>
          <button class="btn btn-filter">Apply</button>
          <button class="btn btn-reset">Reset</button>
        </div>

        <!-- Results Section -->
        <div class="results-section">
          <div class="sort-section">
            <button class="btn sort-button">
              Sort by most popular <i class="fas fa-chevron-down"></i>
            </button>
          </div>
          <h3 class="results-title">4 results</h3>
          <transition-group name="card-fade" tag="div">
            <div
              class="course-card"
              v-for="(course, index) in courses"
              :key="index"
            >
              <img :src="course.image" :alt="course.title" />
              <div class="course-info">
                <h4>{{ course.title }}</h4>
                <p>by {{ course.author }}</p>
                <div class="rating">
                  <span
                    v-for="star in 5"
                    :key="star"
                    class="star"
                    :class="{ filled: star <= course.rating }"
                    >★</span
                  >
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
export default {
  name: "CoursesPage",
  data() {
    return {
      courses: [
        {
          image: "/assets/courses1.jpg",
          title: "The complete IELTS writing bootcamp",
          author: "SOMEONE",
          rating: 5,
        },
        {
          image: "/assets/courses2.jpg",
          title: "The complete IELTS reading bootcamp",
          author: "SOMEONE",
          rating: 4,
        },
        {
          image: "/assets/courses3.jpg",
          title: "The complete IELTS listening bootcamp",
          author: "SOMEONE",
          rating: 4,
        },
        {
          image: "/assets/courses4.jpg",
          title: "The complete IELTS speaking bootcamp",
          author: "SOMEONE",
          rating: 3,
        },
      ],
    };
  },
};
</script>

<style scoped>
.courses-hero-bg {
  min-height: 100vh;
  background: linear-gradient(120deg, #5e97bb 100%, #232526 40%);
  display: flex;
  flex-direction: column;
}

.courses-header-section {
  position: relative;
  width: 100vw;
  min-height: 320px;
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;
}

.courses-header-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100%;
  background: rgba(30, 60, 100, 0.55);
  z-index: 1;
}

.courses-header-content {
  position: relative;
  z-index: 2;
  text-align: center;
  color: #fff;
  width: 100%;
  padding: 3rem 0 2rem 0;
}

.courses-header-content h1 {
  font-size: 2.8rem;
  font-weight: bold;
  margin-bottom: 1.5rem;
  letter-spacing: 1px;
  text-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
}

.search-bar-container {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 0.5rem;
}

.search-bar {
  width: 50%;
  padding: 0.75rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 1.1rem;
  box-sizing: border-box;
  background: #f8fafd;
  transition: box-shadow 0.2s;
}

.search-bar:focus {
  outline: none;
  box-shadow: 0 0 0 2px #00bcd4;
}

.search-button {
  padding: 0.75rem 1.2rem;
  background-color: #00bcd4;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-weight: 600;
  font-size: 1.1rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  transition: background 0.2s;
}

.search-button:hover {
  background-color: #0097a7;
}

.main-content {
  display: flex;
  gap: 2rem;
  padding: 2.5rem 5vw 2rem 5vw;
  flex: 1 0 auto;
}

.filter-section {
  flex: 1;
  background: rgba(255, 255, 255, 0.95);
  padding: 2rem 1.5rem;
  border-radius: 16px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.08);
  min-width: 220px;
  max-width: 260px;
  margin-top: 1rem;
  animation: fadeInLeft 0.7s;
}

.filter-section h3 {
  margin-bottom: 1rem;
  color: #00bcd4;
  font-weight: bold;
}

.btn-filter,
.btn-reset {
  padding: 0.5rem 1.2rem;
  border: none;
  border-radius: 4px;
  background-color: #00bcd4;
  color: white;
  cursor: pointer;
  margin-top: 1rem;
  margin-right: 0.5rem;
  font-weight: 600;
  transition: background 0.2s;
}

.btn-reset {
  background: #e0e0e0;
  color: #222;
}

.btn-filter:hover {
  background: #0097a7;
}
.btn-reset:hover {
  background: #bdbdbd;
}

.ratings-list,
.levels-list {
  list-style: none;
  padding: 0;
}

.ratings-list li,
.levels-list li {
  margin-bottom: 0.5rem;
  color: #333;
}

.results-section {
  flex: 3;
  animation: fadeInUp 0.7s;
}

.sort-section {
  display: flex;
  justify-content: flex-end;
  margin-bottom: 1.5rem;
}

.sort-button {
  padding: 0.5rem 1.2rem;
  border: 1px solid #00bcd4;
  border-radius: 4px;
  background-color: #fff;
  cursor: pointer;
  color: #00bcd4;
  font-weight: 600;
  transition: background 0.2s, color 0.2s;
}

.sort-button:hover {
  background-color: #00bcd4;
  color: #fff;
}

.results-title {
  margin-bottom: 1.5rem;
  color: #222;
  font-weight: bold;
  font-size: 1.3rem;
}

.course-card {
  display: flex;
  gap: 1.5rem;
  margin-bottom: 1.5rem;
  padding: 1.5rem;
  border: none;
  border-radius: 16px;
  background: linear-gradient(120deg, #e3f2fd 0%, #f8fafd 100%);
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.08);
  align-items: center;
  transition: transform 0.2s, box-shadow 0.2s;
  animation: fadeInUp 0.7s;
}

.course-card:hover {
  transform: translateY(-6px) scale(1.03);
  box-shadow: 0 8px 32px rgba(30, 60, 100, 0.18);
}

.course-card img {
  width: 170px;
  height: 110px;
  border-radius: 8px;
  object-fit: cover;
  box-shadow: 0 2px 8px rgba(30, 60, 100, 0.08);
}

.course-info h4 {
  margin: 0;
  font-size: 1.3rem;
  color: #222;
  font-weight: bold;
}

.course-info p {
  margin: 0.5rem 0;
  color: #666;
}

.rating {
  color: #ffc107;
}

.star {
  font-size: 1.1rem;
  transition: color 0.2s;
}

.star.filled {
  color: #ffc107;
}

@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(40px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes fadeInLeft {
  from {
    opacity: 0;
    transform: translateX(-40px);
  }
  to {
    opacity: 1;
    transform: translateX(0);
  }
}

.fade-slide-enter-active,
.fade-slide-leave-active {
  transition: opacity 0.5s, transform 0.5s;
}
.fade-slide-enter-from,
.fade-slide-leave-to {
  opacity: 0;
  transform: translateY(40px);
}

.card-fade-enter-active,
.card-fade-leave-active {
  transition: opacity 0.5s, transform 0.5s;
}
.card-fade-enter-from,
.card-fade-leave-to {
  opacity: 0;
  transform: translateY(40px);
}
</style>
