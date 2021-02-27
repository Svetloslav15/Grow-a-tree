import React from 'react';
import * as styles from './NotFound.module.scss';
import {Link} from "react-router-dom";
import Button from '../../../common/Button/Button';

const BgImageTop = require('../../../../assets/bg-shape-2.png');
const BgImageBottom = require('../../../../assets/bg-shape-3.png');

const NotFoundPage = () => (
    <div className={styles.wrapper}>
        <img src={BgImageTop} className={styles.wrapper__topImage} alt="Green shape background"/>
        <h1>404</h1>
        <h3>Страницата не е намерена</h3>
        <div>
            <Button type='DarkOutline'>
                <Link to='/'>Начало</Link>
            </Button>
        </div>
        <img src={BgImageBottom} className={styles.wrapper__bottomImage} alt="Green shape background"/>
    </div>
);

export default NotFoundPage;