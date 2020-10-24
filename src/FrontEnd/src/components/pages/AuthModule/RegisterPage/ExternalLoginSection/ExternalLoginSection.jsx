import React from 'react';
import GoogleLogin from 'react-google-login';
import FacebookLogin from 'react-facebook-login';

import * as style from './ExternalLoginSection.module.scss';

const ExternalLoginSection = () => {
    const responseGoogle = (response) => {
        console.log(response);
    };
    const responseFacebook = (response) => {
        console.log(response);
    };

    return (
        <div className={style.wrapper}>
            <GoogleLogin
                clientId={process.env.REACT_APP_GOOGLE_ID}
                buttonText="Sign In with Google"
                onSuccess={responseGoogle}
                onFailure={responseGoogle}
                cookiePolicy={'single_host_origin'}
                className={style.googleButton}
            />
            <FacebookLogin
                appId={process.env.REACT_APP_FACEBOOK_ID}
                autoLoad
                icon="fa-facebook"
                cssClass={style.fbButton}
                callback={responseFacebook}/>
        </div>
    )
};

export default ExternalLoginSection;