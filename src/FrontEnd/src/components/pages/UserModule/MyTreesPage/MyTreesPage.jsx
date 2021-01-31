import React, {useEffect, useState} from 'react';
import * as style from './MyTreesPage.module.scss';
import Layout from '../UserLayout/UserLayout';
import ReportsSection from './ReportsSection/ReportsSection';

const MyTreesPage = () => {
    return (
        <Layout>
            <div className={'col-md-9 row mt-5'}>
                <div className={'col-md-8'}>
                    <h3 className={style.pageTitle}># Моята виртуална горичка</h3>
                </div>
                <div className={'col-md-4'}>
                    <h3 className={style.pageTitle}>Общо 56 дървета</h3>
                </div>
                <ReportsSection/>
            </div>
        </Layout>
    )
}

export default MyTreesPage;