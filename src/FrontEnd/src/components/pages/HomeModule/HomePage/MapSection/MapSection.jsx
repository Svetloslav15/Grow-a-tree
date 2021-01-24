import React from 'react';
import style from './MapSection.module.scss';
import Button from '../../../../common/Button/Button';
import Map from "../../../../common/Map/Map";

const UserImage = require('../../../../../assets/user-profile.png');

const MapSection = () => {
    return (
        <div className={`col-md-12 row ${style.wrapper}`}>
            <div className={`col-md-8 ${style.wrapper__mapSection}`}>
                <div className={style.wrapper__mapSection__info}>
                    <h1 className={style.wrapper__mapSection__info__title}>Карта на дървета</h1>
                    <Button type='DarkOutline'># Засади дърво</Button>
                </div>
                <div className={style.wrapper__mapSection__wrapper}>
                    <Map/>
                </div>
            </div>
            <div className={`col-md-4 ${style.wrapper__treesSection}`}>
                <h3 className={style.wrapper__treesSection__title}>Последно засадени</h3>
                <ul className={style.wrapper__treesSection__items}>
                    <li className={style.wrapper__treesSection__items__item}>
                        <img className={style.wrapper__treesSection__items__item__userImage} src={UserImage} alt=""/>
                        <div className={style.wrapper__treesSection__items__item__user}>
                            <p className={style.wrapper__treesSection__items__item__user__name}>Svetloslav Novoselski</p>
                            <p className={style.wrapper__treesSection__items__item__user__date}>10/10/2020, 1:49:39 PM</p>
                        </div>
                        <p className={style.wrapper__treesSection__items__item__user__tree}>1 дърво</p>
                    </li>
                </ul>
            </div>
        </div>
    )
}

export default MapSection;