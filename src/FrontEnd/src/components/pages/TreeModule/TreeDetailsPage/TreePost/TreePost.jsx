import React, {useEffect, useState} from 'react';
import parse from 'html-react-parser';
import {useSelector} from 'react-redux';

import './TreePost.scss';
import ReactionButton from '../../../../common/ReactionButton/ReactionButton';
import TreeService from '../../../../../services/treeService';

const TreePost = ({data}) => {
    const [post, setPost] = useState(data);
    useEffect(() => {
        setPost(data);
    }, [data]);

    const currUser = useSelector(state => state.auth);
    const reactToPost = async (reaction) => {
        const bodyData = {
            type: reaction,
            treePostId: data.id
        }
        const response = await TreeService.postAuthorizedUpsertTreeReaction(bodyData, currUser.accessToken);
        //TODO add toaster message
        console.log(response);
    }

    return (
        <div className='post'>
            <div className='post__user-section'>
                <img className='post__user-section__avatar' src={post.userProfilePictureUrl} alt={post.userUserName}/>
                <p className='post__user-section__username' >{post.userUserName}</p>
            </div>
            <div className='post__content'>
                {parse(post.content)}
            </div>
            <div>
                <ReactionButton reactToPost={reactToPost} data={post}/>
            </div>
        </div>
    );
}

export default TreePost;