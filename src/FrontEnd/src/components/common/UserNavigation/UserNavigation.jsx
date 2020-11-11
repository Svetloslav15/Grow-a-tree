import React from 'react';
import {Link} from 'react-router-dom';
import Item from './Item/Item';

import * as style from './UserNavigation.module.scss';
const UserImage = require('../../../assets/user-profile.png');
const UpArrow = require('../../../assets/up-arrow.png');

const UserNavigation = ({props}) => (
    <div className={style.wrapper}>
        <div className={`mx-0 row ${style.userSection}`}>
            <img className={style.userProfileImage} src={UserImage} alt=""/>
            <div className=''>
                <p className={style.username}>@Svetloslav</p>
                <p>(Svetloslav Novoselski)</p>
            </div>
        </div>
        <div>
            <Item link='/' text='Моят профил' icon='user'/>
            <Item link='/' text='Моят профил' icon='user'/>
            <Item link='/' text='Моят профил' icon='user'/>
            <Item link='/' text='Моят профил' icon='user'/>
            <Item link='/' text='Моят профил' icon='user'/>
            <Item link='/' text='Моят профил' icon='user'/>
            <Item link='/' text='Моят профил' icon='user'/>
            <Item link='/' text='Моят профил' icon='user'/>
            <Item link='/' text='Моят профил' icon='user'/>
            <div className='col-md-12 text-center'>
                <img className={style.collapseIcon} src={UpArrow} alt="Close Icon"/>
            </div>
        </div>
    </div>
);

export default UserNavigation;