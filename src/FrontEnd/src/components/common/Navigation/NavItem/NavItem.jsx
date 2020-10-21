import React from 'react';
import * as style from './NavItem.module.scss';

const NavItem = ({text, link}) => (
    <li className='nav-item'>
        <a className={`${style.link} nav-link`} href={link}>{text}</a>
    </li>
);

export default NavItem;