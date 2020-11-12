import React from 'react';
import {Link} from 'react-router-dom';

import * as style from './Item.module.scss';

const Item = ({icon, link, text, onClick}) => (
    <Link to={link}>
        <div className={style.item} onClick={onClick}>
            <i className={`${style.icon} fas fa-${icon}`}/>
            <span className={style.text}>{text}</span>
        </div>
    </Link>
);

export default Item;