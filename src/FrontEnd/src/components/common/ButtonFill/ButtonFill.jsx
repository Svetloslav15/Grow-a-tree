import React from 'react';
import {btn} from './ButtonFill.module.scss';

const ButtonFill = ({children, className}) => {
    return (
        <button className={`${className} ${btn}`}>{children}</button>
    );
};

export default ButtonFill;