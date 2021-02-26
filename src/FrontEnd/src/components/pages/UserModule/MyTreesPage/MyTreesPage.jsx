import React, {useEffect, useState} from 'react';
import {useSelector} from 'react-redux';

import * as style from './MyTreesPage.module.scss';
import Layout from '../UserLayout/UserLayout';
import ReportsSection from './ReportsSection/ReportsSection';
import TreeService from '../../../../services/treeService';

const MyTreesPage = () => {
    const stateUserData = useSelector(state => state.auth);
    const [userTrees, setUserTrees] = useState([]);
    const [activeReportTypes, setActiveReportTypes] = useState([]);
    const [archivedReportTypes, setArchivedReportTypes] = useState([]);

    useEffect(() => {
        TreeService.getAuthorizedTreesByUser(`?id=${stateUserData.id}`, stateUserData.accessToken)
            .then((data) => {
                setUserTrees(data.data.data);
                getActiveReportTypes(data.data.data);
                getArchivedReportTypes(data.data.data);
            });
    }, []);

    const getActiveReportTypes = (trees) => {
        for (const tree of trees) {
            TreeService.getAuthorizedActiveReportTypes(`?treeId=${tree.id}`, stateUserData.accessToken)
                .then((data) => setActiveReportTypes(data.data.data));
        }
    }

    const getArchivedReportTypes = (trees) => {
        for (const tree of trees) {
            TreeService.getAuthorizedArchivedReportTypes(`?treeId=${tree.id}`, stateUserData.accessToken)
                .then((data) => setArchivedReportTypes(data.data.data));
        }
    }

    return (
        <Layout>
            <div className={'col-md-9 row mt-5'}>
                <div className={'col-md-8'}>
                    <h3 className={style.pageTitle}># Моята виртуална горичка</h3>
                </div>
                <div className={'col-md-4'}>
                    <h3 className={style.pageTitle}>Общо 56 дървета</h3>
                </div>
                <ReportsSection
                    activeTypes={activeReportTypes}
                    archivedTypes={archivedReportTypes}
                />
            </div>
        </Layout>
    )
}

export default MyTreesPage;