import React, {useState, useEffect} from 'react';

import * as style from './AddTreePage.module.scss';
import Layout from '../../../common/Layout/Layout';
import InputField from '../../../common/InputField/InputField';
import DropdownField from "../../../common/DropdownField/DropdownField";
import Map from "../../../common/Map/Map";
import Button from "../../../common/Button/Button";
import FileInput from "../../../common/FileInput/FileInput";
import TreeService from '../../../../services/treeService';

const AddTreePage = ({}) => {
    const [data, setData] = useState({});
    const currUser = useSelector(state => state.auth);

    const handleChange = (event) => {
        data[event.target.id] = event.target.value;
        setData(data);
    };

    const handleCoordinates = (latitude, longitute) => {
        data.latitude = latitude;
        data.longitute = longitute;
        setData({...data});
    };

    const handleFilesUpload = (event) => {
        setData({files: event.target.files, ...data})
    };

    const handleSubmit = async () => {
        const formData = new FormData();
        formData.append('ImageFiles', data.files);
        formData.append('nickname', data.nickname);
        formData.append('type', data.type);
        formData.append('category', data.category);
        formData.append('latitude', data.latitude);
        formData.append('longitude', data.longitude);
        formData.append('city', "Blg");
        formData.append('ownerId', currUser.id);
        const res = await TreeService.postAuthorized.addTree(formData, token, 'multipart/form-data');
        console.log(res);
    };

    return (
        <Layout>
            <div className='ml-5 col-md-12 p-5'>
                <div className='row justify-content-around'>
                    <h2 className={style.title}># Добави дърво</h2>
                    <h3 className={style.subtitle}># Изберете локация за дървото</h3>
                </div>
                <div className='row col-md-12'>
                    <div className='col-md-6'>
                        <InputField type='email'
                                    label={'Прякор'}
                                    id='nickname'
                                    width={12}
                                    onChange={handleChange}/>
                        <DropdownField width={12}
                                       id='type'
                                       label='Вид'
                                       values={['test', 'test1', 'test2']}
                                       onChange={handleChange}/>
                        <DropdownField width={12}
                                       id='category'
                                       label='Категория'
                                       values={['test', 'test1', 'test2']}
                                       onChange={handleChange}/>
                        <FileInput onChange={handleFilesUpload}/>
                        <div className='text-right mt-3'>
                            <Button type='DarkOutline' onClick={() => console.log(data)}>Добави</Button>
                        </div>
                    </div>
                    <div className={`${style.mapContainer} col-md-5`}>
                        {data.latitude !== undefined ? (
                            <p>Lat: <b>{data.latitude}</b>, Lng: <b>{data.longitute}</b></p>) : ''}
                        <Map handleCoordinates={handleCoordinates}/>
                    </div>
                </div>
            </div>
        </Layout>
    );
};

export default AddTreePage;