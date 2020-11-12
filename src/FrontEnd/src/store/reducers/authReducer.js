import {SAVE_CURRENT_USER} from '../actions/actionTypes';

const initialState = {
    accessToken: '',
    expired: '',
    id: '',
    isStore: false,
    refreshToken: '',
    username: ''
};

const authReducer = (state = initialState, action) => {
    localStorage.setItem()
    switch (action.type) {
        case SAVE_CURRENT_USER:
            return Object.assign({}, state, {...action.data});
        default:
            return state;
    }
};

export default authReducer;