import React, {useEffect, useState} from 'react';
import {useSelector} from 'react-redux';
import { Editor } from '@tinymce/tinymce-react';

import './TreeDetailsPage.scss';

import Layout from '../../../common/Layout/Layout';
import Carousel from './Carousel/Carousel';
import TreeService from "../../../../services/treeService";
import Map from '../../../common/Map/Map';
import GeoCodingService from '../../../../services/geocodingService';

const ReportButton = require('../../../../assets/report-button.svg');

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
            })
    }, []);

    const handleEditorChange = () => {

    }

    return (
        <Layout>
            <section className='info-section'>
                {tree.images && <Carousel images={tree.images}/>}
                <section className='info-section__wrapper'>
                    <h1 className='info-section__wrapper__title'># {tree.nickname}</h1>
                    <p className='info-section__wrapper__status'>Статус: здраво</p>
                    <p className='info-section__wrapper__status'>Вид: {tree.type}</p>
                    <p className='info-section__wrapper__owner'>Засадено от: {tree.owner && tree.owner.userName}</p>
                </section>
                <img className='info-section__report-button' src={ReportButton} alt="Report Problem Button"/>
            </section>
            <section className='info-section__map'>
                <p className='info-section__map__location-text'>{treeLocation}</p>
                <Map coordinates={{latitude: tree.latitude, longitude: tree.longitude}}/>
            </section>
            <div className='info-section__posts'>
                <Editor
                    init={{
                        height: 200,
                        menubar: false,
                        plugins: [
                            'advlist autolink lists link image charmap print preview anchor',
                            'searchreplace visualblocks code fullscreen',
                            'insertdatetime media table paste code help wordcount'
                        ],
                        toolbar:
                            'undo redo | formatselect | bold italic backcolor | \
                            alignleft aligncenter alignright alignjustify | \
                            bullist numlist outdent indent | removeformat | help'
                    }}
                    onEditorChange={handleEditorChange}
                />
            </div>
        </Layout>
    )
};

export default TreeDetailsPage;