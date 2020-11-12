import React from 'react';
import GoogleLogin from 'react-google-login';
import FacebookLogin from 'react-facebook-login';
import {withRouter} from 'react-router-dom';
import {useDispatch} from 'react-redux';
import Cookies from 'js-cookie';

import AuthService from '../../../../../services/authService';
import * as style from './ExternalLoginSection.module.scss';
import {SAVE_CURRENT_USER} from '../../../../../store/actions/actionTypes';

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
        const res = await AuthService.externalLogin(model);
        dispatch({type: SAVE_CURRENT_USER, data: res.data});
        Cookies.set('gt_curr_user', res.data);
        history.push('/');
    };
    const responseFacebook = async (response) => {
        const model = {
            "providerKey": process.env.REACT_APP_FACEBOOK_ID,
            "providerName": "Facebook",
            "email": response.email,
            "userId": response.id,
            "firstName": response.name.split(' ')[0],
            "lastName": response.name.split(' ')[1],
            "profilePictureUrl": response.picture.data.url
        };
        const res = await AuthService.externalLogin(model);
        dispatch({type: SAVE_CURRENT_USER, data: res.data});
        Cookies.set('gt_curr_user', res.data);
        history.push('/');
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