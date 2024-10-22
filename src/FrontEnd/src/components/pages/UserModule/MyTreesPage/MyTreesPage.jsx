import React, {useEffect, useState} from 'react';
import {useSelector} from 'react-redux';

import * as style from './MyTreesPage.module.scss';
import Layout from '../UserLayout/UserLayout';
import ReportsSection from './ReportsSection/ReportsSection';
import TreeService from '../../../../services/treeService';
import TreePartial from "../../../common/TreePartial/TreePartial";

const MyTreesPage = () => {
    const stateUserData = useSelector(state => state.auth);
    const [userTrees, setUserTrees] = useState([]);
    const [activeReportTypes, setActiveReportTypes] = useState([]);
    const [archivedReportTypes, setArchivedReportTypes] = useState([]);

    useEffect(() => {
        TreeService.getAuthorizedTreesByUser(`?id=${stateUserData.id}`, stateUserData.accessToken)
            .then((data) => {
                data = data.data.data;
                data.forEach(x => x.image = x.images[0]);
                setUserTrees(data);
                getActiveReportTypes(data);
                getArchivedReportTypes(data);
            });
    }, []);

    const getActiveReportTypes = async (trees) => {
        const finalData = [];
        for (const tree of trees) {
            let data = await TreeService.getAuthorizedActiveReportTypes(`?treeId=${tree.id}`, stateUserData.accessToken);
            data = data.data.data;
            data.forEach(x => x.tree = tree);
            finalData.push(...data);
        }
        setActiveReportTypes(finalData);
    }

    const getArchivedReportTypes = async (trees) => {
        const finalData = [];
        for (const tree of trees) {
            let data = await TreeService.getAuthorizedArchivedReportTypes(`?treeId=${tree.id}`, stateUserData.accessToken);
            data = data.data.data;
            data.forEach(x => x.tree = tree);
            finalData.push(...data);
        }
        setArchivedReportTypes(finalData);
    }

    const fetchData = async () => {
        await getActiveReportTypes(userTrees);
        await getArchivedReportTypes(userTrees);
    }

    return (
        <Layout>
            <div className={'col-md-9 row mt-5'}>
                <div className={'col-md-12'}>
                    <h3 className={style.pageTitle}># Моите {userTrees.length} дървета</h3>
                    <div className='row'>
                        {userTrees && userTrees.map(x => <TreePartial tree={x}/>)}
                    </div>
                </div>
                <ReportsSection
                    activeTypes={activeReportTypes}
                    archivedTypes={archivedReportTypes}
                    fetchData={fetchData}
                />
            </div>
        </Layout>
    )
}

export default MyTreesPage;