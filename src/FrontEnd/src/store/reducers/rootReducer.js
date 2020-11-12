import { combineReducers } from 'redux';
import authReducer from './authReducer';
import commonReducer from './commonReducer';

const rootReducer = combineReducers({
    auth: authReducer,
    common: commonReducer
});

export default rootReducer;
