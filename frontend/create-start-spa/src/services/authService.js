import axios from "axios";

const API_URL_LOGIN = "http://localhost:5067/api/Auth/login";
const API_URL_REGISTER = "http://localhost:5067/api/Auth/register";

export default {
  async login(username, password) {
    const payload = {
      username: username,
      password: password,
    };
    const res = await axios.post(API_URL_LOGIN, payload);
    return res.data.token;
  },

  async register(username, email, password) {
    const payload = {
      username: username,
      email: email,
      password: password,
    };
    const res = await axios.post(API_URL_REGISTER, payload);
    return res.data;
  },
};
