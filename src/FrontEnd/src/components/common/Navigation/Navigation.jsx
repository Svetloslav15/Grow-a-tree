import React from 'react';
import NavItem from './NavItem/NavItem';
import NavCollapseButton from './NavCollapseButton/NavCollapseButton';
import * as style from './Navigation.module.scss';
import Button from '../../common/Button/Button';
import {Link} from 'react-router-dom';
const Logo = require('../../../assets/logo.png');

const Navigation = () => {
    return (
        <nav className={`${style.navigation} py-0 navbar navbar-expand-lg`}>
            <NavCollapseButton/>
            <Link to="/"><img src={Logo} className={style.logo} alt="Grow A Tree Logo"/></Link>
            <div className="collapse navbar-collapse" id="basicExampleNav">
                <ul className="navbar-nav mr-auto">
                    <NavItem link='/map'>Карта</NavItem>
                    <NavItem link='/about'>За платформата</NavItem>
                    <NavItem link='/ranking'>Класация</NavItem>
                    <NavItem link='/shop'>Магазин</NavItem>
                    <NavItem link='/events'>Събития</NavItem>
                    <NavItem link='/forum'>Форум</NavItem>
                    <NavItem link={'/auth/login'}>
                        <Button type='GreenOutline'>Вход</Button>
                    </NavItem>
                </ul>
            </div>
        </nav>
    )
};

export default Navigation;