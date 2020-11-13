import axios from 'axios';

const BASE_URL = process.env.REACT_APP_BASE_URL;

export default {
    get: async (url) => await axios.get(`${BASE_URL}${url}`),
    getExternal: async (url) => await axios.get(`${url}`),
    post: async (url, data) => {
        let res = await axios({
            method: 'post',
            url: `${BASE_URL}${url}`,
            data,
            headers: {"Content-Type": "application/json"}
        });
        return res.data;
    },
    postAuthorized: async (url, data, token) => {
        return await axios(`${BASE_URL}${url}`, {
            method: 'post',
            data: JSON.stringify(data),
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-type": "application/json"
            }
        })
    },
    getAuthorized: async (url, token) => await axios.get(`${BASE_URL}${url}`, {headers: {"Authorization": `Bearer ${token}`}})
}