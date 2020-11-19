import React, {useState, useEffect} from 'react';
import {useSelector} from 'react-redux';
import {withRouter} from 'react-router-dom';

import Layout from '../../../common/Layout/Layout';
import TreeService from '../../../../services/treeService';
import AlertService from '../../../../services/alertService';
import ContentTypes from '../../../../static/contentTypes';
import SuccessMessages from '../../../../static/successMessages';
import ErrorMessages from '../../../../static/errorMessages';
import FormUpsertTree from '../FormUpsertTree/FormUpsertTree';

const EditTreePage = ({history, match}) => {
    const [data, setData] = useState({
        id: '',
        nickName: '',
        type: '',
        category: '',
        latitude: '',
        longitude: '',
        city: '',
        ownerId: ''
    });
    const currUser = useSelector(state => state.auth);

    useEffect(() => {
        if (currUser) {
            TreeService.getAuthorizedTreeById(match.params.id, currUser.accessToken)
                .then(data => {
                    if (data.succeeded) {
                        setData({...data.data.data});
                    } else {
                        history.push('/');
                        AlertService.warning(ErrorMessages.treeNotExists);
                    }
                })
        }
    }, [currUser]);

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
        console.log(Object.keys(data));
        if (Object.keys(data).length < 6) {
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
        formData.append('id', data.id);
        formData.append('type', data.type);
        formData.append('category', data.category);
        formData.append('latitude', data.latitude);
        formData.append('longitude', data.longitude);
        formData.append('city', "Blagoevgrad");
        formData.append('ownerId', currUser.id);

        const res = await TreeService.postAuthorizedAddTree(formData, currUser.accessToken, ContentTypes.FormData);

        if (res.succeeded) {
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

export default withRouter(EditTreePage);