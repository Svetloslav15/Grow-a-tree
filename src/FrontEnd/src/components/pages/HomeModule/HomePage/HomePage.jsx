import React, {useEffect} from 'react';
import {Link} from 'react-router-dom';
import * as style from './HomePage.module.scss';

import Button from '../../../common/Button/Button'

const BgShape1 = require('../../../../assets/bg-shape-1.png');
const BgShape2 = require('../../../../assets/bg-shape-2.png');
const BgShape5 = require('../../../../assets/bg-shape-5.png');
const TreeBgImage = require('../../../../assets/tree-for-bg.png');

const HomePage = () => {
    return (
        <div className={style.wrapper}>
            <div className='col-md-12 text-center'>
                <img src={TreeBgImage} className={style.treeBg} alt="Grow A Tree Bg Image"/>
            </div>
            <img src={BgShape1} className='shape1' alt="Grow A Tree Bg Shape 1"/>
            <img src={BgShape2} className='shape2' alt="Grow A Tree Bg Shape 2"/>
            <img src={BgShape5} className={style.shape5} alt="Grow A Tree Bg Shape 3"/>
            <div className={style.wrapperHeader}>
                <h1 className={style.title}>Посади дърво</h1>
                <Link to='/auth/register'>
                    <Button type='Dark' className={style.button}>Започни своето приключение</Button>
                </Link>
            </div>
        </div>
    );
};

export default HomePage;