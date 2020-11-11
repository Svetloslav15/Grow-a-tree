import {CHANGE_IS_USER_NAV_FIXED, CHANGE_IS_USER_NAV_OPENED} from '../actions/actionTypes';

const initialState = {
    isUserNavFixed: false,
    isUserNavOpen: false
};

const commonReducer = (state = initialState, action) => {
    switch (action.type) {
        case CHANGE_IS_USER_NAV_FIXED:
            return Object.assign({}, state, {isUserNavFixed: action.data});
        case CHANGE_IS_USER_NAV_OPENED:
            return Object.assign({}, state, {isUserNavOpen: action.data});
        default:
            return state;
    }
};

export default commonReducer;