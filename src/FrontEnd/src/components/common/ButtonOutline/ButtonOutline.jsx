import React from 'react';
import { MDBBtn } from 'mdbreact';
import {btn} from './ButtonOutline.module.scss';

const ButtonOutline = ({children}) => {
    return (
        <MDBBtn className={btn} outline>{children}</MDBBtn>
    );
};

export default ButtonOutline;