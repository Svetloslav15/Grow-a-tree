import React, {useState, useEffect} from 'react';

import * as style from './AddTreePage.module.scss';
import Layout from '../../../common/Layout/Layout';
import InputField from '../../../common/InputField/InputField';
import DropdownField from "../../../common/DropdownField/DropdownField";
import Map from "../../../common/Map/Map";
import Button from "../../../common/Button/Button";
import FileInput from "../../../common/FileInput/FileInput";

const AddTreePage = ({}) => {
    const [data, setData] = useState({});

    const handleChange = (event) => {
        data[event.target.id] = event.target.value;
        setData(data);
    };

    const handleCoordinates = (latitude, longitute) => {
        data.latitude = latitude;
        data.longitute = longitute;
        setData({...data});
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
                        <FileInput/>
                        <div className='text-right mt-3'>
                            <Button type='DarkOutline' onClick={() => console.log(data)}>Добави</Button>
                        </div>
                    </div>
                    <div className={`${style.mapContainer} col-md-5`}>
                        {data.latitude !== undefined ? (<p>Lat: <b>{data.latitude}</b>, Lng: <b>{data.longitute}</b></p>) : ''}
                        <Map handleCoordinates={handleCoordinates}/>
                    </div>
                </div>
            </div>
        </Layout>
    );
};

export default AddTreePage;