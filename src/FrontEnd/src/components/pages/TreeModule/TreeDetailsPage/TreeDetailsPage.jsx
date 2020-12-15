import React from 'react';
import {useSelector} from 'react-redux';
import * as style from './TreeDetailsPage.module.scss';

import Layout from '../../../common/Layout/Layout';
const TreeDetailsPage = ({}) => {

    return (
        <Layout>
            <section></section>
            <section>
                <h1># Яворчо</h1>
                <p>Статус: здраво</p>
                <p>Засадено от: Светлослав Новоселски</p>
            </section>
        </Layout>
    )
};

export default TreeDetailsPage;