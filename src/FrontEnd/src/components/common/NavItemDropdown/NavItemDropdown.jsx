import React from 'react';
import {Link} from 'react-router-dom';
import * as styles from './NavItemDropdown.module.scss';

const NavItemDropdown = ({mainTitle, links}) => (
    <li className={`${styles.mainLinkWrapper} nav-item dropdown`}>
        <a className={`nav-link dropdown-toggle ${styles.mainLink}`} href="#" id="navbarDropdown" role="button" data-toggle="dropdown"
           aria-haspopup="true" aria-expanded="false">
            {mainTitle}
        </a>
        <div className="dropdown-menu" aria-labelledby="navbarDropdown">
            {links.map((x, i) => <Link key={i} className='dropdown-item' to={`/static/${x.route}`}>{x.title}</Link>)}
        </div>
    </li>
);

export default NavItemDropdown;