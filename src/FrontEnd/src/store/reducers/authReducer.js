import {SAVE_CURRENT_USER} from '../actions/actionTypes';

const initialState = {
    accessToken: '',
    expired: '',
    id: '',
    isStore: false,
    refreshToken: ''
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