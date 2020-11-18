import React, {useState, useEffect} from 'react';
import {useSelector} from 'react-redux';

import * as style from './AddTreePage.module.scss';
import Layout from '../../../common/Layout/Layout';
import InputField from '../../../common/InputField/InputField';
import DropdownField from '../../../common/DropdownField/DropdownField';
import Map from '../../../common/Map/Map';
import Button from '../../../common/Button/Button';
import FileInput from '../../../common/FileInput/FileInput';
import TreeService from '../../../../services/treeService';
import AlertService from '../../../../services/alertService';
import ContentTypes from '../../../../static/contentTypes';
import SuccessMessages from '../../../../static/successMessages';
import ErrorMessages from '../../../../static/errorMessages';
import TreeCategories from '../../../../static/treeCategories';
import TreeTypes from '../../../../static/treeTypes';
import InputAutoComplete from "../../../common/InputAutoComplete/InputAutoComplete";
import FormUpsertTree from "../FormUpsertTree/FormUpsertTree";

const AddTreePage = ({}) => {
    const [data, setData] = useState({});
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
        formData.append('City', "Blagoevgrad");
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
                            handleChange={handleChange}
                            handleFilesUpload={handleFilesUpload}
                            handleSubmit={handleSubmit}
                            handleCoordinates={handleCoordinates}/>
        </Layout>
    );
};

export default AddTreePage;