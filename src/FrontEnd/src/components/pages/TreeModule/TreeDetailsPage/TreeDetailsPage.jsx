import React, {useEffect, useState} from 'react';
import {useSelector} from 'react-redux';
import './TreeDetailsPage.scss';

import Layout from '../../../common/Layout/Layout';
import Carousel from './Carousel/Carousel';
import TreeService from "../../../../services/treeService";
import Map from '../../../common/Map/Map';
import GeoCodingService from '../../../../services/geocodingService';

const TreeDetailsPage = ({history, match}) => {
    const [tree, setTree] = useState([]);
    const [treeLocation, setLocation] = useState('');

    const currUser = useSelector(state => state.auth);

    useEffect(() => {
        TreeService.getAuthorizedTreeById(match.params.id, currUser.accessToken)
            .then(async (data) => {
                const treeInfo = data.data.data;
                setTree(treeInfo);
                const res = await GeoCodingService.getCityByCoords(treeInfo.latitude, treeInfo.longitude);
                const {municipality, suburb, village} = res.data.address;
                const result = [municipality, suburb, village]
                    .filter(x => x !== undefined)
                    .map(x => `${x} `);
                setLocation(result);
                console.log(result);
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
            <section className='info-section__map'>
                <p className='info-section__map__location-text'>{treeLocation}</p>
                <Map coordinates={{latitude: tree.latitude, longitude: tree.longitude}}/>
            </section>
        </Layout>
    )
};

export default TreeDetailsPage;