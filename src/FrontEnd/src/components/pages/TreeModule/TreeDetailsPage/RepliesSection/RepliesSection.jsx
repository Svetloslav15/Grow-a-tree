import React, {useState, useEffect} from 'react';
import styles from './RepliesSection.module.scss';
import ReactionButton from "../../../../common/ReactionButton/ReactionButton";

const DefaultUserImage = require('../../../../../assets/user-profile.png');

const RepliesSection = ({replies, postId}) => {
    const [repliesData, setReplies] = useState(replies);

    useEffect(() => {
        setReplies(replies.filter(x => x.treePostId === postId));
    }, [replies]);

    const reactToReply = () => {

    }

    return (
        <div className={styles.wrapper}>
            <ul className={styles.wrapper__items}>
                {repliesData.map(x => <li className={styles.wrapper__items__item}>
                    <div className={styles.wrapper__items__item__userWrapper}>
                        <img src={x.userProfilePictureUrl} alt={x.userUserName}/>
                        <span>{x.userUserName}</span>
                    </div>
                    <p className={styles.wrapper__items__item__content}>{x.content}</p>
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