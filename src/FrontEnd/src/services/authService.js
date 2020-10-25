import baseService from './baseService';

const SIGN_UP_ROUTE = '/auth/register';
const CONFIRM_EMAIL_ROUTE = '/auth/confirm-email';
const FORGOTTEN_PASSWORD_ROUTE = '/auth/forgotten-password';

export default {
    signUp: async (data) => await baseService.post(SIGN_UP_ROUTE, data),
    confirmEmail: async (data) => await baseService.post(CONFIRM_EMAIL_ROUTE, data),
    forgottenPassword: async (data) => await baseService.post(FORGOTTEN_PASSWORD_ROUTE, data)
}