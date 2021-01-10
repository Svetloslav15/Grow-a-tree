import React, {useState, useEffect} from 'react';
import styles from './RepliesSection.module.scss';
import ReactionButton from "../../../../common/ReactionButton/ReactionButton";
import Button from "../../../../common/Button/Button";
import {Editor} from "@tinymce/tinymce-react";
import parse from 'html-react-parser';

const RepliesSection = ({replies, postId}) => {
    const [repliesData, setReplies] = useState(replies);
    const [isAddCommentInputOpen, setIsAddCommentInputOpen] = useState(false);
    const [editorKey, setEditorKey] = useState(4);
    const [currentComment, setCurrentComment] = useState('');

    useEffect(() => {
        setReplies(replies.filter(x => x.treePostId === postId));
    }, [replies]);

    const reactToReply = () => {

    }

    const openAddCommentInput = () => {
        if (isAddCommentInputOpen) {
            addComment();
            setIsAddCommentInputOpen(false);
        } else {
            setIsAddCommentInputOpen(true);
        }
    }

    const handleEditorChange = (data) => {
        setCurrentComment(data);
    }

    const addComment = () => {
        console.log(currentComment);
    }

    return (
        <div className={styles.wrapper}>
            <ul className={styles.wrapper__items}>
                <Button type='Green' onClick={openAddCommentInput}>Коментирай</Button>
                {isAddCommentInputOpen && <Editor
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