import React from 'react';
import GoogleLogin from 'react-google-login';
import FacebookLogin from 'react-facebook-login';
import {withRouter} from 'react-router-dom';
import {useDispatch} from 'react-redux';
import Cookies from 'js-cookie';

import AuthService from '../../../../../services/authService';
import AlertService from '../../../../../services/alertService';
import * as style from './ExternalLoginSection.module.scss';
import {SAVE_CURRENT_USER} from '../../../../../store/actions/actionTypes';
import CookieNames from '../../../../../static/cookieNames';

const ExternalLoginSection = ({history}) => {
    const dispatch = useDispatch();

    const responseGoogle = async (response) => {
        const model = {
            "providerKey": process.env.REACT_APP_GOOGLE_PROVIDER_ID,
            "providerName": "Google",
            "email": response.profileObj.email,
            "userId": response.profileObj.googleId,
            "firstName": response.profileObj.givenName,
            "lastName": response.profileObj.familyName,
            "profilePictureUrl": response.profileObj.imageUrl
        };
        await processData(model);
    };
    const responseFacebook = async (response) => {
        const model = {
            "providerKey": process.env.REACT_APP_FACEBOOK_ID,
            "providerName": "Facebook",
            "email": response.email,
            "userId": response.id,
            "firstName": response.name.split(' ')[0],
            "lastName": response.name.split(' ')[1],
            "profilePictureUrl": `https://graph.facebook.com/${response.id}/picture?width=500&height=500&access_token=${response.accessToken}`
        };
        await processData(model);
    };

    const processData = async (model) => {
        const res = await AuthService.externalLogin(model);
        if (res.errors.length > 0) {
            AlertService.error(res.errors[0]);
        }
        else {
            Cookies.set(CookieNames.currentUser, res.data);
            dispatch({type: SAVE_CURRENT_USER, data: res.data});
            history.push('/');
        }
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

export default withRouter(ExternalLoginSection);