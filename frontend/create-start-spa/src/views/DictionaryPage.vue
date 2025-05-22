<template>
  <div class="dictionary-hero-bg">
    <div class="dictionary-hero-overlay"></div>
    <div class="dictionary-content">
      <div class="dictionary-search-box">
        <h2 class="dictionary-title">Dictionary</h2>
        <div class="dictionary-search-bar">
          <input
            class="search-bar"
            type="text"
            v-model="searchWord"
            placeholder="Enter a word..."
            @keyup.enter="fetchWord"
          />
          <button class="btn btn-primary" @click="fetchWord">Search</button>
        </div>
        <div v-if="error" class="error-message">
          <p>{{ error }}</p>
        </div>
      </div>

      <transition name="fade-slide">
        <div v-if="wordData" class="notebook">
          <h3>{{ wordData.word }}</h3>
          <div v-for="(meaning, index) in wordData.meanings" :key="index">
            <h4>{{ meaning.partOfSpeech }}</h4>
            <ul>
              <li
                v-for="(definition, defIndex) in meaning.definitions"
                :key="defIndex"
              >
                {{ definition.definition }}
              </li>
            </ul>
          </div>
        </div>
      </transition>

      <div class="suggestion-section">
        <h3 class="suggestion-title">Today's Suggestions</h3>
        <div class="suggestion-list">
          <div
            class="suggestion-card"
            v-for="(word, idx) in suggestedWords"
            :key="idx"
            @click="searchSuggestion(word)"
          >
            <span>{{ word }}</span>
          </div>
        </div>
      </div>
    </div>
    <img
      class="dictionary-bg-img"
      src="@/assets/classroom.jpg"
      alt="Background"
    />
  </div>
</template>

<script>
export default {
  name: "DictionaryPage",
  data() {
    return {
      searchWord: "",
      wordData: null,
      error: null,
      suggestedWords: [],
    };
  },
  mounted() {
    this.fetchSuggestions();
  },
  methods: {
    async fetchWord() {
      if (!this.searchWord.trim()) {
        this.error = "Please enter a word to search.";
        this.wordData = null;
        return;
      }
      try {
        const response = await fetch(
          `https://api.dictionaryapi.dev/api/v2/entries/en/${this.searchWord}`
        );
        if (!response.ok) {
          throw new Error("Word not found. Please try another word.");
        }
        const data = await response.json();
        this.wordData = data[0];
        this.error = null;
      } catch (err) {
        this.error = err.message;
        this.wordData = null;
      }
    },
    async fetchSuggestions() {
      try {
        const res = await fetch(
          "https://random-word-api.herokuapp.com/word?number=6"
        );
        const data = await res.json();
        this.suggestedWords = data;
      } catch {
        this.suggestedWords = [
          "example",
          "study",
          "learn",
          "focus",
          "dream",
          "success",
        ];
      }
    },
    searchSuggestion(word) {
      this.searchWord = word;
      this.fetchWord();
    },
  },
};
</script>

<style scoped>
.dictionary-hero-bg {
  min-height: 100vh;
  background: linear-gradient(120deg, #232526 0%, #5e97bb 100%);
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
}

.dictionary-bg-img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  object-fit: cover;
  z-index: 1;
  opacity: 0.25;
}

.dictionary-hero-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(30, 60, 100, 0.55);
  z-index: 2;
}

.dictionary-content {
  position: relative;
  z-index: 3;
  width: 100%;
  max-width: 700px;
  margin: 0 auto;
  padding: 3rem 2rem 2rem 2rem;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.dictionary-title {
  color: #00bcd4;
  font-weight: bold;
  font-size: 2.2rem;
  margin-bottom: 1.5rem;
  text-shadow: 0 2px 8px rgba(0, 0, 0, 0.12);
}

.dictionary-search-box {
  width: 100%;
  background: rgba(255, 255, 255, 0.95);
  border-radius: 16px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.08);
  padding: 2rem 1.5rem 1.5rem 1.5rem;
  margin-bottom: 2rem;
  text-align: center;
  animation: fadeInUp 0.7s;
}

.dictionary-search-bar {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 0.5rem;
  margin-bottom: 1rem;
}

.search-bar {
  width: 60%;
  padding: 0.75rem;
  font-size: 1.1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  background: #f8fafd;
  transition: box-shadow 0.2s;
}

.search-bar:focus {
  outline: none;
  box-shadow: 0 0 0 2px #00bcd4;
}

.btn {
  padding: 0.75rem 1.5rem;
  font-size: 1rem;
  background-color: #00bcd4;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  width: auto;
  font-weight: 600;
  transition: background 0.2s;
}

.btn:hover {
  background-color: #0097a7;
}

.error-message {
  color: #e53935;
  text-align: center;
  margin-bottom: 1rem;
  font-weight: bold;
}

.notebook {
  background: #fff;
  border: 1px solid #ccc;
  border-radius: 16px;
  padding: 2rem 1.5rem;
  box-shadow: 0 4px 16px rgba(30, 60, 100, 0.08);
  font-family: "Courier New", Courier, monospace;
  line-height: 1.6;
  max-width: 700px;
  margin: 0 auto 2rem auto;
  animation: fadeInUp 0.7s;
}

.notebook h3 {
  text-align: center;
  margin-bottom: 1rem;
  color: #00bcd4;
}

.notebook h4 {
  margin-top: 1rem;
  color: #007bff;
}

.notebook ul {
  list-style-type: disc;
  margin-left: 1.5rem;
}

.suggestion-section {
  width: 100%;
  margin-top: 2rem;
  background: rgba(255, 255, 255, 0.95);
  border-radius: 16px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.08);
  padding: 1.5rem 1rem 1.5rem 1rem;
  animation: fadeInUp 0.7s;
}

.suggestion-title {
  color: #00bcd4;
  font-size: 1.2rem;
  font-weight: bold;
  margin-bottom: 1rem;
  text-align: center;
}

.suggestion-list {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  justify-content: center;
}

.suggestion-card {
  background: linear-gradient(120deg, #e3f2fd 0%, #f8fafd 100%);
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(30, 60, 100, 0.08);
  padding: 0.8rem 1.5rem;
  font-size: 1.1rem;
  color: #222;
  cursor: pointer;
  font-weight: 600;
  transition: background 0.2s, color 0.2s, transform 0.2s;
  border: 1px solid #00bcd4;
}

.suggestion-card:hover {
  background: #00bcd4;
  color: #fff;
  transform: translateY(-4px) scale(1.05);
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

.fade-slide-enter-active,
.fade-slide-leave-active {
  transition: opacity 0.5s, transform 0.5s;
}
.fade-slide-enter-from,
.fade-slide-leave-to {
  opacity: 0;
  transform: translateY(40px);
}
</style>
