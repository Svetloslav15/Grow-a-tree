import React from 'react';
import {btn} from './ButtonFill.module.scss';

const ButtonFill = ({children}) => {
    return (
        <button className={btn}>{children}</button>
    );
};

export default ButtonFill;