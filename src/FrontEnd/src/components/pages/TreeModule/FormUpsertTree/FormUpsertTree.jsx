import React from 'react';
import * as style from './FormUpsertTree.module.scss';
import InputField from '../../../common/InputField/InputField';
import InputAutoComplete from '../../../common/InputAutoComplete/InputAutoComplete';
import TreeTypes from '../../../../static/treeTypes';
import DropdownField from '../../../common/DropdownField/DropdownField';
import TreeCategories from '../../../../static/treeCategories';
import FileInput from '../../../common/FileInput/FileInput';
import Button from '../../../common/Button/Button';
import Map from '../../../common/Map/Map';

const FormUpsertTree = ({title, data, type, handleChange, handleFilesUpload, handleSubmit, handleCoordinates}) => (
    <div className='mx-0 col-md-12 p-5'>
        <div className='mx-0 row justify-content-around'>
            <h2 className={style.title}># {title}</h2>
            <h3 className={style.subtitle}># Изберете локация за дървото</h3>
        </div>
        <div className='mx-0 row col-md-12'>
            <div className='mx-0 col-md-6'>
                <InputField type='email'
                            label={'Прякор'}
                            id='nickname'
                            width={12}
                            onChange={handleChange}/>
                <InputAutoComplete label='Вид'
                                   id='type'
                                   data={TreeTypes}
                                   width={12}
                                   onChange={handleChange}/>
                <DropdownField width={12}
                               id='category'
                               label='Категория'
                               values={TreeCategories}
                               onChange={handleChange}/>
                <FileInput onChange={handleFilesUpload}/>
                <div className='text-right mt-3'>
                    <Button type='DarkOutline' onClick={handleSubmit}>{type}</Button>
                </div>
            </div>
            <div className={`${style.mapContainer} col-md-5`}>
                {data.latitude !== undefined ? (
                    <p>Lat: <b>{data.latitude}</b>, Lng: <b>{data.longitude}</b></p>) : ''}
                <Map handleCoordinates={handleCoordinates}/>
            </div>
        </div>
    </div>
);

export default FormUpsertTree;