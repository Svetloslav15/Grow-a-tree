import React, {useEffect, useState} from 'react';
import {useSelector} from 'react-redux';
import {Editor} from '@tinymce/tinymce-react';

import './TreeDetailsPage.scss';

import Layout from '../../../common/Layout/Layout';
import Button from '../../../common/Button/Button';
import Carousel from './Carousel/Carousel';
import TreeService from '../../../../services/treeService';
import Map from '../../../common/Map/Map';
import Timer from './Timer/Timer';

import GeoCodingService from '../../../../services/geocodingService';

const ReportButton = require('../../../../assets/report-button.svg');

const TreeDetailsPage = ({history, match}) => {
    const [tree, setTree] = useState([]);
    const [treeLocation, setLocation] = useState('');
    const [currPost, setCurrPost] = useState({id: ''});

    const currUser = useSelector(state => state.auth);

    useEffect(() => {
        TreeService.getAuthorizedTreeById(match.params.id, currUser.accessToken)
            .then(async (data) => {
                const treeInfo = data.data.data;
                setTree(treeInfo);
                setCurrPost({treeId: treeInfo.id, ...currPost});
                const res = await GeoCodingService.getCityByCoords(treeInfo.latitude, treeInfo.longitude);
                const {municipality, suburb, village} = res.data.address;
                const result = [municipality, suburb, village]
                    .filter(x => x !== undefined)
                    .map(x => `${x} `);
                setLocation(result);
            })
    }, []);

    const addPost = async () => {
        const response = await TreeService.postAuthorizedUpsertTreePost(currPost, currUser.accessToken);
        console.log(response);
    }

    const handleEditorChange = async (data) => {
        setCurrPost({content: data, ...currPost});
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
                <Timer/>
                <p className='info-section__map__location-text'>{treeLocation}</p>
                <Map coordinates={{latitude: tree.latitude, longitude: tree.longitude}}
                     isStatic={true}
                     className='map-container-section'/>
            </section>
            <div className='info-section__posts'>
                <Editor
                    apiKey="e6k1kg0bmgdif8788ck1k320wefwovv7sll2ynujx30327lk"
                    init={{
                        height: 200,
                        menubar: false,
                        plugins: [
                            'advlist autolink lists link image charmap print preview anchor',
                            'searchreplace visualblocks code fullscreen',
                            'insertdatetime media table paste code help wordcount imagetools'
                        ],
                        toolbar:
                            'formatselect  | image link | bold italic backcolor | \
                            alignleft aligncenter alignright alignjustify',
                        file_picker_types: 'file image',
                    }}
                    onEditorChange={handleEditorChange}
                />
                <Button type='DarkOutline' onClick={addPost}>Добави</Button>
            </div>
        </Layout>
    )
};

export default TreeDetailsPage;