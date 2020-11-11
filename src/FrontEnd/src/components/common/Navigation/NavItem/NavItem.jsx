import React from 'react';
import * as style from './NavItem.module.scss';
import {Link} from 'react-router-dom';

const NavItem = ({children, link, isBold}) => (
    <li className={`${style.wrapper} nav-item`}>
        <Link className={`${style.link} nav-link ${isBold ? 'font-weight-bold' : ''}`} to={link}>{children}</Link>
    </li>
);

export default NavItem;