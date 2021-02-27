import React from 'react';
import * as style from './Layout.module.scss';

const BgShape3 = require('../../../assets/bg-shape-3.png');
const BgShape4 = require('../../../assets/bg-shape-4.png');

const Layout = ({children, customStyle}) => (
    <div className={`${style.wrapper} ${customStyle} mx-0 row mb-3`}>
        <img src={BgShape3} className='shape3'/>
        <img src={BgShape4} className='shape4'/>
        {children}
    </div>
);
export default Layout;