import React from 'react';
import GoogleLogin from 'react-google-login';
import FacebookLogin from 'react-facebook-login';
import AuthService from '../../../../../services/authService';

import * as style from './ExternalLoginSection.module.scss';

const ExternalLoginSection = () => {

    const responseGoogle = async (response) => {
        const model = {
            "providerKey": process.env.REACT_APP_GOOGLE_PROVIDER_ID,
            "providerName": "Google",
            "email": response.profileObj.email,
            "firstName": response.profileObj.givenName,
            "lastName": response.profileObj.familyName,
            "profilePictureUrl": response.profileObj.imageUrl
        };
        await AuthService.externalLogin(model);
    };
    const responseFacebook = async (response) => {
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
                icon="fa-facebook"
                fields="name,email,picture"
                cssClass={style.fbButton}
                callback={responseFacebook}/>
        </div>
    )
};

export default ExternalLoginSection;