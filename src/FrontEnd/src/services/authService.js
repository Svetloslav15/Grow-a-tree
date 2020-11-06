import baseService from './baseService';

const ROUTES = {
    signUp: '/auth/register',
    signUpStore: '/stores/upsert',
    login: '/auth/login',
    confirmEmail: '/auth/confirm-email',
    forgottenPassword: '/auth/forgotten-password',
    resendConfirmationLink: '/auth/resend-link-confirm-email',
    resetPassword: '/auth/reset-password',
    externalLogin: '/auth/external-login',
};

const api = new Proxy({}, {
        get(target, propName) {
            return async (data) => await baseService.post(ROUTES[propName], data);
        }
    });
export default api;