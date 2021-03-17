import React, {useState, useEffect} from 'react';
import {Link} from 'react-router-dom';
import style from './RecentTrees.module.scss';
import TreeService from '../../../../../services/treeService';
import {useSelector} from "react-redux";

const BgImage = require('../../../../../assets/recent-trees-bg.png');
const UserImage = require('../../../../../assets/user-profile.png');

const RecentTrees = () => {
    const [trees, setTrees] = useState([]);

    useEffect(() => {
        getTrees();
    }, []);

    const getTrees = async () => {
        const response = await TreeService.getRecentTrees('?perPage=10');
        setTrees(response.data.data);
    }

    return (
        <div className={`col-md-12 row ${style.wrapper}`}>
            <div className={`col-md-8 ${style.wrapper__infoSection}`}>
                <img className={style.wrapper__infoSection__bgImage} src={BgImage} alt="Trees Forest Home"/>
                <Link to='/trees/add'>
                    <span className={style.wrapper__infoSection__button}># ЗАСАДИ ДЪРВО</span>
                </Link>
                <p className={style.wrapper__infoSection__text}>
                    Най-доброто време за засаждане на дърво е преди 20 години, а второто най-добро е сега.
                    Засади дърво и помогни на природата.
                </p>
            </div>
            <div className={`col-md-4 ${style.wrapper__treesSection}`}>
                <h3 className={style.wrapper__treesSection__title}>Последно засадени</h3>
                <ul className={style.wrapper__treesSection__items}>
                    {
                        trees && trees.map(x =>
                            <li className={style.wrapper__treesSection__items__item}>
                                <img className={style.wrapper__treesSection__items__item__userImage}
                                     src={x.owner.profilePictureUrl}
                                     alt={x.owner.userName}/>
                                <div className={style.wrapper__treesSection__items__item__user}>
                                    <p className={style.wrapper__treesSection__items__item__user__name}>
                                        {x.owner.userName}
                                    </p>
                                    <p className={style.wrapper__treesSection__items__item__user__date}>
                                        {new Date(x.plantedOn).toJSON().slice(0,10).split('-').reverse().join('/')}
                                    </p>
                                </div>
                                <p className={style.wrapper__treesSection__items__item__user__tree}>1 дърво</p>
                            </li>
                        )
                    }
                </ul>
            </div>
        </div>
    )
}

export default RecentTrees;