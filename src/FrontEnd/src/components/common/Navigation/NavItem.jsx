import React from 'react';

const NavItem = ({text, link}) => (
    <li className="nav-item">
        <a className="nav-link" href={link}>{text}</a>
    </li>
);

export default NavItem;