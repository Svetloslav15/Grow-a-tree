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
        setReplies(replies.filter(x => x.treePostId === postId));
        setPostId(postId);
    }, [replies, postId]);

    const reactToReply = () => {

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
            setReplies([...repliesData, response.data]);
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
                {repliesData.map(x => <li className={styles.wrapper__items__item}>
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