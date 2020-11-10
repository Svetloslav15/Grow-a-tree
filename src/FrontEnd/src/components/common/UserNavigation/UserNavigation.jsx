import React from 'react';
import {Link} from 'react-router-dom';
import Item from './Item/Item';

import * as style from './UserNavigation.module.scss';
const UserImage = require('../../../assets/user-profile.png');

const UserNavigation = ({props}) => (
    <div className={style.wrapper}>
        <div className={`mx-0 row ${style.userSection}`}>
            <img className={style.userProfileImage} src={UserImage} alt=""/>
            <div className=''>
                <p>@Svetloslav</p>
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
        </div>
    </div>
);

export default UserNavigation;