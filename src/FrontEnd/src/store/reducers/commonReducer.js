import {CHANGE_IS_USER_NAV_LOCKED, CHANGE_IS_USER_NAV_OPENED} from '../actions/actionTypes';

const initialState = {
    isUserNavLocked: false,
    isUserNavOpen: false
};

const commonReducer = (state = initialState, action) => {
    switch (action.type) {
        case CHANGE_IS_USER_NAV_LOCKED:
            return Object.assign({}, state, {isUserNavLocked: action.data});
        case CHANGE_IS_USER_NAV_OPENED:
            return Object.assign({}, state, {isUserNavOpen: action.data});
        default:
            return state;
    }
};

export default commonReducer;