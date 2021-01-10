import React, {useEffect, useState} from 'react';
import styles from './Report.module.scss';

const BgShapeTwo = require('../../../../../assets/bg-shape-2.png');
const BgShapeThree = require('../../../../../assets/bg-shape-3.png');
const BgAltTag = 'Background Shape Image';

const ReportModal = ({closeModal}) => {
    return (
        <div className={styles.wrapper}>
            <div className={styles.wrapper__bgOverlay} onClick={closeModal}></div>
            <div className={styles.wrapper__modal}>
                <img className={styles.bgShapeImage} src={BgShapeTwo} alt={BgAltTag}/>
                <img className={styles.bgShapeImage} src={BgShapeThree} alt={BgAltTag}/>
                <i className={`fas fa-times ${styles.closeButton}`} onClick={closeModal}/>
                <p className={styles.wrapper__modal__title}>Добави доклад</p>
            </div>
        </div>
    )
};

export default ReportModal;