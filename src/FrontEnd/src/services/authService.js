import baseService from './baseService';

const SIGN_UP_ROUTE = '/auth/register';
const SIGN_UP_STORE_ROUTE = '/auth/register/store';
const LOGIN_ROUTE = '/auth/login';
const EXTERNAL_LOGIN_ROUTE = '/auth/external-login';
const CONFIRM_EMAIL_ROUTE = '/auth/confirm-email';
const FORGOTTEN_PASSWORD_ROUTE = '/auth/forgotten-password';
const RESEND_CONFIRMATION_LINK_ROUTE = '/auth/resend-link-confirm-email';
const RESET_PASSWORD_ROUTE = '/auth/reset-password';

export default {
    signUp: async (data) => await baseService.post(SIGN_UP_ROUTE, data),
    signUpStore: async (data) => await baseService.post(SIGN_UP_STORE_ROUTE, data),
    login: async (data) => await baseService.post(LOGIN_ROUTE, data),
    confirmEmail: async (data) => await baseService.post(CONFIRM_EMAIL_ROUTE, data),
    forgottenPassword: async (data) => await baseService.post(FORGOTTEN_PASSWORD_ROUTE, data),
    resendConfirmationLink: async (data) => await baseService.post(RESEND_CONFIRMATION_LINK_ROUTE, data),
    resetPassword: async (data) => await baseService.post(RESET_PASSWORD_ROUTE, data),
    externalLogin: async (data) => await baseService.post(EXTERNAL_LOGIN_ROUTE, data)
}