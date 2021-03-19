import {SAVE_CURRENT_USER} from '../actions/actionTypes';
import Cookies from 'js-cookie';
import CookieNames from '../../static/cookieNames';
import AuthService from '../../services/authService';

const emptyUser = {
    accessToken: '',
    expires: '',
    id: '',
    username: '',
    isStore: false,
    refreshToken: '',
    isAdmin: false
};

let initialState = {
    accessToken: emptyUser.accessToken,
    expires: emptyUser.expires,
    id: emptyUser.id,
    isStore: false,
    refreshToken: emptyUser.refreshToken,
    username: emptyUser.username
};

const authReducer = (state = initialState, action) => {
    state = Cookies.get(CookieNames.currentUser) ? JSON.parse(Cookies.get(CookieNames.currentUser)) : emptyUser;
    if (new Date(new Date().toISOString()).getTime() >= new Date(state.expires).getTime()) {
        AuthService.getNewAccessToken({
            accessToken: state.accessToken,
            refreshToken: state.refreshToken
        }).then((response) => {
            state = {...response.data};
            if (response.succeeded) {
                Cookies.set(CookieNames.currentUser, response.data);
            }
            else {
                Cookies.remove(CookieNames.currentUser);
                state = emptyUser;
            }
        });
    }
    switch (action.type) {
        case SAVE_CURRENT_USER:
            return Object.assign({}, state, {...action.data});
        default:
            return state;
    }
};

export default authReducer;