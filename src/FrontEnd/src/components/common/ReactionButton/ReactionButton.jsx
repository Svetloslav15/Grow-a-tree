import React, {useEffect, useState} from 'react';
import styles from './ReactionButton.module.scss';
import Button from '../Button/Button';
import {Reactions} from '../../../static/reactionTypes';

const LikeImage = require('../../../assets/reaction-like.png');
const HeartImage = require('../../../assets/reaction-heart.png');
const SadImage = require('../../../assets/reaction-sad.png');
const LaughImage = require('../../../assets/reaction-laugh.png');

const ImagesAltTags = {
    SadImage: 'Sad Reaction Image',
    HeartImage: 'Heart Reaction Image',
    LikeImage: 'Like Reaction Image',
    LaughImage: 'Laugh Reaction Image',
}

const ReactionButton = ({reactTo, item, reactionsVisible, hasBorder}) => {
    const [reactions, setReactions] = useState(item.reactions);
    const [currReactionTypes, setCurrReactionTypes] = useState([]);
    const [isDataFetched, setIsDataFetched] = useState(false);
    const [isFirstTime, setFirstTime] = useState(true);
    const [areReactionsVisible, setReactionVisible] = useState(reactionsVisible);

    useEffect(() => {
        if (!isDataFetched) {
            setReactions(item.reactions)
            setIsDataFetched(true);
            if (isFirstTime) {
                checkReactionTypes();
                setFirstTime(false);
            }
        }
        else {
            checkReactionTypes();
            setIsDataFetched(false);
        }

    }, [item, reactions]);

    const checkReactionTypes = () => {
        const currImages = [];
        if (reactions) {
            if (reactions.filter(x => x.type === 1).length > 0) {
                currImages.push(HeartImage);
            }
            if (reactions.filter(x => x.type === 2).length > 0) {
                currImages.push(LaughImage);
            }
            if (reactions.filter(x => x.type === 3).length > 0) {
                currImages.push(LikeImage);
            }
            if (reactions.filter(x => x.type === 4).length > 0) {
                currImages.push(SadImage);
            }
        }

        setCurrReactionTypes(currImages);
    }

    return (
        <div className={`${styles.wrapper} ${(!hasBorder ? styles.noBorder : '')}`}>
            <div className={styles.wrapper__popup}>
                <img className={styles.wrapper__popup__image}
                     src={LikeImage}
                     alt={ImagesAltTags.LikeImage}
                     onClick={() => reactTo(Reactions.Like)}/>
                <img className={styles.wrapper__popup__image}
                     src={HeartImage}
                     alt={ImagesAltTags.HeartImage}
                     onClick={() => reactTo(Reactions.Heart)}/>
                <img className={styles.wrapper__popup__image}
                     src={LaughImage}
                     alt={ImagesAltTags.LaughImage}
                     onClick={() => reactTo(Reactions.Laugh)}/>
                <img className={styles.wrapper__popup__image}
                     src={SadImage}
                     alt={ImagesAltTags.SadImage}
                     onClick={() => reactTo(Reactions.Sad)}/>
            </div>
            <div className={styles.wrapper__reactions}>
                {areReactionsVisible ? <span className={styles.wrapper__reactions__count}>{reactions && reactions.length}</span> : '' }
                {areReactionsVisible ? currReactionTypes.map(x => (<img className={styles.wrapper__reactions__image}
                                                     src={x}
                                                     alt='Reaction Image'/>)) : ''}
            </div>
            <div className={styles.wrapper__buttonSection}>
                <Button type='OutlineGreen'>Реагирай</Button>
            </div>
        </div>
    )
};

export default ReactionButton;