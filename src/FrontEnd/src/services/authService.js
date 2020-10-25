import baseService from './baseService';

const SIGN_UP_ROUTE = '/auth/register';
const CONFIRM_EMAIL_ROUTE = '/auth/confirm-email';
const FORGOTTEN_PASSWORD_ROUTE = '/auth/forgotten-password';
const RESEND_CONFIRMATION_LINK_ROUTE = '/auth/resend-link-confirm-email';

export default {
    signUp: async (data) => await baseService.post(SIGN_UP_ROUTE, data),
    confirmEmail: async (data) => await baseService.post(CONFIRM_EMAIL_ROUTE, data),
    forgottenPassword: async (data) => await baseService.post(FORGOTTEN_PASSWORD_ROUTE, data),
    resendConfirmationLink: async (data) => await baseService.post(RESEND_CONFIRMATION_LINK_ROUTE, data)
}