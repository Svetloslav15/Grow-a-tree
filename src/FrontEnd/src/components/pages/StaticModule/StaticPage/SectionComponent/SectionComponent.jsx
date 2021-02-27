import React from 'react';
import * as styles from './SectionComponent.module.scss';

const SectionComponent = ({title, content, image, position}) => (
    <div className={`${styles.wrapper} px-0 col-md-12 row`}>
        {image && position === 'left' && <img className={`${styles.image} col-md-6`} src={`/images/${image}`} alt={title}/>}
        <div className={image ? 'px-0 col-md-6' : 'px-0 col-md-12'}>
            <h2 className={styles.title}>{title}</h2>
            <p className={styles.content}>{content}</p>
        </div>
        {image && position === 'right' && <img className={`${styles.image} col-md-6`} src={`/images/${image}`} alt={title}/>}
    </div>
)

export default SectionComponent;