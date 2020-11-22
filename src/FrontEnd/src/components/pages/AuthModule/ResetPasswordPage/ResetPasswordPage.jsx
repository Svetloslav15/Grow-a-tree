import React, {useState, useEffect} from 'react';
import * as style from './ResetPasswordPage.module.scss';
import Icons from '../../../../static/icons';
import InputField from '../../../common/InputField/InputField';
import Button from '../../../common/Button/Button';

import AuthService from '../../../../services/authService';
import SuccessMessages from "../../../../static/successMessages";
import AlertService from "../../../../services/alertService";
import {useSelector} from "react-redux";

const BgShape3 = require('../../../../assets/bg-shape-3.png');
const BgShape4 = require('../../../../assets/bg-shape-4.png');

const ResetPasswordPage = ({location}) => {
    const [token, setToken] = useState(location.search.slice(7));
    const [data, setData] = useState({});
    const user = useSelector(state => state.auth);
    const handleChange = (event) => {
        data[event.target.id] = event.target.value;
        setData(data);
    };

    const handleSubmit = async () => {
        const result = await AuthService.postAuthorizedResetPassword(data, user.accessToken);
        return result.succeeded ? AlertService.success(SuccessMessages.successResetPassword) : AlertService.error(result.errors[0]);
    };

    return (
        <>
            <img src={BgShape3} className='shape3'/>
            <img src={BgShape4} className='shape4'/>
            <div className={`col-md-12 ${style.sectionWrapper}`}>
                <div className='col-md-4 my-5 mx-auto text-center'>
                    <h2 className={style.title}># Смени парола</h2>
                        <InputField type='email'
                                    label={'Имейл'}
                                    id='email'
                                    width={12}
                                    onChange={handleChange}/>
                        <InputField type='password'
                                    label={'Нова парола'}
                                    id='password'
                                    width={12}
                                    onChange={handleChange}/>
                    <Button type={'DarkOutline'} className={'mb-5'} onClick={handleSubmit}>Смени</Button>
                </div>
            </div>
        </>
    )
};

export default ResetPasswordPage;