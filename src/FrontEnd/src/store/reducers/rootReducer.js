import { combineReducers } from 'redux';
import authReducer from './authReducer';
import commonReducer from './commonReducer';
import treeReducer from './treeReducer';
import usersReducer from './usersReducer';

const rootReducer = combineReducers({
    auth: authReducer,
    common: commonReducer,
    tree: treeReducer,
    users: usersReducer
});

export default rootReducer;
