import axios from 'axios';

const BASE_URL = process.env.REACT_APP_BASE_URL;

export default {
    get: async (url) => await axios.get(`${BASE_URL}${url}`),
    post: async (url, data) => {
        let res = await axios({
            method: 'post',
            url: `${BASE_URL}${url}`,
            data,
            headers: {"Content-Type": "application/json"}
        });
        return res.data;
    },
    postAuthorized: async (url, data, token) => await axios.post(`${BASE_URL}${url}`, {
        data,
        headers: {"Authorization": `Bearer ${token}`}
    }),
    getAuthorized: async (url, token) => await axios.get(`${BASE_URL}${url}`, {headers: {"Authorization": `Bearer ${token}`}})
}