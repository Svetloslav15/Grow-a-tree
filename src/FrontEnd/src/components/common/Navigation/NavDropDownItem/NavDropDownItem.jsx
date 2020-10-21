import React from 'react';

const NavDropDownItem = () => {
    return(
        <li className="nav-item dropdown">
            <a className="nav-link dropdown-toggle" id="navbarDropdownMenuLink" data-toggle="dropdown"
               aria-haspopup="true" aria-expanded="false">Dropdown</a>
            <div className="dropdown-menu dropdown-primary" aria-labelledby="navbarDropdownMenuLink">
                <a className="dropdown-item" href="#">Action</a>
                <a className="dropdown-item" href="#">Another action</a>
                <a className="dropdown-item" href="#">Something else here</a>
            </div>
        </li>

    )
};

export default NavDropDownItem;