import React from 'react';
import styles from './WateringModal.module.scss';

const BgShapeBlue = require('../../../../../assets/bg-shape-blue.png');
const SunImage = require('../../../../../assets/sun-image.svg');
const WateringImage = require('../../../../../assets/watering-image.png');

const WateringModal = ({xp, closeModal}) => {
    return (
        <div className={styles.wrapper}>
            <div className={styles.overlayBg} onClick={closeModal}></div>
            <div className={styles.wrapper__modal}>
                <img className={styles.bgShapeImage} src={SunImage} alt="Background Shape Image"/>
                <img className={styles.bgShapeImage} src={WateringImage} alt="Background Shape Image"/>
                <img className={styles.bgShapeImage} src={BgShapeBlue} alt="Background Shape Image"/>
                <i className={`fas fa-times ${styles.closeButton}`} onClick={closeModal}/>
                <p className={styles.wrapper__modal__title}>Честито</p>
                <p className={styles.wrapper__modal__description}>Успешно поляхте дървото Яворчо! Получавате {xp} XP!</p>
            </div>
        </div>
    )
};

export default WateringModal;