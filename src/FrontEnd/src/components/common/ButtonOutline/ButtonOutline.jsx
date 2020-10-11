import React from 'react';
import {btn} from './ButtonOutline.module.scss';

const ButtonOutline = ({children}) => {
    return (
        <button className={btn}>{children}</button>
    );
};

export default ButtonOutline;