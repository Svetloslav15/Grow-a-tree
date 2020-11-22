import baseService from './baseService';

const ROUTES = {
    signUp: '/auth/register',
    signUpStore: '/stores/upsert',
    login: '/auth/login',
    confirmEmail: '/auth/confirm-email',
    forgottenPassword: '/auth/forgotten-password',
    resendConfirmationLink: '/auth/resend-link-confirm-email',
    postAuthorizedResetPassword: '/auth/reset-password',
    externalLogin: '/auth/external-login',
    getNewAccessToken: '/auth/refresh-token'
};

export default new Proxy({}, {
    get(target, propName) {
        if (propName.startsWith('postAuthorized')) {
            return async (data, token, contentType) => await baseService.postAuthorized(ROUTES[propName], data, token, contentType);
        }
        return async (data) => await baseService.post(ROUTES[propName], data);
    }
});