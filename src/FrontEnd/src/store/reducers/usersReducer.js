import {ADD_USERS} from '../actions/actionTypes';

const initialState = {
    users: []
};

const usersReducer = (state = initialState, action) => {
    switch (action) {
        case ADD_USERS:
            return Object.assign({}, state, {users: action.payload});
        default:
            return state;
    }
};

export default usersReducer();