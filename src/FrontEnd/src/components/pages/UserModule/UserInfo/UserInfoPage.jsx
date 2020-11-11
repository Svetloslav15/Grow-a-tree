import React, {useState, useEffect} from 'react';
import {useDispatch} from 'react-redux';

import * as style from './UserInfoPage.module.scss';
import Icons from "../../../../static/icons";
import SuccessMessages from "../../../../static/successMessages";
import AuthService from "../../../../services/authService";
import AlertService from "../../../../services/alertService";
import InputField from '../../../common/InputField/InputField';
import Button from '../../../common/Button/Button';
import {CHANGE_IS_USER_NAV_FIXED, CHANGE_IS_USER_NAV_OPENED} from '../../../../store/actions/actionTypes';

const BgShape3 = require('../../../../assets/bg-shape-3.png');
const BgShape4 = require('../../../../assets/bg-shape-4.png');

const ForgottenPasswordPage = () => {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch({type: CHANGE_IS_USER_NAV_FIXED, data: true});
        return () => {
            dispatch({type: CHANGE_IS_USER_NAV_FIXED, data: false});
            dispatch({type: CHANGE_IS_USER_NAV_OPENED, data: false});
        }
    }, []);

    return (
        <>
            <img src={BgShape3} className='shape3'/>
            <img src={BgShape4} className='shape4'/>
            <div className={`col-md-12`}>

            </div>
        </>
    )
};

export default ForgottenPasswordPage;