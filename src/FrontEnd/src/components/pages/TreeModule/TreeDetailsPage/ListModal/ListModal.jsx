import React, {useEffect, useState} from 'react';
import styles from './ListModal.module.scss';
import {getDifference} from '../../../../../helpers/getDifferenceBetweenDates';

const DefaultProfilePicture = require('../../../../../assets/user-profile.png');
const BgShapeTwo = require('../../../../../assets/bg-shape-2.png');
const BgShapeThree = require('../../../../../assets/bg-shape-3.png');
const HeartIcon = require('../../../../../assets/reaction-heart.png');
const LikeIcon = require('../../../../../assets/reaction-like.png');
const LaughIcon = require('../../../../../assets/reaction-laugh.png');
const SadIcon = require('../../../../../assets/reaction-sad.png');

const BgAltTag = 'Background Shape Image';

const ListModal = ({data, closeModal, hasReaction, title}) => {
    const [message, setMessage] = useState('поля дърво преди');
    const [reactionsByType, setReactionsByType] = useState([]);

    useEffect(() => {
        console.log(data);
        if (hasReaction) {
            setMessage('реагира преди');
            filterReactionsByType();
        }
    }, []);

    const filterReactionsByType = () => {
        const result = [[], [], [], []];

        for (const reaction of data) {
            if (reaction.type === 1) {
                result[0].push(reaction);
            }
            else if (reaction.type === 2) {
                result[1].push(reaction);
            }
            else if (reaction.type === 3) {
                result[2].push(reaction);
            }
            else if (reaction.type === 4) {
                result[3].push(reaction);
            }
        }
        setReactionsByType(result);
    }

    return (
        <div className={styles.wrapper}>
            <div className={styles.overlayBg} onClick={closeModal}></div>
            <div className={styles.wrapper__modal}>
                <img className={styles.bgShapeImage} src={BgShapeTwo} alt={BgAltTag}/>
                <img className={styles.bgShapeImage} src={BgShapeThree} alt={BgAltTag}/>
                <i className={`fas fa-times ${styles.closeButton}`} onClick={closeModal}/>

                <p className={styles.wrapper__modal__title}>
                    {hasReaction ?
                        (<div className={'d-flex row'}>
                            <div className={styles.wrapper__modal__item}>
                                <span>{reactionsByType[0] && reactionsByType[0].length}</span>
                                <img src={HeartIcon} alt=""/>
                            </div>
                            <div className={styles.wrapper__modal__item}>
                                <span>{reactionsByType[1] && reactionsByType[1].length}</span>
                                <img src={LaughIcon} alt=""/>
                            </div>
                            <div className={styles.wrapper__modal__item}>
                                <span>{reactionsByType[2] && reactionsByType[2].length}</span>
                                <img src={LikeIcon} alt=""/>
                            </div>
                            <div className={styles.wrapper__modal__item}>
                                <span>{reactionsByType[3] && reactionsByType[3].length}</span>
                                <img src={SadIcon} alt=""/>
                            </div>
                        </div>)
                        :
                        {title}
                    }
                </p>
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