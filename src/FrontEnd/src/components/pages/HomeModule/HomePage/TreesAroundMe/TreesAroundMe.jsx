import React, {useState, useEffect} from 'react';
import {Link} from 'react-router-dom';
import * as style from './TreesAroundMe.module.scss';
import TreeService from '../../../../../services/treeService';

const BgShapeTop = require('../../../../../assets/bg-shape-6.png');
const BgShapeBottom = require('../../../../../assets/bg-shape-7.png');

const TreesAroundMe = () => {
    const [currLocation, setCurrentLocation] = useState({});
    const [trees, setTrees] = useState([]);

    useEffect(() => {
        navigator.geolocation.getCurrentPosition(setCoordinates)
    }, []);

    const setCoordinates = async (position) => {
        setCurrentLocation(position.coords);
        const response = await TreeService.getNearestTrees(`?latitude=${position.coords.latitude}&longitude=${position.coords.longitude}`);
        if (response.data.succeeded) {
            setTrees(response.data.data)
        }
    }
    return (
        <div className={style.wrapper}>
            <h1 className={style.wrapper__title}>Дървета около теб</h1>
            <img className={style.wrapper__bgImageTop} src={BgShapeTop}/>
            <img className={style.wrapper__bgImageBottom} src={BgShapeBottom} alt=""/>
            <div className={`col-md-10 mx-auto row ${style.wrapper__items}`}>
                {trees.map(tree =>
                    <Link to={`/trees/details/${tree.id}`} className={`col-md-5 row ${style.wrapper__items__item}`}>
                        <img className={`col-md-2 ${style.wrapper__items__item__image}`} src={tree.image.url}
                             alt={tree.nickName}/>
                        <div className={`col-md-10 ${style.wrapper__items__item__info}`}>
                            <h3 className={style.wrapper__items__item__info__name}>{tree.nickname}</h3>
                            <p className={style.wrapper__items__item__info__description}>Намира се на <span
                                className='font-weight-bold '><i>{tree.metresAway} метра</i></span> от теб</p>
                        </div>
                    </Link>)}
            </div>
        </div>
    )
};

export default TreesAroundMe;