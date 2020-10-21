import React from 'react';
import * as style from './NavItem.module.scss';
import {Link} from 'react-router-dom';

const NavItem = ({children, link}) => (
    <li className={`${style.wrapper} nav-item`}>
        <Link className={`${style.link} nav-link`} to={link}>{children}</Link>
    </li>
);

export default NavItem;