import React, {useEffect, useState} from 'react';
import parse from 'html-react-parser';
import {useSelector} from 'react-redux';
import {Editor} from '@tinymce/tinymce-react';

import './TreePost.scss';
import ReactionButton from '../../../../common/ReactionButton/ReactionButton';
import TreeService from '../../../../../services/treeService';
import AlertService from '../../../../../services/alertService';
import SuccessMessages from '../../../../../static/successMessages';
import RepliesSection from "../RepliesSection/RepliesSection";
import successMessages from "../../../../../static/successMessages";

const TreePost = ({data, fetchTreePosts, treeId}) => {
    const [post, setPost] = useState(data);
    const [isInEditMode, toggleEditMode] = useState(false);
    const [editorKey, setEditorKey] = useState(4);

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
            await fetchTreePosts(post.treeId);
        } else {
            await AlertService.error(response?.errors[0]);
        }
    }

    const editPost = async () => {
       if (isInEditMode) {
           const response = await TreeService.postAuthorizedUpsertTreePost(post, currUser.accessToken);
           if (response.succeeded) {
               await AlertService.success(successMessages.successEditPost);
               await fetchTreePosts(post.treeId);
               toggleEditMode(false);
           } else {
               await AlertService.error(response?.errors[0]);
           }
       }
       else {
           toggleEditMode(true);
       }
    }

    const handleEditorChange = async (data) => {
        setPost({...post, content: data});
    }

    return (
        <div className='post'>
            <div className='post__user-section'>
                <img className='post__user-section__avatar' src={post.userProfilePictureUrl} alt={post.userUserName}/>
                <p className='post__user-section__username'>{post.userUserName}</p>
                {
                    post.userId === currUser.id && (
                        <div className='ml-auto row mr-2'>
                            <button className='btn btn-danger delete-button-p-sm mr-2'
                                    onClick={() => deletePost(post.id)}>
                                <i className="fas fa-trash-alt"></i>
                            </button>
                            <button className='btn btn-warning delete-button-p-sm'
                                    onClick={() => editPost(post.id)}>
                                <i className="far fa-edit"></i>
                            </button>
                        </div>
                    )
                }
                <div>
                </div>
            </div>
            <div className='post__content'>
                {
                    !isInEditMode ?
                        parse(post.content)
                        :
                        <Editor
                            key={editorKey}
                            apiKey={process.env.REACT_APP_TINYMCE_KEY}
                            init={{
                                height: 200,
                                menubar: false,
                                plugins: [
                                    'advlist autolink lists link image charmap print preview anchor',
                                    'searchreplace visualblocks code fullscreen',
                                    'insertdatetime media table paste code help wordcount imagetools'
                                ],
                                toolbar:
                                    'formatselect  | image link | bold italic backcolor | \
                                    alignleft aligncenter alignright alignjustify',
                                file_picker_types: 'file image',
                            }}
                            value={post.content}
                            onEditorChange={handleEditorChange}
                        />
                }

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