import React, {useState} from 'react';
import styles from './RepliesSection.module.scss';
import ReactionButton from "../../../../common/ReactionButton/ReactionButton";

const DefaultUserImage = require('../../../../../assets/user-profile.png');

const RepliesSection = () => {
    const [replies, setReplies] = useState([]);

    const reactToReply = () => {

    }

    return (
        <div className={styles.wrapper}>
            <ul className={styles.wrapper__items}>
                <li className={styles.wrapper__items__item}>
                    <div className={styles.wrapper__items__item__userWrapper}>
                        <img src={DefaultUserImage} alt=""/>
                        <span>Svetli Novoselski</span>
                    </div>
                    <p className={styles.wrapper__items__item__content}>Lorem ipsum dolor sit amet, consectetur
                        adipisicing elit. Ad asperiores aspernatur, corporis esse laborum maxime, neque quasi qui,
                        ratione sapiente tempora temporibus vel vitae. Assumenda excepturi facilis iste magnam
                        porro!</p>
                    <ReactionButton hasBorder={false}
                                    reactionsVisible={true}
                                    hasCustomButton={true}
                                    reactTo={reactToReply}
                                    item={{reactions: []}}>
                        <i className={`${styles.fontAwesomeIcon} far fa-heart`}/>
                    </ReactionButton>
                </li>
            </ul>
        </div>
    )
};

export default RepliesSection;