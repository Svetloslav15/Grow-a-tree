import React from 'react';
import * as style from './SocialMediaIcon.module.scss';

const SocialMediaIcon = ({props}) => (
    <a href={props.link} target='_blank'>
        <img className={`${style.icon}`} src={props.src} alt={props.alt}/>
    </a>
);

export default SocialMediaIcon;