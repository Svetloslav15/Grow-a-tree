import React, {useState, useEffect} from 'react';
import {Link} from 'react-router-dom';
import {useSelector, useDispatch} from 'react-redux';
import Cookies from 'js-cookie';

import UserNavigation from '../UserNavigation/UserNavigation';
import NavItemDropdown from "../NavItemDropdown/NavItemDropdown";
import NavItem from './NavItem/NavItem';
import NavCollapseButton from './NavCollapseButton/NavCollapseButton';
import * as style from './Navigation.module.scss';
import Button from '../../common/Button/Button';
import {CHANGE_IS_USER_NAV_OPENED} from "../../../store/actions/actionTypes";
import CookieNames from "../../../static/cookieNames";
import DropdownField from "../DropdownField/DropdownField";
import staticPagesData from "../../../static/staticPagesData";
const Logo = require('../../../assets/logo.png');

const Navigation = () => {
    const commonData = useSelector(state => state.common);
    const currUser = useSelector(state => state.auth);
    const dispatch = useDispatch();

    const toggleNavOpen = () => {
        dispatch({type: CHANGE_IS_USER_NAV_OPENED, data: !commonData.isUserNavOpen});
    };

    return (
        <nav className={`${style.navigation} py-0 navbar navbar-expand-lg`}>
            <NavCollapseButton/>
            <Link to="/"><img src={Logo} className={style.logo} alt="Grow A Tree Logo"/></Link>
            <div className="collapse navbar-collapse" id="basicExampleNav">
                <ul className="navbar-nav mr-auto">
                    <NavItem link='/map'>Карта</NavItem>
                    <NavItem link='/about'>За платформата</NavItem>
                    <NavItemDropdown mainTitle='Информация' links={staticPagesData}/>
                    <NavItem link='/about'>За платформата</NavItem>
                    {
                        !currUser.accessToken ?
                            (
                                <NavItem link={'/auth/login'}>
                                    <Button type='GreenOutline'>Вход</Button>
                                </NavItem>
                            )
                            :
                            (
                                <>
                                    <NavItem link='#'
                                             isBold={true}
                                             onClick={toggleNavOpen}>
                                        Здравей, {currUser.username}!
                                    </NavItem>
                                    <UserNavigation isOpen={commonData.isUserNavOpen}
                                                    closeNavigation={toggleNavOpen}
                                                    isLocked={commonData.isUserNavLocked}/>
                                </>
                            )
                    }
                </ul>
            </div>
        </nav>
    )
};
export default Navigation;