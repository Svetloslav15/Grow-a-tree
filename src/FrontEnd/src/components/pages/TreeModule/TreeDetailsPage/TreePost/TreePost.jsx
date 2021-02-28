import React, {useEffect, useState} from 'react';
import parse from 'html-react-parser';
import {useSelector} from 'react-redux';

import './TreePost.scss';
import ReactionButton from '../../../../common/ReactionButton/ReactionButton';
import TreeService from '../../../../../services/treeService';
import AlertService from '../../../../../services/alertService';
import SuccessMessages from '../../../../../static/successMessages';
import RepliesSection from "../RepliesSection/RepliesSection";
import successMessages from "../../../../../static/successMessages";

const TreePost = ({data, fetchTreePosts, treeId}) => {
    const [post, setPost] = useState(data);
    const currUser = useSelector(state => state.auth);

    useEffect(() => {
        setPost(data);
    }, [data]);


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
        fetchTreePosts(treeId);
    }

    const deletePost = async (postId) => {
        const response = await TreeService.postAuthorizedDeletePost({id: postId}, currUser.accessToken);
        if (response.succeeded) {
            await AlertService.success(successMessages.successDeletePost);
            await fetchTreePosts();
        }
        else {
            await AlertService.error(response?.errors[0]);
        }
    }

    return (
        <div className='post'>
            <div className='post__user-section'>
                <img className='post__user-section__avatar' src={post.userProfilePictureUrl} alt={post.userUserName}/>
                <p className='post__user-section__username'>{post.userUserName}</p>
                {
                    post.userId === currUser.id && (
                        <button onClick={() => deletePost(post.id)}>
                            Изтрий
                        </button>
                    )
                }
                <div>

                </div>
            </div>
            <div className='post__content'>
                {parse(post.content)}
            </div>
            <div>
                <ReactionButton reactTo={reactToPost}
                                item={post}
                                reactionsVisible={true}
                                hasBorder={true}/>
            </div>
            {post && <RepliesSection postId={post.id}/>}
        </div>
    );
}

export default TreePost;