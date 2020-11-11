import {CHANGE_IS_USER_NAV_FIXED} from '../actions/actionTypes';

const initialState = {
    isUserNavFixed: false
};

const commonReducer = (state = initialState, action) => {
    switch (action.type) {
        case CHANGE_IS_USER_NAV_FIXED:
            return Object.assign({}, state, {isUserNavFixed: action.data});
        default:
            return state;
    }
};

export default commonReducer;