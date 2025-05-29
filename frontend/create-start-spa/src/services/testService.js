import axios from "axios";

const API_URL = "http://localhost:5067/api";

export default {
  async getLessonDetail(lessonId) {
    const token = localStorage.getItem("token");
    const res = await axios.get(`${API_URL}/Lessons/${lessonId}`, {
      headers: token ? { Authorization: `Bearer ${token}` } : {},
    });
    return res.data;
  },
  async getTestsByLesson(lessonId) {
    const token = localStorage.getItem("token");
    const res = await axios.get(`${API_URL}/Tests/by-lesson/${lessonId}`, {
      headers: token ? { Authorization: `Bearer ${token}` } : {},
    });
    return res.data;
  },
  async getQuestionsByTest(testId) {
    const token = localStorage.getItem("token");
    const res = await axios.get(
      `http://localhost:5067/api/Questions/by-test/${testId}`,
      {
        headers: token ? { Authorization: `Bearer ${token}` } : {},
      }
    );
    return res.data;
  },
  async saveWritingAnswer({ questionId, textAnswer }) {
    const token = localStorage.getItem("token");
    // Always get userId from localStorage to avoid stale/incorrect values
    const userId = parseInt(localStorage.getItem("userId"), 10);
    if (!userId || isNaN(userId)) {
      throw new Error("Không tìm thấy userId trong localStorage. Vui lòng đăng nhập lại.");
    }
    const payload = {
      userId,
      questionId,
      textAnswer,
      isMarked: false,
    };
    await axios.post(
      `${API_URL}/UserAnswers`,
      payload,
      { headers: token ? { Authorization: `Bearer ${token}` } : {} }
    );
  },
};
