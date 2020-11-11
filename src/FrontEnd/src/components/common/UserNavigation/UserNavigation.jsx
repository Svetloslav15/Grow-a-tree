import React from 'react';
import Item from './Item/Item';
import {useDispatch, useSelector} from 'react-redux';

import * as style from './UserNavigation.module.scss';
import {SAVE_CURRENT_USER} from '../../../store/actions/actionTypes';
const UserImage = require('../../../assets/user-profile.png');
const UpArrow = require('../../../assets/up-arrow.png');

const UserNavigation = ({isOpen, closeNavigation, isFixed}) => {
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
        dispatch({ type:  SAVE_CURRENT_USER, data});
    };
    return (
        <div className={`${style.wrapper} ${isOpen ? '' : 'd-none'}  ${isFixed ? style.isFixed : ''} `}>
            <div className={`mx-0 row ${style.userSection}`}>
                <img className={style.userProfileImage} src={UserImage} alt=""/>
                <div className=''>
                    <p className={style.username}>@Svetloslav</p>
                    <p>(Svetloslav Novoselski)</p>
                </div>
            </div>
            <div>
                <Item link='/users/my-info' text='Моят профил' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='/' text='Comming soon...' icon='user'/>
                <Item link='#' onClick={logoutUser} text='Изход' icon='door-open'/>
                <div className='col-md-12 text-center' onClick={() => closeNavigation(false)}>
                    <img className={style.collapseIcon} src={UpArrow} alt="Close Icon"/>
                </div>
            </div>
        </div>
    );
};

export default UserNavigation;