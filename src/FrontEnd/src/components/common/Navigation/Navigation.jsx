import React from 'react';
import NavItem from './NavItem/NavItem';
import NavCollapseButton from './NavCollapseButton/NavCollapseButton';
import * as style from './Navigation.module.scss';

const Logo = require('../../../assets/logo.png');

const Navigation = () => {
    return (
        <nav className={`${style.navigation} navbar navbar-expand-lg`}>
            <NavCollapseButton/>
            <img src={Logo} className={style.logo} alt="Grow A Tree Logo"/>
            <div className="collapse navbar-collapse" id="basicExampleNav">
                <ul className="navbar-nav mr-auto">
                    <NavItem text={'Карта'} link={'#'}/>
                    <NavItem text={'За платформата'} link={'#'}/>
                    <NavItem text={'Класация'} link={'#'}/>
                    <NavItem text={'Магазин'} link={'#'}/>
                    <NavItem text={'Събития'} link={'#'}/>
                    <NavItem text={'Форум'} link={'#'}/>
                </ul>
            </div>
        </nav>
    )
};

export default Navigation;