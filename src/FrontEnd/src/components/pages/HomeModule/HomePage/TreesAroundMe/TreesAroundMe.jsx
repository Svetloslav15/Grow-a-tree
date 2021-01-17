import React from 'react';
import * as style from './TreesAroundMe.module.scss';

const BgShapeTop = require('../../../../../assets/bg-shape-6.png');
const BgShapeBottom = require('../../../../../assets/bg-shape-7.png');

const TreesAroundMe = () => {
    return (
        <div className={style.wrapper}>
            <h1 className={style.wrapper__title}>Дървета около теб</h1>
            <img className={style.wrapper__bgImageTop} src={BgShapeTop}/>
            <img className={style.wrapper__bgImageBottom} src={BgShapeBottom} alt=""/>
            <div className={`col-md-10 mx-auto row ${style.wrapper__items}`}>
                <div className={`col-md-5 row ${style.wrapper__items__item}`}>
                    <img className={`col-md-2 ${style.wrapper__items__item__image}`} src="https://static.toiimg.com/photo/msid-67586673/67586673.jpg" alt=""/>
                    <div className={`col-md-10 ${style.wrapper__items__item__info}`}>
                        <h3 className={style.wrapper__items__item__info__name}>Яворът на Данчо</h3>
                        <p className={style.wrapper__items__item__info__description}>Описание на дървото...</p>
                    </div>
                </div>
                <div className={`col-md-5 row ${style.wrapper__items__item}`}>
                    <img className={`col-md-2 ${style.wrapper__items__item__image}`} src="https://static.toiimg.com/photo/msid-67586673/67586673.jpg" alt=""/>
                    <div className={`col-md-10 ${style.wrapper__items__item__info}`}>
                        <h3 className={style.wrapper__items__item__info__name}>Яворът на Данчо</h3>
                        <p className={style.wrapper__items__item__info__description}>Описание на дървото...</p>
                    </div>
                </div>
                <div className={`col-md-5 row ${style.wrapper__items__item}`}>
                    <img className={`col-md-2 ${style.wrapper__items__item__image}`} src="https://static.toiimg.com/photo/msid-67586673/67586673.jpg" alt=""/>
                    <div className={`col-md-10 ${style.wrapper__items__item__info}`}>
                        <h3 className={style.wrapper__items__item__info__name}>Яворът на Данчо</h3>
                        <p className={style.wrapper__items__item__info__description}>Описание на дървото...</p>
                    </div>
                </div>
                <div className={`col-md-5 row ${style.wrapper__items__item}`}>
                    <img className={`col-md-2 ${style.wrapper__items__item__image}`} src="https://static.toiimg.com/photo/msid-67586673/67586673.jpg" alt=""/>
                    <div className={`col-md-10 ${style.wrapper__items__item__info}`}>
                        <h3 className={style.wrapper__items__item__info__name}>Яворът на Данчо</h3>
                        <p className={style.wrapper__items__item__info__description}>Описание на дървото...</p>
                    </div>
                </div>
                <div className={`col-md-5 row ${style.wrapper__items__item}`}>
                    <img className={`col-md-2 ${style.wrapper__items__item__image}`} src="https://static.toiimg.com/photo/msid-67586673/67586673.jpg" alt=""/>
                    <div className={`col-md-10 ${style.wrapper__items__item__info}`}>
                        <h3 className={style.wrapper__items__item__info__name}>Яворът на Данчо</h3>
                        <p className={style.wrapper__items__item__info__description}>Описание на дървото...</p>
                    </div>
                </div>
            </div>
        </div>
    )
};

export default TreesAroundMe;