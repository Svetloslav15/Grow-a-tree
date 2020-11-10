import React from 'react';
import {Link} from 'react-router-dom';

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
        <div>
            <i className={`${style.icon} fas fa-user`}/>
            <Link to='/'>Моят профил</Link>
        </div>
    </div>
);

export default UserNavigation;