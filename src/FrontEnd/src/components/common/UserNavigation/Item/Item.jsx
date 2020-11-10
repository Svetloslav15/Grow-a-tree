import React from 'react';
import {Link} from 'react-router-dom';

import * as style from './Item.module.scss';

const Item = ({icon, link, text}) => (
    <div className={style.item}>
        <i className={`${style.icon} fas fa-${icon}`}/>
        <Link to={link}>
            <span className={style.text}>{text}</span>
        </Link>
    </div>
);

export default Item;