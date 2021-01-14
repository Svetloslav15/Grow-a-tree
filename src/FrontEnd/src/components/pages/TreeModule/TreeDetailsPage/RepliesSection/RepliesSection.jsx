import React, {useState, useEffect} from 'react';
import styles from './RepliesSection.module.scss';
import ReactionButton from "../../../../common/ReactionButton/ReactionButton";
import Button from "../../../../common/Button/Button";
import {Editor} from "@tinymce/tinymce-react";
import parse from 'html-react-parser';
import TreeService from '../../../../../services/treeService';
import AlertService from '../../../../../services/alertService';
import SuccessMessages from '../../../../../static/successMessages';
import {useSelector} from 'react-redux';

const RepliesSection = ({replies, postId}) => {
    const [repliesData, setReplies] = useState(replies);
    const [currPostId, setPostId] = useState(postId);
    const [isAddReplyInputOpen, setIsAddReplyInputOpen] = useState(false);
    const [editorKey, setEditorKey] = useState(4);
    const [currentReply, setCurrentReply] = useState('');
    const currUser = useSelector(state => state.auth);

    useEffect(() => {
        fetchTreePostReplies();
        setPostId(postId);
    }, [postId]);

    const reactToReply = async (type) => {
        //TODO add reaction to reply
        /*let response = await TreeService.postAuthorizedUpsertTreeReaction({
            type,
            treePostReplyId: data.id
        }, currUser.accessToken);*/
    }

    const fetchTreePostReplies = async () => {
        const response = await TreeService.getAuthorizedTreePostReplies(`?page=1&perPage=10000`, currUser.accessToken);
        if (response.data.succeeded) {
            setReplies(response.data.data.filter(x => x.treePostId === postId));
        }
    }

    const openAddReplyInput = () => {
        if (isAddReplyInputOpen) {
            addReply();
            setIsAddReplyInputOpen(false);
        } else {
            setIsAddReplyInputOpen(true);
        }
    }

    const handleEditorChange = (data) => {
        setCurrentReply(data);
    }

    const addReply = async () => {
        const response = await TreeService.postAuthorizedUpsertTreeReply({
            content: currentReply,
            treePostId: currPostId
        }, currUser.accessToken);

        if (response.succeeded) {
            await fetchTreePostReplies();
            return await AlertService.success(SuccessMessages.successPostReply);
        }
        return await AlertService.error(response.errors[0]);
    }

    return (
        <div className={styles.wrapper}>
            <ul className={styles.wrapper__items}>
                <Button type='Green' onClick={openAddReplyInput}>Коментирай</Button>
                {isAddReplyInputOpen && <Editor
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
                    onEditorChange={handleEditorChange}
                />}
                {repliesData && repliesData.map(x => <li className={styles.wrapper__items__item}>
                    <div className={styles.wrapper__items__item__userWrapper}>
                        <img src={x.userProfilePictureUrl} alt={x.userUserName}/>
                        <span>{x.userUserName}</span>
                    </div>
                    <p className={styles.wrapper__items__item__content}>{parse(x.content)}</p>
                    <ReactionButton hasBorder={false}
                                    reactionsVisible={true}
                                    hasCustomButton={true}
                                    reactTo={reactToReply}
                                    item={x}>
                        <i className={`${styles.fontAwesomeIcon} far fa-heart`}/>
                    </ReactionButton>
                </li>)}
            </ul>
        </div>
    )
};

export default RepliesSection;