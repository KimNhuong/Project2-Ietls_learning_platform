<template>
  <div class="flashcard-page-container">
    <div class="flashcard-page">
      <!-- Header Section -->
      <h1>Flashcards</h1>
      <div class="tabs">
        <button class="tab active">My word list</button>
        <button class="tab">Studying</button>
      </div>

      <!-- Studying Section -->
      <div class="studying-section">
        <p>
          <strong>Studying:</strong> You haven't learned any word lists yet.
          <a href="#">Explore</a> now or start creating new word lists.
        </p>
      </div>

      <!-- Word List Section -->
      <div class="word-list">
        <div class="word-card create-card">
          <span>+</span>
          <p>Create word list</p>
        </div>
        <div class="word-card">
          <p>Office English Vocabulary</p>
          <p>500 Words</p>
        </div>
        <div class="word-card">
          <p>Office English Vocabulary</p>
          <p>500 Words</p>
        </div>
        <div class="word-card">
          <p>Office English Vocabulary</p>
          <p>500 Words</p>
        </div>
        <div class="word-card">
          <p>Office English Vocabulary</p>
          <p>500 Words</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "FlashCardPage",
};
</script>

<style scoped>
.flashcard-page {
  min-height: 100vh; /* Đảm bảo chiều cao tối thiểu của trang là 100% chiều cao màn hình */
  flex: 1; /* Đẩy footer xuống dưới bằng cách chiếm không gian còn lại */
  padding: 2rem;
  background-color: #f8f9fa;
  color: black;
}

h1 {
  font-size: 2.5rem;
  font-weight: bold;
  margin-bottom: 1rem;
}

.tabs {
  display: flex;
  gap: 1rem;
  margin-bottom: 1.5rem;
}

.tab {
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 4px;
  background-color: #333;
  color: white;
  cursor: pointer;
}

.tab.active {
  background-color: #6a0dad;
}

.studying-section {
  margin-bottom: 2rem;
}

.studying-section a {
  color: #007bff;
  text-decoration: none;
}

.studying-section a:hover {
  text-decoration: underline;
}

.word-list {
  display: flex;
  gap: 1rem;
  flex-wrap: wrap;
}

.word-card {
  background-color: black;
  color: white;
  border-radius: 8px;
  padding: 1rem;
  text-align: center;
  width: 200px;
  height: 150px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.word-card p {
  margin: 0.5rem 0;
}

.create-card {
  background-color: #333;
  font-size: 2rem;
  font-weight: bold;
  cursor: pointer;
  text-align: center;
}

.create-card span {
  font-size: 3rem;
  color: white;
}

.footer {
  text-align: center;
  padding: 1rem;
  background-color: #333;
  color: white;
  position: relative;
  bottom: 0;
  width: 100%;
}
</style>
