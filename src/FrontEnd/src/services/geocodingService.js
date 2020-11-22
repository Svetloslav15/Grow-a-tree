import axios from 'axios';

const ROUTES = {
    getCityByCoords: 'https://nominatim.openstreetmap.org/reverse?'
}

export default {
    getCityByCoords: async (lat, lon) => {
        return await axios({
            method: 'GET',
            url: `${ROUTES.getCityByCoords}lat=${lat}&lon=${lon}&format=json`,
            headers: {
                'Content-Type': 'application/json',
                'Accept-Language': 'bg'
            }
        });
    }
}