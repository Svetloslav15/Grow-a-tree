import React, {useEffect, useState} from 'react';
import parse from 'html-react-parser';
import {useSelector, useDispatch} from 'react-redux';

import './TreePost.scss';
import ReactionButton from '../../../../common/ReactionButton/ReactionButton';
import TreeService from '../../../../../services/treeService';
import AlertService from '../../../../../services/alertService';
import SuccessMessages from '../../../../../static/successMessages';

const TreePost = ({data, fetchTreePosts}) => {
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
        let response = await TreeService.postAuthorizedUpsertTreeReaction(bodyData, currUser.accessToken);
        response.succeeded ? await AlertService.success(SuccessMessages.successAddedPostReaction) : await AlertService.error(response.errors[0]);

        response = await TreeService.getAuthorizedTreePostReactions(`?postId=${post.id}`, currUser.accessToken);
        post.reactions = response.data.data;
        setPost(post);
        fetchTreePosts();
    }

    return (
        <div className='post'>
            <div className='post__user-section'>
                <img className='post__user-section__avatar' src={post.userProfilePictureUrl} alt={post.userUserName}/>
                <p className='post__user-section__username'>{post.userUserName}</p>
            </div>
            <div className='post__content'>
                {parse(post.content)}
            </div>
            <div>
                <ReactionButton reactTo={reactToPost} item={post} reactionsVisible={true}/>
            </div>
        </div>
    );
}

export default TreePost;