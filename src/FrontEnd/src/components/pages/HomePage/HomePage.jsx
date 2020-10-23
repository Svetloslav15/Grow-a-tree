import React from 'react';
import * as style from './HomePage.module.scss';

import Button from '../../common/Button/Button'
import Navigation from '../../common/Navigation/Navigation';

const BgShape1 = require('../../../assets/bg-shape-1.png');
const BgShape2 = require('../../../assets/bg-shape-2.png');
const BgShape3 = require('../../../assets/bg-shape-3.png');

const HomePage = () => {
    return (
        <div>
            <Navigation/>
            <img src={BgShape1} className={style.shape1} alt=""/>
            <img src={BgShape2} className={style.shape2} alt=""/>
            <img src={BgShape3} className={style.shape3} alt=""/>
            <h1 className={style.h1}>Home Page</h1>
            <Button type='Green'>Green</Button>
            <Button type='GreenOutline'>GreenOutline</Button>
            <Button type='Dark'>Dark</Button>
            <Button type='DarkOutline'>DarkOutline</Button>
            <Button type='Red'>Red</Button>
            <Button type='Yellow'>Yellow</Button>
        </div>
    );
};

export default HomePage;