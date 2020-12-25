import React from 'react';
import './ReactionButton.scss';
import Button from "../Button/Button";

const LikeImage = require('../../../assets/reaction-like.png');
const HeartImage = require('../../../assets/reaction-heart.png');
const SadImage = require('../../../assets/reaction-sad.png');
const LaughImage = require('../../../assets/reaction-laugh.png');

const ReactionButton = () => {
    return (
        <div className='wrapper'>
            <div className='wrapper__popup'>
                <img className='wrapper__popup__image' src={LikeImage} alt="Like Reaction Image"/>
                <img className='wrapper__popup__image' src={HeartImage} alt="Like Reaction Image"/>
                <img className='wrapper__popup__image' src={LaughImage} alt="Like Reaction Image"/>
                <img className='wrapper__popup__image' src={SadImage} alt="Like Reaction Image"/>
            </div>
            <div className='wrapper__button'>
                <Button type='OutlineGreen'>Реагирай</Button>
            </div>
        </div>
    )
};

export default ReactionButton;