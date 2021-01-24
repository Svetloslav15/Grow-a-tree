import React from 'react';
import {Link} from 'react-router-dom';
import style from './RecentTrees.module.scss';

const BgImage = require('../../../../../assets/recent-trees-bg.png');

const RecentTrees = () => {

    return (
        <div className={`col-md-12 row ${style.wrapper}`}>
            <div className={`col-md-6 ${style.wrapper__infoSection}`}>
                <img className={style.wrapper__infoSection__bgImage} src={BgImage} alt="Trees Forest Home"/>
                <Link to='/trees/add'>
                    <span className={style.wrapper__infoSection__button}># ЗАСАДИ ДЪРВО</span>
                </Link>
                <p className={style.wrapper__infoSection__text}>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Amet autem consectetur dicta, dignissimos
                    dolor earum esse, magni maxime neque nulla optio, pariatur perferendis quis rem voluptate. At
                    commodi laborum sequi.</p>
            </div>
            <div className={style.wrapper__treesSection}>
                <h3 className={style.wrapper__treesSection__title}>Последно засадени</h3>
                <ul className={style.wrapper__treesSection__items}>
                    <li className={style.wrapper__treesSection__items__item}>
                        <img className={style.wrapper__treesSection__items__item__userImage} src="" alt=""/>
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

export default RecentTrees;