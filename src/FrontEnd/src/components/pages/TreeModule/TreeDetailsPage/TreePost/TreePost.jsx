import React, {useState} from 'react';
import parse from 'html-react-parser';
import {useSelector} from 'react-redux';

import './TreePost.scss';
import ReactionButton from '../../../../common/ReactionButton/ReactionButton';
import TreeService from '../../../../../services/treeService';

const TreePost = ({data}) => {
    const currUser = useSelector(state => state.auth);

    const reactToPost = async (reaction) => {
        console.log(data.id);
        const bodyData = {
            type: reaction,
            treePostId: data.id
        }
        const response = await TreeService.postAuthorizedUpsertTreeReaction(bodyData, currUser.accessToken);
        console.log(response);
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
                <ReactionButton reactToPost={reactToPost}/>
            </div>
        </div>
    );
}

export default TreePost;