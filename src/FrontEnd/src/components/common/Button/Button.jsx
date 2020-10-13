import React from 'react';
import { MDBBtn } from 'mdbreact';
import * as style from './Button.module.scss';

const Button = ({type, children}) => {
    const isOutlined = type.toLowerCase().includes('outline');

    return isOutlined ?
        (<MDBBtn className={`${style.BaseButton} ${style[type]}`} outline>{children}</MDBBtn>) :
        (<MDBBtn className={`${style.BaseButton} ${style[type]}`} >{children}</MDBBtn>);
};

export default Button;
