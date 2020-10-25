import React, {useState, useEffect} from 'react';
import {Link, withRouter} from 'react-router-dom';
import * as style from './LoginPage.module.scss';
import InputField from '../../../common/InputField/InputField';
import Button from '../../../common/Button/Button';
import ExternalLoginSection from '../RegisterPage/ExternalLoginSection/ExternalLoginSection';
import Icons from '../../../../static/icons';
import InputAutoComplete from "../../../common/InputAutoComplete/InputAutoComplete";
import Cities from '../../../../static/cities';
import ErrorMessages from '../../../../static/errorMessages';
import SuccessMessages from '../../../../static/successMessages';
import AuthService from '../../../../services/authService';
import AlertService from '../../../../services/alertService';

const BgImage = require('../../../../assets/tree-for-bg2.png');
const BgShape1 = require('../../../../assets/bg-shape-1.png');
const BgShape2 = require('../../../../assets/bg-shape-2.png');
const BgShape3 = require('../../../../assets/bg-shape-3.png');

const LoginPage = ({history}) => {
    const [user, setUser] = useState({});

    const handleChange = (event) => {
        user[event.target.id] = event.target.value;
        setUser(user);
    };

    const handleSubmit = async () => {
        if (Object.values(user).includes('') || Object.keys(user).length < 2) {
            return AlertService.error(ErrorMessages.allFieldsAreRequired);
        }
        const result = await AuthService.login(user);
        console.log(result);
        if (result.succeeded) {
            AlertService.success(SuccessMessages.successLogin);
            history.push('/');
        }
        else {
            AlertService.error(result.errors[0]);
        }
    };

    return (
        <>
            <img src={BgShape1} className='shape1'/>
            <img src={BgShape2} className='shape2'/>
            <img src={BgShape3} className='shape3'/>
            <div className='px-0 mx-0 my-5 row'>
                <div className={`col-md-6`}>
                    <img src={BgImage} className={style.imageBg}/>
                </div>
                <div className='col-md-5'>
                    <h2 className={style.title}>Вход</h2>
                    <ExternalLoginSection/>
                    <div className='row'>
                        <InputField type='email'
                                    label={'Имейл'}
                                    id='email'
                                    icon={Icons.email}
                                    width={12}
                                    onChange={handleChange}/>
                    </div>
                    <div className='row'>
                        <InputField type='password'
                                    label={'Парола'}
                                    id='password'
                                    icon={Icons.password}
                                    width={12}
                                    onChange={handleChange}/>
                    </div>
                    <div className='text-center'>
                        <Link to='/auth/forgotten-password'><span
                            className={'dark-text'}>Забравена парола?</span></Link>
                        <Button type='Green' className='w-75' onClick={handleSubmit}>Вход</Button>
                        <Link to='/auth/register'>
                            <Button type='GreenOutline' className='w-75'>Нямате акаунт?</Button>
                        </Link>
                    </div>
                </div>

            </div>
        </>
    )
};

export default withRouter(LoginPage);