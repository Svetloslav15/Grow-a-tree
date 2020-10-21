import React from 'react';
import {h1} from './HomePage.module.scss';
import Button from '../../common/Button/Button'
import Navigation from '../../common/Navigation/Navigation';

const HomePage = () => {
    return (
        <div>
            <Navigation/>
            <h1 className={h1}>Home Page</h1>
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