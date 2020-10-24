import axios from 'axios';

const BASE_URL = 'http://localhost:3000';

export default {
    get: async (url) => await axios.get(`${BASE_URL}${url}`),
    post: async (url, body) => await axios.post(`${BASE_URL}${url}`)
}