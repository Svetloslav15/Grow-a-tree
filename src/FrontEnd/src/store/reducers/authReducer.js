import {SAVE_CURRENT_USER} from '../actions/actionTypes';
import Cookies from 'js-cookie';

let currUser = Cookies.get('gt_curr_user') || {
    accessToken: '',
    expired: '',
    id: '',
    isStore: false,
    refreshToken: '',
    username: ''
};

const initialState = {
    accessToken: currUser.accessToken,
    expired: currUser.expired,
    id: currUser.id,
    isStore: false,
    refreshToken: currUser.refreshToken,
    username: currUser.username
};

const authReducer = (state = initialState, action) => {
    switch (action.type) {
        case SAVE_CURRENT_USER:
            return Object.assign({}, state, {...action.data});
        default:
            return state;
    }
};

export default authReducer;