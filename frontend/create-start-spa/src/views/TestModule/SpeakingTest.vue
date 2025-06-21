<template>
  <div class="attempt-wrapper" v-if="questions.length">
    <h2>Làm bài Speaking Test</h2>
    <form @submit.prevent="submit">
      <div v-for="(q, idx) in questions" :key="q.id" class="question-block">
        <div class="question-content">
          <span class="q-order">Câu {{ idx + 1 }}.</span>
          <span v-html="q.content"></span>
        </div>
        <div class="speaking-block">
          <textarea v-model="answers[q.id]" rows="4" placeholder="Ghi chú hoặc gõ ý chính (không bắt buộc)..." class="answer-input">
          </textarea>
          <div v-if="isSpeechRecognizing(q.id) && interimTranscripts[q.id]" class="interim-transcript">
            <span style="color:#43a047; font-style:italic;">{{ interimTranscripts[q.id] }}</span>
          </div>
          <button type="button" class="btn-record" @click="startRecording(q.id)">
            <span v-if="!isRecording(q.id)">🎤 Bắt đầu ghi âm</span>
            <span v-else>⏺ Đang ghi...</span>
          </button>
          <button type="button" class="btn-speech" :class="{ 'active': isSpeechRecognizing(q.id) }" @click="startSpeechToText(q.id)">
            <span v-if="!isSpeechRecognizing(q.id)">🗣️ Nhận diện giọng nói</span>
            <span v-else>🛑 Dừng nhận diện</span>
          </button>
          <button type="button" class="btn-save" @click="saveSpeakingAnswer(q.id)">Lưu câu trả lời</button>
          <audio v-if="recordings[q.id]" :src="recordings[q.id]" controls class="audio-preview"></audio>
        </div>
      </div>
      <button class="btn-submit" type="submit">Nộp bài Speaking</button>
    </form>
  </div>
  <div v-else>
    <p>Đang tải câu hỏi...</p>
  </div>
</template>

<script>
import testService from "@/services/testService";

