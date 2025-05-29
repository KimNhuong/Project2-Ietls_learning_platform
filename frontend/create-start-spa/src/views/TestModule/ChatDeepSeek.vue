<template>
  <div class="chatbox-wrapper">
    <h2>Chat với DeepSeek</h2>
    <div class="chat-window">
      <div v-for="(msg, idx) in messages" :key="idx" :class="['chat-msg', msg.role]">
        <span class="msg-role">{{ msg.role === 'user' ? 'Bạn' : 'DeepSeek' }}:</span>
        <span class="msg-content">{{ msg.content }}</span>
      </div>
    </div>
    <form class="chat-input-row" @submit.prevent="sendMessage">
      <textarea v-model="input" placeholder="Nhập câu hỏi..." rows="3" class="chat-input"></textarea>
      <button class="btn-send" :disabled="loading || !input.trim()">Gửi</button>
    </form>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "ChatDeepSeek",
  data() {
    return {
      input: "",
      messages: [],
      loading: false,
    };
  },
  methods: {
    async sendMessage() {
      const prompt = (this.input || "").trim();
      if (!prompt) {
        this.$toast?.error?.("Bạn cần nhập nội dung câu hỏi!") || window.alert("Bạn cần nhập nội dung câu hỏi!");
        return;
      }
      const userMsg = { role: "user", content: prompt };
      this.messages.push(userMsg);
      this.loading = true;
      try {
        // Gửi raw string thay vì object
        const res = await axios.post(
          "http://localhost:5067/api/DeepSeekRequests/prompt",
          JSON.stringify(prompt),
          { headers: { "Content-Type": "application/json" } }
        );
        this.messages.push({ role: "deepseek", content: res.data?.response || res.data || "(Không có phản hồi)" });
      } catch (e) {
        this.messages.push({ role: "deepseek", content: "Lỗi gửi yêu cầu!" });
      }
      this.input = "";
      this.loading = false;
    },
  },
};
</script>

<style scoped>
.chatbox-wrapper {
  max-width: 700px;
  margin: 2rem auto;
  background: #fff;
  border-radius: 18px;
  box-shadow: 0 4px 24px rgba(30, 60, 100, 0.12);
  padding: 2rem;
  min-height: 70vh;
  display: flex;
  flex-direction: column;
}
.chat-window {
  flex: 1;
  overflow-y: auto;
  margin-bottom: 1.5rem;
  background: #f7fafd;
  border-radius: 10px;
  padding: 1rem;
  min-height: 350px;
}
.chat-msg {
  margin-bottom: 1.1rem;
  display: flex;
  align-items: flex-start;
}
.chat-msg.user .msg-role {
  color: #00bcd4;
  font-weight: bold;
  margin-right: 0.5rem;
}
.chat-msg.deepseek .msg-role {
  color: #4caf50;
  font-weight: bold;
  margin-right: 0.5rem;
}
.msg-content {
  background: #e3f2fd;
  border-radius: 8px;
  padding: 0.5rem 1rem;
  max-width: 80%;
  word-break: break-word;
}
.chat-msg.deepseek .msg-content {
  background: #e8f5e9;
}
.chat-input-row {
  display: flex;
  gap: 1rem;
}
.chat-input {
  flex: 1;
  border: 1.5px solid #bdbdbd;
  border-radius: 6px;
  padding: 0.7rem;
  font-size: 1.1rem;
  resize: none;
}
.btn-send {
  background: #00bcd4;
  color: #fff;
  border: none;
  border-radius: 8px;
  padding: 0.9rem 2.2rem;
  font-size: 1.1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.2s;
}
.btn-send:disabled {
  background: #bdbdbd;
  cursor: not-allowed;
}
</style>
