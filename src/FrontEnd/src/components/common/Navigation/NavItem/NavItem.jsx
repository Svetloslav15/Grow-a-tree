import React from 'react';
import * as style from './NavItem.module.scss';

const NavItem = ({children, link}) => (
    <li className={`${style.wrapper} nav-item`}>
        <a className={`${style.link} nav-link`} href={link}>{children}</a>
    </li>
);

export default NavItem;