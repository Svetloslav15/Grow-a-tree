import React from 'react';

const NavCollapseButton = () => (
    <button className="navbar-toggler"
            type="button"
            data-toggle="collapse"
            data-target="#basicExampleNav"
            aria-controls="basicExampleNav"
            aria-expanded="false"
            aria-label="Toggle navigation">
        <span className="navbar-toggler-icon"/>
    </button>
);

export default NavCollapseButton;