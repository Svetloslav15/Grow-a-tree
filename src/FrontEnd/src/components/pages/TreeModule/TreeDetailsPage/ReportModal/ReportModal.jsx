import React, {useEffect, useState} from 'react';
import styles from './Report.module.scss';
import InputField from '../../../../common/InputField/InputField';
import DropdownField from "../../../../common/DropdownField/DropdownField";
import FileInput from "../../../../common/FileInput/FileInput";
import Button from "../../../../common/Button/Button";

const BgShapeTwo = require('../../../../../assets/bg-shape-2.png');
const BgShapeThree = require('../../../../../assets/bg-shape-3.png');
const BgAltTag = 'Background Shape Image';

const ReportModal = ({closeModal}) => {
    const addReport = () => {

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
                    <InputField label='Описание'/>
                    <DropdownField defaultValue='Липсващо' values={['Счупено', 'Увехнало']}/>
                    <FileInput/>
                    <div className='w-100 text-center'>
                        <Button type='Dark' onClick={addReport} className='mx-auto'>Добави</Button>
                    </div>
                </div>
            </div>
        </div>
    )
};

export default ReportModal;