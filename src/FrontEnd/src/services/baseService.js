import axios from 'axios';
import ContentTypes from '../static/contentTypes';

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
    postAuthorized: async (url, data, token, contentType = ContentTypes.ApplicationJson) => {
        data = contentType === ContentTypes.ApplicationJson ? JSON.stringify(data) : data;
        const response = await axios(`${BASE_URL}${url}`, {
            method: 'post',
            data,
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-type": {contentType}
            }
        });

        return response.data;
    },
    getAuthorized: async (url, token) => await axios.get(`${BASE_URL}${url}`, {headers: {"Authorization": `Bearer ${token}`}})
}