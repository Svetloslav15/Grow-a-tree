import React, {useEffect, useState} from 'react';
import styles from './ListModal.module.scss';
import {getDifference} from '../../../../../helpers/getDifferenceBetweenDates';

const DefaultProfilePicture = require('../../../../../assets/user-profile.png');
const BgShapeTwo = require('../../../../../assets/bg-shape-2.png');
const BgShapeThree = require('../../../../../assets/bg-shape-3.png');

const BgAltTag = 'Background Shape Image';

const ListModal = ({data, closeModal, hasReaction, title}) => {
    const [message, setMessage] = useState('поля дърво преди');

    useEffect(() => {
        console.log(data);
        if (hasReaction) {
            setMessage('реагира преди');
        }
    }, []);

    return (
        <div className={styles.wrapper}>
            <div className={styles.overlayBg} onClick={closeModal}></div>
            <div className={styles.wrapper__modal}>
                <img className={styles.bgShapeImage} src={BgShapeTwo} alt={BgAltTag}/>
                <img className={styles.bgShapeImage} src={BgShapeThree} alt={BgAltTag}/>
                <i className={`fas fa-times ${styles.closeButton}`} onClick={closeModal}/>

                <p className={styles.wrapper__modal__title}>{title}</p>
                <ul className={styles.wrapper__modal__items}>
                    {
                        data.map(x => (
                            <li className={styles.wrapper__modal__items__item}>
                                {
                                    x.userProfilePictureUrl && <img src={x.userProfilePictureUrl} alt={x.userUserName}
                                                                    className={styles.wrapper__modal__items__item__image}/>
                                }

                                <p className={styles.wrapper__modal__items__item__name}>{x.userUserName}</p>
                                <p className={styles.wrapper__modal__items__item__description}> - {message} <b>
                                         {hasReaction ? getDifference(new Date(x.createdOn), new Date()) : getDifference(new Date(x.wateredOn), new Date())}
                                    </b>
                                </p>
                            </li>
                        ))
                    }
                </ul>
            </div>
        </div>
    )
};

export default ListModal;