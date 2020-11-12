import {SAVE_CURRENT_USER} from '../actions/actionTypes';
import Cookies from "js-cookie";
import CookieNames from "../../static/cookieNames";

const emptyUser = {
    accessToken: '',
    expired: '',
    id: '',
    username: '',
    isStore: false,
    refreshToken: ''
};

const initialState = {
    accessToken: emptyUser.accessToken,
    expired: emptyUser.expired,
    id: emptyUser.id,
    isStore: false,
    refreshToken: emptyUser.refreshToken,
    username: emptyUser.username
};

const authReducer = (state = initialState, action) => {
    state = Cookies.get(CookieNames.currentUser) ? JSON.parse(Cookies.get(CookieNames.currentUser)) : emptyUser;

    switch (action.type) {
        case SAVE_CURRENT_USER:
            return Object.assign({}, state, {...action.data});
        default:
            return state;
    }
};

export default authReducer;