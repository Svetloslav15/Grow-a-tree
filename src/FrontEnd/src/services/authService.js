import baseService from './baseService';

const SIGN_UP_ROUTE = '/auth/register';
const CONFIRM_EMAIL_ROUTE = '/auth/confirm-email';

export default {
    signUp: async (data) => await baseService.post(SIGN_UP_ROUTE, data),
    confirmEmail: async (data) => await baseService.post(CONFIRM_EMAIL_ROUTE, data)
}