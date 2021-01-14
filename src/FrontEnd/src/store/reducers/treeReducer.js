import {ADD_TREE_POSTS} from '../actions/actionTypes';

const initialState = {
    posts: [],
};

const treeReducer = (state = initialState, action) => {
    switch (action.type) {
        case ADD_TREE_POSTS:
            return Object.assign({}, state, {posts: action.data});
        default:
            return state;
    }
};

export default treeReducer;