export default {
  name: "SpeakingTest",
  props: ["testId"],
  data() {
    return {
      questions: [],
      answers: {},
      recordings: {}, // { questionId: audioUrl }
      recordingId: null, // questionId đang ghi âm
      mediaRecorder: null,
      audioChunks: [],
      speechRecognizing: {}, // { [qid]: true/false }
      speechRecognitionInstances: {}, // { [qid]: recognition instance }
      interimTranscripts: {}, // { [qid]: interim transcript }
    };
  },
  async mounted() {
    this.questions = await testService.getQuestionsByTest(this.testId || this.$route.params.testId);
  },
  methods: {
    isRecording(qid) {
      return this.recordingId === qid;
    },
    async startRecording(qid) {
      if (this.isRecording(qid)) {
        // Đang ghi thì dừng lại
        this.mediaRecorder.stop();
        return;
      }
      // Nếu đang ghi câu khác thì dừng lại
      if (this.mediaRecorder && this.mediaRecorder.state === 'recording') {
        this.mediaRecorder.stop();
      }
      this.recordingId = qid;
      this.audioChunks = [];
      try {
        const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
        this.mediaRecorder = new window.MediaRecorder(stream);
        this.mediaRecorder.ondataavailable = e => this.audioChunks.push(e.data);
        this.mediaRecorder.onstop = () => {
          const blob = new Blob(this.audioChunks, { type: 'audio/webm' });
          this.recordings[qid] = URL.createObjectURL(blob);
          this.recordingId = null;
        };
        this.mediaRecorder.start();
      } catch (e) {
        alert('Không thể truy cập micro!');
        this.recordingId = null;
      }
    },
    async submit() {
      // Lưu đáp án speaking vào localStorage (không gửi API)
      const userAnswers = JSON.parse(localStorage.getItem('userAnswers') || '[]');
      for (const q of this.questions) {
        if (this.recordings[q.id]) {
          // Xoá đáp án speaking cũ nếu có
          const idx = userAnswers.findIndex(a => a.questionId === q.id && a.questionType === 'speaking');
          if (idx !== -1) userAnswers.splice(idx, 1);
          userAnswers.push({
            questionId: q.id,
            answer: this.recordings[q.id],
            questionType: 'speaking'
          });
        }
      }
      localStorage.setItem('userAnswers', JSON.stringify(userAnswers));
      this.$router.push({ name: "AfterTest" });
    },
    getUserId() {
      const user = JSON.parse(localStorage.getItem("userID") || "null");
      return user?.userId || 0;
    },
    isSpeechRecognizing(qid) {
      return !!this.speechRecognizing[qid];
    },
    startSpeechToText(qid) {
      if (!('webkitSpeechRecognition' in window)) {
        alert('Trình duyệt không hỗ trợ Speech Recognition!');
        return;
      }
      // Toggle: nếu đang nhận diện thì dừng lại
      if (this.speechRecognizing[qid]) {
        if (this.speechRecognitionInstances[qid]) {
          this.speechRecognitionInstances[qid].stop();
        }
        this.speechRecognizing[qid] = false;
        this.interimTranscripts[qid] = '';
        return;
      }
      // Nếu đang nhận diện câu khác thì dừng lại
      Object.keys(this.speechRecognitionInstances).forEach(id => {
        if (this.speechRecognitionInstances[id]) {
          this.speechRecognitionInstances[id].stop();
          this.speechRecognizing[id] = false;
          this.interimTranscripts[id] = '';
        }
      });
      const recognition = new window.webkitSpeechRecognition();
      recognition.lang = 'en-US'; // hoặc 'en-US' nếu là tiếng Anh
      recognition.interimResults = true;
      recognition.maxAlternatives = 1;
      this.speechRecognizing[qid] = true;
      this.speechRecognitionInstances[qid] = recognition;
      this.interimTranscripts[qid] = '';
      recognition.onresult = (event) => {
        let interim = '';
        let final = '';
        for (let i = event.resultIndex; i < event.results.length; ++i) {
          if (event.results[i].isFinal) {
            final += event.results[i][0].transcript;
          } else {
            interim += event.results[i][0].transcript;
          }
        }
        // Hiển thị transcript realtime
        this.interimTranscripts[qid] = interim;
        if (final) {
          this.answers[qid] = (this.answers[qid] || '') + final;
          this.interimTranscripts[qid] = '';
        }
      };
      recognition.onerror = (event) => {
        this.speechRecognizing[qid] = false;
        this.interimTranscripts[qid] = '';
        console.error('Speech error:', event.error, event);
        alert('Lỗi nhận diện giọng nói: ' + event.error);
      };
      recognition.onend = () => {
        if (this.speechRecognizing[qid]) {
          // Tự động restart để nhận diện liên tục
          recognition.start();
        } else {
          this.speechRecognitionInstances[qid] = null;
          this.interimTranscripts[qid] = '';
        }
      };
      recognition.start();
    },
    async saveSpeakingAnswer(questionId) {
      // Lưu đáp án speaking (text) vào localStorage
      const userAnswers = JSON.parse(localStorage.getItem('userAnswers') || '[]');
      // Xoá đáp án speaking cũ nếu có
      const idx = userAnswers.findIndex(a => a.questionId === questionId && a.questionType === 'speaking');
      if (idx !== -1) userAnswers.splice(idx, 1);
      const text = this.answers[questionId] || '';
      if (text.trim()) {
        userAnswers.push({
          questionId: questionId,
          answer: text,
          questionType: 'speaking'
        });
      }
      localStorage.setItem('userAnswers', JSON.stringify(userAnswers));
      console.log('Saved userAnswers:', userAnswers);
      if (this.$toast && this.$toast.success) {
        this.$toast.success("Đã lưu câu trả lời!", { timeout: 1800, position: "top-right" });
      } else {
        window.alert("Đã lưu câu trả lời!");
      }
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
.speaking-block {
  margin-top: 1rem;
  display: flex;
  flex-direction: column;
  gap: 0.7rem;
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
.btn-record {
  background: #ff9800;
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
.btn-record:hover {
  background: #f57c00;
}
.btn-speech {
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
.btn-speech:hover {
  background: #43a047;
}
.btn-save {
  background: #2196f3;
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
  background: #1976d2;
}
.audio-preview {
  margin-top: 0.5rem;
  width: 100%;
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
.transcript-preview {
  margin-top: 0.5rem;
  font-style: italic;
  color: #666;
}
.interim-transcript {
  margin-top: -0.5rem;
  margin-bottom: 0.5rem;
  font-style: italic;
  color: #43a047;
}
</style>
