import React, {useState, useEffect} from 'react';
import * as style from './TreesAroundMe.module.scss';
import TreeService from '../../../../../services/treeService';
import TreePartial from "../../../../common/TreePartial/TreePartial";

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
                {trees.map(tree => <TreePartial tree={tree}/>)}
            </div>
        </div>
    )
};

export default TreesAroundMe;