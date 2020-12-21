import React from 'react';
import './TreePost.scss';

const TreePost = ({data}) => (
    <div className='post'>
        <div className='post__user-section'>
            <img className='post__user-section__avatar' src={data.userProfilePictureUrl} alt={data.userUserName}/>
            <p className='post__user-section__username' >{data.userUserName}</p>
        </div>
        <div className='post__content' >
            {data.content}
        </div>
    </div>
);

export default TreePost;