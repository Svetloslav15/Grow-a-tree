import React, {useState} from 'react';
import parse from 'html-react-parser';

import './TreePost.scss';
import ReactionButton from '../../../../common/ReactionButton/ReactionButton';
import ReactionTypes from '../../../../../static/reactionTypes';

const TreePost = ({data}) => {
    const [selectedReactionType, setSelectedReactionType] = useState('');
    const reactToPost = () => {

    }

    return (
        <div className='post'>
            <div className='post__user-section'>
                <img className='post__user-section__avatar' src={data.userProfilePictureUrl} alt={data.userUserName}/>
                <p className='post__user-section__username' >{data.userUserName}</p>
            </div>
            <div className='post__content'>
                {parse(data.content)}
            </div>
            <div>
                <ReactionButton {...setSelectedReactionType}/>
            </div>
        </div>
    );
}

export default TreePost;