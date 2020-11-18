import React, {useState} from 'react';
import {useSelector} from 'react-redux';
import Layout from '../../../common/Layout/Layout';
import TreeService from '../../../../services/treeService';
import AlertService from '../../../../services/alertService';
import ContentTypes from '../../../../static/contentTypes';
import SuccessMessages from '../../../../static/successMessages';
import ErrorMessages from '../../../../static/errorMessages';
import FormUpsertTree from '../FormUpsertTree/FormUpsertTree';

const EditTreePage = () => {
    const [data, setData] = useState({
        email: '',
        nickName: '',
        type: '',
        categorie: '',
        latitude: '',
        longitude: '',
        city: '',
        ownerId: ''
    });
    const currUser = useSelector(state => state.auth);

    const handleChange = (event) => {
        data[event.target.id] = event.target.value;
        setData(data);
    };

    const handleCoordinates = (latitude, longitute) => {
        data.latitude = latitude;
        data.longitude = longitute;
        setData({...data});
    };

    const handleFilesUpload = (event) => {
        setData({files: event.target.files, ...data})
    };

    const handleSubmit = async () => {
        if (Object.keys(data).length !== 6) {
            return AlertService.error(ErrorMessages.allFieldsAreRequired);
        }
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
        formData.append('city', "Blagoevgrad");
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
            <FormUpsertTree title='Промени дърво'
                            data={data}
                            type='Промени'
                            handleChange={handleChange}
                            handleFilesUpload={handleFilesUpload}
                            handleSubmit={handleSubmit}
                            handleCoordinates={handleCoordinates}/>
        </Layout>
    );
};

export default EditTreePage;