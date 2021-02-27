import { combineReducers } from 'redux';
import authReducer from './authReducer';
import commonReducer from './commonReducer';
import treeReducer from './treeReducer';

const rootReducer = combineReducers({
    auth: authReducer,
    common: commonReducer,
    tree: treeReducer
});

export default rootReducer;
