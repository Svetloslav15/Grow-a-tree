import baseService from './baseService';

const ROUTES = {
    getUserById: '/users/',
    editUser: '/users/edit',
    getFacebookProfilePicture: 'https://graph.facebook.com'
};
export default {
    get: new Proxy({}, {
        get(target, propName) {
            return async (urlParams) => await baseService.get(`${ROUTES[propName]}${urlParams}`);
        }
    }),
    post: new Proxy({}, {
        get(target, propName) {
            return async (data) => await baseService.post(ROUTES[propName], data);
        }
    }),
    getAuthorized: new Proxy({}, {
        get(target, propName) {
            return async (urlParams, token) => await baseService.getAuthorized(`${ROUTES[propName]}${urlParams}`, token);
        }
    }),
    postAuthorized: new Proxy({}, {
        get(target, propName) {
            return async (data, token) => {
                return await baseService.postAuthorized(ROUTES[propName], data, token, "application/json");
            }
        }
    }),
}