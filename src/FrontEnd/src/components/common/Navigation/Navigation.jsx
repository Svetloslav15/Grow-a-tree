import React from 'react';
import NavItem from './NavItem/NavItem';
import NavCollapseButton from './NavCollapseButton/NavCollapseButton';
import * as style from './Navigation.module.scss';
import Button from '../../common/Button/Button';

const Logo = require('../../../assets/logo.png');

const Navigation = () => {
    return (
        <nav className={`${style.navigation} py-0 navbar navbar-expand-lg`}>
            <NavCollapseButton/>
            <img src={Logo} className={style.logo} alt="Grow A Tree Logo"/>
            <div className="collapse navbar-collapse" id="basicExampleNav">
                <ul className="navbar-nav mr-auto">
                    <NavItem link={'#'}>Карта</NavItem>
                    <NavItem link={'#'}>За платформата</NavItem>
                    <NavItem link={'#'}>Класация</NavItem>
                    <NavItem link={'#'}>Магазин</NavItem>
                    <NavItem link={'#'}>Събития</NavItem>
                    <NavItem link={'#'}>Форум</NavItem>
                    <NavItem link={'/login'}>
                        <Button type='GreenOutline'>Вход</Button>
                    </NavItem>
                </ul>
            </div>
        </nav>
    )
};

export default Navigation;