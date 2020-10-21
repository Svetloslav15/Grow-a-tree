import React from 'react';
import NavItem from './NavItem';
import NavDropDownItem from './NavDropDownItem';
import NavCollapseButton from './NavCollapseButton';
import NavInputItem from './NavInputItem';

const Navigation = () => {
    return (
        <nav className="navbar navbar-expand-lg navbar-dark primary-color">

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