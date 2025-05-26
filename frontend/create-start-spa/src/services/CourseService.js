import axios from "axios";

const API_URL = "http://localhost:5067/api/Exams";

export default {
  async getCourses() {
    const token = localStorage.getItem("token");
    const res = await axios.get(API_URL, {
      headers: token ? { Authorization: `Bearer ${token}` } : {},
    });
    return res.data;
  },
  async searchCourses(keyword) {
    const token = localStorage.getItem("token");
    const res = await axios.get(`${API_URL}/search`, {
      params: { keyword },
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
};
