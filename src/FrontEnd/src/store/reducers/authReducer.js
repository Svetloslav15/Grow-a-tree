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
    refreshToken: ''
};

const initialState = {
    accessToken: emptyUser.accessToken,
    expires: emptyUser.expires,
    id: emptyUser.id,
    isStore: false,
    refreshToken: emptyUser.refreshToken,
    username: emptyUser.username
};
let isFetching = false;

const authReducer = (state = initialState, action) => {
    state = Cookies.get(CookieNames.currentUser) ? JSON.parse(Cookies.get(CookieNames.currentUser)) : emptyUser;
    if (new Date().getTime() >= new Date(state.expires).getTime() && !isFetching) {
        isFetching = true;
        AuthService.getNewAccessToken({
            accessToken: state.accessToken,
            refreshToken: state.refreshToken
        }).then((data) => {
            console.log(data);
            //TODO check if it saves data correctly
            state = {...data};
            console.log(data);
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