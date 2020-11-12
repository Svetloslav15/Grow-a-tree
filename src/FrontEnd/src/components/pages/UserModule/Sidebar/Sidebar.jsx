import React from 'react';
import Item from '../../../common/UserNavigation/Item/Item';
import {useDispatch} from 'react-redux';
import Cookies from 'js-cookie';

import * as style from './Sidebar.module.scss';
import {SAVE_CURRENT_USER} from '../../../../store/actions/actionTypes';
import CookieNames from '../../../../static/cookieNames';

const UserImage = require('../../../../assets/user-profile.png');

const Sidebar = ({isOpen, closeNavigation, isFixed}) => {
    const dispatch = useDispatch({});

    const logoutUser = () => {
        const data = {
            accessToken: '',
            expired: '',
            id: '',
            isStore: false,
            refreshToken: '',
            username: ''
        };
        Cookies.remove(CookieNames.currentUser);
        dispatch({ type:  SAVE_CURRENT_USER, data});
    };

    return (
        <div className={`${style.wrapper} col-md-3`}>
            <div className={`mx-0 row ${style.userSection}`}>
                <img className={style.userProfileImage} src={UserImage} alt=""/>
                <div className=''>
                    <p className={style.username}>@Svetloslav</p>
                    <p>(Svetloslav Novoselski)</p>
                </div>
            </div>
            <div>
                <Item link='/users/my-info' text='Моят профил' icon='user'/>
                <Item link='/users/ref' text='QR код' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='#' onClick={logoutUser} text='Изход' icon='door-open'/>
            </div>
        </div>
    );
};

export default Sidebar;