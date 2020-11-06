import React from 'react';
import * as style from './NavCollapseButton.module.scss';

const NavCollapseButton = () => (
    <button className={`${style.navCustom} navbar-toggler`}
            type="button"
            data-toggle="collapse"
            data-target="#basicExampleNav"
            aria-controls="basicExampleNav"
            aria-expanded="false"
            aria-label="Toggle navigation">
        <i className="fas fa-bars"/>
    </button>
);

export default NavCollapseButton;