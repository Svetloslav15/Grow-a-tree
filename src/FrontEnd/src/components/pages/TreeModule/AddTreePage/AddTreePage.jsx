import React, {useState} from 'react';
import {useSelector} from 'react-redux';

import Layout from '../../../common/Layout/Layout';
import TreeService from '../../../../services/treeService';
import GeoCodingService from '../../../../services/geocodingService';
import AlertService from '../../../../services/alertService';
import ContentTypes from '../../../../static/contentTypes';
import SuccessMessages from '../../../../static/successMessages';
import ErrorMessages from '../../../../static/errorMessages';
import FormUpsertTree from '../FormUpsertTree/FormUpsertTree';

const AddTreePage = ({}) => {
    const [data, setData] = useState({
        nickName: '',
        type: '',
        categorie: '',
        latitude: '',
        longitude: '',
        city: '',
        ownerId: ''
    });
    const [location, setLocation] = useState('');

    const currUser = useSelector(state => state.auth);

    const handleChange = (event) => {
        data[event.target.id] = event.target.value;
        setData(data);
    };

    const handleCoordinates = async (latitude, longitute) => {
        const res = await GeoCodingService.getCityByCoords(latitude, longitute);
        const {municipality, suburb, village} = res.data.address;
        const result = [municipality, suburb, village]
            .filter(x => x !== undefined)
            .map(x => `${x} `);
        setLocation(result);
        data.city = res.data.address.municipality;
        data.latitude = latitude;
        data.longitude = longitute;
        setData({...data});
    };

    const handleFilesUpload = (event) => {
        setData({files: event.target.files, ...data})
    };

    const handleSubmit = async () => {
        const formData = new FormData();
        let images;
        if (data.files) {
            images = [...data.files];
            for (let i = 0; i < images.length; i++) {
                formData.append('ImageFiles', images[i]);
            }
        }
        formData.append('nickname', data.nickname);
        formData.append('type', data.type);
        formData.append('category', data.category);
        formData.append('latitude', data.latitude);
        formData.append('longitude', data.longitude);
        formData.append('City', data.city);
        formData.append('ownerId', currUser.id);

        const res = await TreeService.postAuthorizedAddTree(formData, currUser.accessToken, ContentTypes.FormData);

        if (res.succeeded) {
            setData({});
            return await AlertService.success(SuccessMessages.successAddedTree);
        }
        return await AlertService.error(res.errors[0]);
    };

    return (
        <Layout>
            <FormUpsertTree title='Добави дърво'
                            data={data}
                            type='Добави'
                            location={location}
                            handleChange={handleChange}
                            handleFilesUpload={handleFilesUpload}
                            handleSubmit={handleSubmit}
                            handleCoordinates={handleCoordinates}/>
        </Layout>
    );
};

export default AddTreePage;