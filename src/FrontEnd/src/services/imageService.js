import baseService from './baseService';

const ROUTES = {
    addImage: '/images/upload-image'
};

export default new Proxy({}, {
    get(target, propName) {
        return async (data, token, contentType) => await baseService.postAuthorized(ROUTES[propName], data, token, contentType);
    }
});