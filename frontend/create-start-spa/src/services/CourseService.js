import axios from "axios";

const API_URL = "http://localhost:5067/api/Courses";

export default {
  async getCourses() {
    const token = localStorage.getItem("token");
    const res = await axios.get(API_URL, {
      headers: token ? { Authorization: `Bearer ${token}` } : {},
    });
    return res.data;
  },
  async searchCourses(title) {
    const token = localStorage.getItem("token");
    const res = await axios.get(`${API_URL}/search`, {
      params: { title },
      headers: token ? { Authorization: `Bearer ${token}` } : {},
    });
    return res.data;
  },
  async getCourseDetail(id) {
    const token = localStorage.getItem("token");
    const res = await axios.get(`${API_URL}/${id}`, {
      headers: token ? { Authorization: `Bearer ${token}` } : {},
    });
    return res.data;
  },
  async getLessonsByCourse(courseId) {
    const token = localStorage.getItem("token");
    const res = await axios.get(
      `http://localhost:5067/api/Lessons/by-course/${courseId}`,
      {
        headers: token ? { Authorization: `Bearer ${token}` } : {},
      }
    );
    return res.data;
  },
};
