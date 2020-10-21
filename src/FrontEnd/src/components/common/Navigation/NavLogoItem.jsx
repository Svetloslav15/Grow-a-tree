import React from 'react';

const NavItem = ({children, link}) => (
    <li className="nav-item">
        <a className="navbar-brand" href={link}>{children}</a>
    </li>
);

export default NavItem;