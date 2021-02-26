import React, {useEffect, useState} from 'react';
import {Link} from 'react-router-dom';
import {useSelector} from 'react-redux';

import * as style from './ReportsSection.module.scss';
import Button from '../../../../common/Button/Button';
import TreeService from '../../../../../services/treeService';
import AlertService from '../../../../../services/alertService';
import treeTypesHelper from '../../../../../static/treeReportTypesEn';
import treeTypesHelperNums from '../../../../../static/treeReportTypesNum';

const ReportsSection = ({activeTypes, archivedTypes, fetchData}) => {
    const [areActiveReportsSelected, selectActiveReports] = useState(true);
    const [currentTypes, setCurrentTypes] = useState(activeTypes);
    const stateUserData = useSelector(state => state.auth);
    const [currActiveData, setCurrentActiveData] = useState([]);
    const [activeTree, setActiveTree] = useState(null);

    useEffect(() => {
        toggleActiveCurrentData(true);
        setCurrentTypes(activeTypes);
    }, [activeTypes, archivedTypes]);

    const toggleActiveTypes = (activeSelected) => {
        selectActiveReports(activeSelected);
        activeSelected ? setCurrentTypes(activeTypes) : setCurrentTypes(archivedTypes);
    }

    const toggleActiveCurrentData = async (treeId, type) => {
        if (areActiveReportsSelected) {
            const data = await TreeService.getAuthorizedActiveReports(`?treeId=${treeId}&userId=${stateUserData.id}&reportType=${type}`, stateUserData.accessToken);
            setCurrentActiveData(data.data.data);
        } else {
            const data = await TreeService.getAuthorizedArchivedReports(`?treeId=${treeId}&userId=${stateUserData.id}&reportType=${type}`, stateUserData.accessToken);
            setCurrentActiveData(data.data.data);
        }
        setActiveTree(treeId);
    }

    const markAsSpam = async (treeReportId) => {
        const response = await TreeService.postAuthorizedMakeSpamReport({treeReportId: treeReportId}, stateUserData.accessToken);
        response.succeeded ? AlertService.success('Успешно маркирахте доклада като спам!') : AlertService.error(response.errors[0]);
        await fetchData();
        setCurrentActiveData(activeTypes);
    }

    const archiveReports = async () => {
        const data = {
            treeId: currActiveData[0].treeId,
            reportType: treeTypesHelperNums[currActiveData[0].type]
        };
        const response = await TreeService.postAuthorizedArchiveReports(data, stateUserData.accessToken);
        response.succeeded ? AlertService.success('Успешно архивирахте докладите!') : AlertService.error(response.errors[0]);
        await fetchData();
        setCurrentActiveData(activeTypes);
    }

    return (
        <div className={`col-md-12 p-3 row ${style.wrapper}`}>
            <div className={`col-md-5 ${style.wrapper__typesSection}`}>
                <h2 className={`px-4 ${style.wrapper__typesSection__title}`}># Доклади</h2>
                <div className={`px-4 row col-md-12 ${style.wrapper__typesSection__tabs}`}>
                    <p className={`col-md-6 ${areActiveReportsSelected ? style.wrapper__typesSection__tabs__activeTab : style.wrapper__typesSection__tabs__tab} `}
                       onClick={() => toggleActiveTypes(true)}>
                        Активни
                    </p>
                    <p className={`col-md-6 ${!areActiveReportsSelected ? style.wrapper__typesSection__tabs__activeTab : style.wrapper__typesSection__tabs__tab}`}
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
                                    activeTypes.map(x =>
                                        <li className={style.wrapper__typesSection__list__item}
                                            onClick={() => toggleActiveCurrentData(x.tree.id, x.type)}>
                                            # Вашето
                                            дърво {x.tree.nickname} е {treeTypesHelper[x.type]} {x.reportsCount} пъти
                                        </li>) :
                                    archivedTypes.map(x =>
                                        <li className={style.wrapper__typesSection__list__item}
                                            onClick={() => toggleActiveCurrentData(x.tree.id, x.type)}>
                                            # Вашето дърво е {treeTypesHelper[x.type]} {x.reportsCount} пъти
                                        </li>)
                            }
                        </ul>
                }

            </div>
            <div className={`col-md-7 ${style.wrapper__reports}`}>
                <div className='col-md-12 row'>
                    {
                        activeTree && <Button type='DarkOutline'>
                            <Link to={`/trees/details/${activeTree}`}>Виж дървото</Link>
                        </Button>
                    }
                    {
                        currentTypes.length > 0 && areActiveReportsSelected &&
                        <Button type='Green'
                                onClick={archiveReports}>
                            Одобри всички
                        </Button>
                    }

                </div>
                <div className={style.wrapper__reports__list}>
                    {
                        currActiveData && currActiveData.map(report => <div className={style.wrapper__reports__list__item}>
                            <div className={style.wrapper__reports__list__item__info}>
                                <img src={report.userProfilePictureUrl}
                                     alt={report.userUserName}/>
                                <span>
                                    {report.userUserName}
                                    <button className='btn btn-danger'
                                            onClick={() => markAsSpam(report.id)}>
                                        <i className="fas fa-exclamation-triangle mr-2"></i>
                                        Спам
                                    </button>
                                </span>
                            </div>
                            <div className={style.wrapper__reports__list__item__description}>
                                <p className={style.wrapper__reports__list__item__description__subtitle}>
                                    Описание на проблема
                                </p>
                                <p className={style.wrapper__reports__list__item__description__content}>
                                    {report.message}
                                </p>
                                <img className='w-85'
                                     src={report.imageUrl}
                                     alt={report.message}/>
                            </div>
                        </div>)
                    }
                </div>
            </div>
        </div>
    )
};

export default ReportsSection;