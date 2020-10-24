import baseService from './baseService';

const SIGN_UP_ROUTE = '/auth/register';

export default {
    signUp: async (data) => await baseService.post(SIGN_UP_ROUTE, data)
}