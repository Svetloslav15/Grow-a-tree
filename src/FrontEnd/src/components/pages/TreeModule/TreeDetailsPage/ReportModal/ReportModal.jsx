import React, {useState} from 'react';
import {useSelector} from 'react-redux';

import styles from './Report.module.scss';
import InputField from '../../../../common/InputField/InputField';
import DropdownField from '../../../../common/DropdownField/DropdownField';
import FileInput from '../../../../common/FileInput/FileInput';
import Button from '../../../../common/Button/Button';
import TreeService from '../../../../../services/treeService';
import AlertService from '../../../../../services/alertService';

import ContentTypes from '../../../../../static/contentTypes';
import SuccessMessages from "../../../../../static/successMessages";
import treeReportTypes from "../../../../../static/treeReportType";

const BgShapeTwo = require('../../../../../assets/bg-shape-2.png');
const BgShapeThree = require('../../../../../assets/bg-shape-3.png');
const BgAltTag = 'Background Shape Image';

const ReportModal = ({closeModal, treeId}) => {
    const [data, setData] = useState({message: '', type: 'Липсващо', treeId});
    const currUser = useSelector(state => state.auth);

    const addReport = async () => {
        const formData = new FormData();

        formData.append('type', treeReportTypes[data.type]);
        formData.append('message', data.message);
        formData.append('imageFile', data.imageFile);
        formData.append('treeId', data.treeId);
        formData.append('userId', currUser.id);

        const response = await TreeService.postAuthorizedReportTree(formData, currUser.accessToken, ContentTypes.FormData);

        if (response.succeeded) {
            setData({});
            return await AlertService.success(SuccessMessages.successReportTree);
        }
        return await AlertService.error(response.errors[0]);
    }
    const handleChange = (event) => {
        data[event.target.id] = event.target.value;
        setData(data);
    };
    const handleFilesUpload = (event) => {
        setData({imageFile: event.target.files[0], ...data});
    }

    return (
        <div className={styles.wrapper}>
            <div className={styles.wrapper__bgOverlay} onClick={closeModal}></div>
            <div className={styles.wrapper__modal}>
                <img className={styles.bgShapeImage} src={BgShapeTwo} alt={BgAltTag}/>
                <img className={styles.bgShapeImage} src={BgShapeThree} alt={BgAltTag}/>
                <i className={`fas fa-times ${styles.closeButton}`} onClick={closeModal}/>
                <p className={styles.wrapper__modal__title}>Докладвай проблем</p>
                <div className={styles.wrapper__modal__form}>
                    <InputField type='text'
                                label='Описание'
                                onChange={handleChange}
                                id='message'/>
                    <DropdownField defaultValue='Липсващо'
                                   values={['Счупено', 'Изсъхнало', 'Наранено']}
                                   onChange={handleChange}
                                   id='type'/>
                    <FileInput onChange={handleFilesUpload} isMultiple={false}/>
                    <div className='w-100 text-center'>
                        <Button type='Dark' onClick={addReport} className='mx-auto'>Добави</Button>
                    </div>
                </div>
            </div>
        </div>
    )
};

export default ReportModal;