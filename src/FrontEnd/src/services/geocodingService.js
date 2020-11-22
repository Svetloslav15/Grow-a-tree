import baseService from './baseService';

const ROUTES = {
    getCityByCoords: 'https://nominatim.openstreetmap.org/reverse?'
}

export default {
    getCityByCoords: async (lat, lon) => await baseService.get(`${ROUTES.getCityByCoords}lat=${lat}&lon=${lon}&format=json`)
}