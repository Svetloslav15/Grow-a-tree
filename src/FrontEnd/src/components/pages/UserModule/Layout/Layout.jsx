import React from 'react';
import * as style from './Layout.module.scss';
import Sidebar from '../Sidebar/Sidebar';

const BgShape3 = require('../../../../assets/bg-shape-3.png');
const BgShape4 = require('../../../../assets/bg-shape-4.png');

const Layout = ({children}) => (
    <div className='mx-0 row'>
        <img src={BgShape3} className='shape3'/>
        <img src={BgShape4} className='shape4'/>
        <Sidebar/>
        {children}
    </div>
);
export default Layout;