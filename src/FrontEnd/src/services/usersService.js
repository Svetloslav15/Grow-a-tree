import baseService from './baseService';

const ROUTES = {
    getAuthorizedUserById: '/users/',
    postAuthorizedEditUser: '/users/edit',
    getFacebookProfilePicture: 'https://graph.facebook.com',
    postAuthorizedChangeProfilePicture: '/users/change-profile-picture',
    getAuthorizedUsers: '/users/',
    postAuthorizedToggleLockoutUser: '/users/toggle-lockout',
    postAuthorizedToggleAdminRights: '/users/toggle-admin'
};

export default new Proxy({}, {
    get(target, propName) {
        if (propName.startsWith('postAuthorized')) {
            return async (data, token, contentType) => await baseService.postAuthorized(ROUTES[propName], data, token, contentType);
        }
        else if (propName.startsWith('post')) {
            return async (data) => await baseService.post(ROUTES[propName], data);
        }
        else if (propName.startsWith('getAuthorized')) {
            return async (urlParams, token) => await baseService.getAuthorized(`${ROUTES[propName]}${urlParams}`, token);
        }
        return async (urlParams) => await baseService.get(`${ROUTES[propName]}${urlParams}`);
    }
});