import React from 'react';
import NavItem from './NavItem/NavItem';
import NavDropDownItem from './NavDropDownItem/NavDropDownItem';
import NavCollapseButton from './NavCollapseButton/NavCollapseButton';
import NavInputItem from './NavInputItem/NavInputItem';
import * as style from './Navigation.module.scss';

const Navigation = () => {
    return (
        <nav className={`${style.navigation} navbar navbar-expand-lg`}>
            <NavCollapseButton/>
            <div className="collapse navbar-collapse" id="basicExampleNav">
                <ul className="navbar-nav mr-auto">
                    <NavItem text={'text'} link={'#'}/>
                    <NavItem text={'text1'} link={'#'}/>
                    <NavItem text={'text2'} link={'#'}/>
                    <NavItem text={'text3'} link={'#'}/>
                    <NavDropDownItem/>
                </ul>
                <NavInputItem/>
            </div>
        </nav>
    )
};

export default Navigation;