import React from 'react';
import './ReactionButton.scss';
import Button from '../Button/Button';
import {Reactions} from '../../../static/reactionTypes';

const LikeImage = require('../../../assets/reaction-like.png');
const HeartImage = require('../../../assets/reaction-heart.png');
const SadImage = require('../../../assets/reaction-sad.png');
const LaughImage = require('../../../assets/reaction-laugh.png');

const ReactionButton = ({reactToPost}) => {
    return (
        <div className='wrapper'>
            <div className='wrapper__popup'>
                <img className='wrapper__popup__image'
                     src={LikeImage}
                     alt="Like Reaction Image"
                     onClick={() => reactToPost(Reactions.Like)}/>
                <img className='wrapper__popup__image'
                     src={HeartImage}
                     alt="Heart Reaction Image"
                     onClick={() => reactToPost(Reactions.Heart)}/>
                <img className='wrapper__popup__image'
                     src={LaughImage}
                     alt="Laugh Reaction Image"
                     onClick={() => reactToPost(Reactions.Laugh)}/>
                <img className='wrapper__popup__image'
                     src={SadImage}
                     alt="Sad Reaction Image"
                     onClick={() => reactToPost(Reactions.Sad)}/>
            </div>
            <div className='wrapper__button'>
                <Button type='OutlineGreen'>Реагирай</Button>
            </div>
        </div>
    )
};

export default ReactionButton;