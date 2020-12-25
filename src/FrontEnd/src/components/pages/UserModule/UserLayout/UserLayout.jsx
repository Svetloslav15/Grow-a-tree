import React, {useEffect} from 'react';
import Sidebar from '../Sidebar/Sidebar';
import {CHANGE_IS_USER_NAV_LOCKED, CHANGE_IS_USER_NAV_OPENED} from "../../../../store/actions/actionTypes";
import {useDispatch} from 'react-redux';

const BgShape3 = require('../../../../assets/bg-shape-3.png');
const BgShape4 = require('../../../../assets/bg-shape-4.png');
const Layout = ({children}) => {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch({type: CHANGE_IS_USER_NAV_LOCKED, data: true});
        return () => {
            dispatch({type: CHANGE_IS_USER_NAV_LOCKED, data: false});
            dispatch({type: CHANGE_IS_USER_NAV_OPENED, data: false});
        }
    }, []);

    return (
        <div className='mx-0 row py-5'>
            <img src={BgShape3} className='shape3'/>
            <img src={BgShape4} className='shape4'/>
            <Sidebar/>
            {children}
        </div>
    );
}
export default Layout;