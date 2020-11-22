import React from 'react';
import {useDispatch} from 'react-redux';
import {withRouter} from 'react-router-dom';

import Item from './Item/Item';
import * as style from './UserNavigation.module.scss';
import Cookies from "js-cookie";
import CookieNames from "../../../static/cookieNames";
import {SAVE_CURRENT_USER} from "../../../store/actions/actionTypes";
const UserImage = require('../../../assets/user-profile.png');
const UpArrow = require('../../../assets/up-arrow.png');

const UserNavigation = ({isOpen, closeNavigation, isLocked, history}) => {
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
        history.push('/');
    };
    return (
        <div className={`${style.wrapper} ${isOpen ? '' : 'd-none'}  ${isLocked ? 'd-none' : ''} `}>
            <div className={`mx-0 row ${style.userSection}`}>
                <img className={style.userProfileImage} src={UserImage} alt=""/>
                <div className=''>
                    <p className={style.username}>@Svetloslav</p>
                    <p>(Svetloslav Novoselski)</p>
                </div>
            </div>
            <div>
                <Item link='/users/my-info' text='Моят профил' icon='user'/>
                <Item link='/' text='Смени парола' icon='user'/>
                <Item link='/' text='Моите дървета' icon='user'/>
                <Item link='/' text='Моите събития' icon='user'/>
                <Item link='/' text='Предизвикателства' icon='user'/>
                <Item link='/' text='Сертификати' icon='user'/>
                <Item link='/users/referral' text='Покани приятел' icon='user'/>
                <Item link='/' text='Активност' icon='user'/>
                <Item link='/' text='Форум' icon='user'/>
                <Item link='/' text='Дарения' icon='user'/>
                <Item link='/' text='Последни влизания' icon='user'/>
                <Item link='#' onClick={logoutUser} text='Изход' icon='door-open'/>
                <div className='col-md-12 text-center' onClick={() => closeNavigation(false)}>
                    <img className={style.collapseIcon} src={UpArrow} alt="Close Icon"/>
                </div>
            </div>
        </div>
    );
};

export default withRouter(UserNavigation);