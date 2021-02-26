import React, {useEffect, useState} from 'react';
import {Link} from 'react-router-dom';
import {useSelector} from 'react-redux';

import * as style from './ReportsSection.module.scss';
import Button from '../../../../common/Button/Button';
import TreeService from '../../../../../services/treeService';
import treeTypesHelper from '../../../../../static/treeReportTypesEn';

const ReportsSection = ({tree, activeTypes, archivedTypes}) => {
    const [areActiveReportsSelected, selectActiveReports] = useState(true);
    const [currentTypes, setCurrentTypes] = useState(activeTypes);
    const stateUserData = useSelector(state => state.auth);
    const [currActiveData, setCurrentActiveData] = useState([]);

    useEffect(() => {
        toggleActiveTypes(true);
    }, [activeTypes, archivedTypes]);

    const toggleActiveTypes = (activeSelected) => {
        selectActiveReports(activeSelected);
        activeSelected ? setCurrentTypes(activeTypes) : setCurrentTypes(archivedTypes);
    }

    const toggleActiveCurrentData = () => {

    }

    return (
        <div className={`col-md-12 p-3 row ${style.wrapper}`}>
            <div className={`col-md-5 ${style.wrapper__typesSection}`}>
                <h2 className={`px-4 ${style.wrapper__typesSection__title}`}># Доклади</h2>
                <div className={`px-4 row col-md-12 ${style.wrapper__typesSection__tabs}`}>
                    <p className={`col-md-6 ${style.wrapper__typesSection__tabs__activeTab} `}
                       onClick={() => toggleActiveTypes(true)}>
                        Активни
                    </p>
                    <p className={`col-md-6 ${style.wrapper__typesSection__tabs__tab}`}
                       onClick={() => toggleActiveTypes(false)}>
                        Архивирани
                    </p>
                </div>
                {
                    !currentTypes.length ?
                        <p className='text-white text-center font-weight-bold'>Все още няма доклади</p>
                        :
                        <ul className={`px-4 ${style.wrapper__typesSection__list}`}>
                            {
                                areActiveReportsSelected ?
                                    activeTypes.map(x => <li className={style.wrapper__typesSection__list__item}>
                                        # Вашето дърво е {treeTypesHelper[x.type]} {x.reportsCount} пъти
                                    </li>) :
                                    archivedTypes.map(x => <li className={style.wrapper__typesSection__list__item}>
                                        # Вашето дърво е {treeTypesHelper[x.type]} {x.reportsCount} пъти
                                    </li>)
                            }
                        </ul>
                }

            </div>
            <div className={`col-md-7 ${style.wrapper__reports}`}>
                <div className='col-md-12 row'>
                    <Button type='DarkOutline'>
                        <Link to={`trees/details/${tree.id}`}>Виж дървото</Link>
                    </Button>
                    <Button type='Green'>Одобри всички</Button>
                </div>
                <div className={style.wrapper__reports__list}>

                    <div className={style.wrapper__reports__list__item}>
                        <div className={style.wrapper__reports__list__item__info}>
                            <img src="https://www.pavilionweb.com/wp-content/uploads/2017/03/man-300x300.png" alt=""/>
                            <span>Svetloslav</span>
                        </div>
                        <div className={style.wrapper__reports__list__item__description}>
                            <p className={style.wrapper__reports__list__item__description__subtitle}>Описание на
                                проблема</p>
                            <p className={style.wrapper__reports__list__item__description__content}>Lorem ipsum dolor
                                sit amet, consectetur adipisicing elit.
                                Asperiores beataconsequuntur
                                corporis ea eligendi est illo illum nam nemo pariatur,
                                perspiciatis quis rem saepe.
                                Dolorem esse qui quibusdam quisquam velit.
                            </p>
                        </div>
                    </div>
                    <div className={style.wrapper__reports__list__item}>
                        <div className={style.wrapper__reports__list__item__info}>
                            <img src="https://www.pavilionweb.com/wp-content/uploads/2017/03/man-300x300.png" alt=""/>
                            <span>Svetloslav</span>
                        </div>
                        <div className={style.wrapper__reports__list__item__description}>
                            <p className={style.wrapper__reports__list__item__description__subtitle}>Описание на
                                проблема</p>
                            <p className={style.wrapper__reports__list__item__description__content}>Lorem ipsum dolor
                                sit amet, consectetur adipisicing elit.
                                Asperiores beataconsequuntur
                                corporis ea eligendi est illo illum nam nemo pariatur,
                                perspiciatis quis rem saepe.
                                Dolorem esse qui quibusdam quisquam velit.
                            </p>
                        </div>
                    </div>
                    <div className={style.wrapper__reports__list__item}>
                        <div className={style.wrapper__reports__list__item__info}>
                            <img src="https://www.pavilionweb.com/wp-content/uploads/2017/03/man-300x300.png" alt=""/>
                            <span>Svetloslav</span>
                        </div>
                        <div className={style.wrapper__reports__list__item__description}>
                            <p className={style.wrapper__reports__list__item__description__subtitle}>Описание на
                                проблема</p>
                            <p className={style.wrapper__reports__list__item__description__content}>Lorem ipsum dolor
                                sit amet, consectetur adipisicing elit.
                                Asperiores beataconsequuntur
                                corporis ea eligendi est illo illum nam nemo pariatur,
                                perspiciatis quis rem saepe.
                                Dolorem esse qui quibusdam quisquam velit.
                            </p>
                        </div>
                    </div>
                    <div className={style.wrapper__reports__list__item}>
                        <div className={style.wrapper__reports__list__item__info}>
                            <img src="https://www.pavilionweb.com/wp-content/uploads/2017/03/man-300x300.png" alt=""/>
                            <span>Svetloslav</span>
                        </div>
                        <div className={style.wrapper__reports__list__item__description}>
                            <p className={style.wrapper__reports__list__item__description__subtitle}>Описание на
                                проблема</p>
                            <p className={style.wrapper__reports__list__item__description__content}>Lorem ipsum dolor
                                sit amet, consectetur adipisicing elit.
                                Asperiores beataconsequuntur
                                corporis ea eligendi est illo illum nam nemo pariatur,
                                perspiciatis quis rem saepe.
                                Dolorem esse qui quibusdam quisquam velit.
                            </p>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    )
};

export default ReportsSection;