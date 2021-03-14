import {ADD_USERS} from '../actions/actionTypes';

const initialState = {
    users: []
};

const usersReducer = (state = initialState, action) => {
    switch (action.type) {
        case ADD_USERS:{
            console.log(action);
            return Object.assign({}, state, {users: action.payload});
        }
        default:
            return state;
    }
};

export default usersReducer;