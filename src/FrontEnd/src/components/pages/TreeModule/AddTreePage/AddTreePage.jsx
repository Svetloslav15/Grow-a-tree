import React, {useState, useEffect} from 'react';

import * as style from './AddTreePage.module.scss';
import Layout from '../../../common/Layout/Layout';
import InputField from '../../../common/InputField/InputField';
import DropdownField from "../../../common/DropdownField/DropdownField";
import Map from "../../../common/Map/Map";

const AddTreePage = ({}) => {
    const [data, setData] = useState({});

    const handleChange = (event) => {
        data[event.target.id] = event.target.value;
        setData(data);
    };

    const handleCoordinates = (latitude, longitute) => {
        //TODO
    };

    return (
        <Layout>
            <div className='offset-md-1 col-md-6 p-5'>
                <h2 className={style.title}># Добави дърво</h2>
                <div className='col-md-12 row'>
                    <div className='col-md-10'>
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
                    </div>
                    <div className={style.mapContainer}>
                        <Map handleCoordinates={handleCoordinates}/>
                    </div>
                </div>
            </div>
        </Layout>
    );
};

export default AddTreePage;