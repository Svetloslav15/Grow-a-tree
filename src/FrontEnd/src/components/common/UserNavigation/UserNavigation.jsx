import React from 'react';
import {Link} from 'react-router-dom';
import Item from './Item/Item';

import * as style from './UserNavigation.module.scss';

const UserNavigation = ({props}) => (
    <div className={style.wrapper}>
        <div className={`row ${style.userSection}`}>
            <img className={style.userProfileImage} src="" alt=""/>
            <div className=''>
                <p>@Svetloslav</p>
                <p>(Svetloslav Novoselski)</p>
            </div>
        </div>
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
);

export default UserNavigation;