import React, {useEffect, useState} from 'react';
import './ReactionButton.scss';
import Button from '../Button/Button';
import {Reactions} from '../../../static/reactionTypes';

const LikeImage = require('../../../assets/reaction-like.png');
const HeartImage = require('../../../assets/reaction-heart.png');
const SadImage = require('../../../assets/reaction-sad.png');
const LaughImage = require('../../../assets/reaction-laugh.png');

const ReactionButton = ({reactTo, item, reactionsVisible}) => {
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
        <div className='wrapper'>
            <div className='wrapper__popup'>
                <img className='wrapper__popup__image'
                     src={LikeImage}
                     alt="Like Reaction Image"
                     onClick={() => reactTo(Reactions.Like)}/>
                <img className='wrapper__popup__image'
                     src={HeartImage}
                     alt="Heart Reaction Image"
                     onClick={() => reactTo(Reactions.Heart)}/>
                <img className='wrapper__popup__image'
                     src={LaughImage}
                     alt="Laugh Reaction Image"
                     onClick={() => reactTo(Reactions.Laugh)}/>
                <img className='wrapper__popup__image'
                     src={SadImage}
                     alt="Sad Reaction Image"
                     onClick={() => reactTo(Reactions.Sad)}/>
            </div>
            <div className='wrapper__reactions'>
                {areReactionsVisible ? <span className='wrapper__reactions__count'>{reactions && reactions.length}</span> : '' }
                {areReactionsVisible ? currReactionTypes.map(x => (<img className='wrapper__reactions__image'
                                                     src={x}
                                                     alt='Reaction Image'/>)) : ''}
            </div>
            <div className='wrapper__buttonSection'>
                <Button type='OutlineGreen'>Реагирай</Button>
            </div>
        </div>
    )
};

export default ReactionButton;