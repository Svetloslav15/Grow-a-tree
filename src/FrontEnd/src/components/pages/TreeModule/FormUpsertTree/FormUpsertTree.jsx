import React, {useState, useEffect} from 'react';
import * as style from './FormUpsertTree.module.scss';
import InputField from '../../../common/InputField/InputField';
import InputAutoComplete from '../../../common/InputAutoComplete/InputAutoComplete';
import TreeTypes from '../../../../static/treeTypes';
import DropdownField from '../../../common/DropdownField/DropdownField';
import TreeCategories from '../../../../static/treeCategories';
import FileInput from '../../../common/FileInput/FileInput';
import Button from '../../../common/Button/Button';
import Map from '../../../common/Map/Map';

const FormUpsertTree = ({title, data, type, location, handleChange, handleFilesUpload, handleSubmit, handleCoordinates}) => {
    const [nickName, setNickname] = useState(data.nickname);

    useEffect(() => {
        setNickname(data.nickname);
    }, [data.nickname]);

    const customHandleChange = (event) => {
        setNickname(event.target.value);
        handleChange(event);
    }
    return (
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
                                value={nickName}
                                onChange={customHandleChange}/>
                    <InputAutoComplete label='Вид'
                                       id='type'
                                       data={TreeTypes}
                                       width={12}
                                       value={data.type}
                                       onChange={handleChange}/>
                    <DropdownField width={12}
                                   id='category'
                                   label='Категория'
                                   values={TreeCategories}
                                   defaultValue={data.category}
                                   onChange={handleChange}/>
                    <FileInput onChange={handleFilesUpload} isMultiple={true}/>
                    <div className='text-right mt-3'>
                        <Button type='DarkOutline' onClick={handleSubmit}>{type}</Button>
                    </div>
                </div>
                <div className={`${style.mapContainer} col-md-5`}>
                    <p>Местоположение: <span className='font-weight-bold'>{location}</span></p>
                    <Map handleCoordinates={handleCoordinates}
                         markers={[]}
                         canSetMarker={true}/>
                </div>
            </div>
        </div>
    );
}

export default FormUpsertTree;