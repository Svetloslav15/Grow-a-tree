import axios from 'axios';

const ROUTES = {
    getCityByCoords: 'https://nominatim.openstreetmap.org/reverse?'
}

export default {
    getCityByCoords: async (lat, lon) => await axios.get(`${ROUTES.getCityByCoords}lat=${lat}&lon=${lon}&format=json`)
}