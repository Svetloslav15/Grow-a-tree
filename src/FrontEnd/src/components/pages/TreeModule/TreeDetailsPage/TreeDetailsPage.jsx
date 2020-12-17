import React, {useEffect, useState} from 'react';
import {useSelector} from 'react-redux';
import './TreeDetailsPage.scss';

import Layout from '../../../common/Layout/Layout';
import Carousel from './Carousel/Carousel';
import TreeService from "../../../../services/treeService";

const TreeDetailsPage = ({history, match}) => {
    const [tree, setTree] = useState([]);
    const currUser = useSelector(state => state.auth);

    useEffect(() => {
        TreeService.getAuthorizedTreeById(match.params.id, currUser.accessToken)
            .then((data) => {
                setTree(data.data.data);
                console.log(data.data.data);
            })
    }, []);

    return (
        <Layout>
            <section className='info-section'>
                {tree.images && <Carousel images={tree.images}/>}
                <section className='info-section__wrapper'>
                    <h1 className='info-section__wrapper__title'># {tree.nickname}</h1>
                    <p className='info-section__wrapper__status'>Статус: здраво</p>
                    <p className='info-section__wrapper__owner'>Засадено от: {tree.owner && tree.owner.userName}</p>
                </section>
            </section>
        </Layout>
    )
};

export default TreeDetailsPage;