import React, {useState} from 'react';
import {Link, withRouter} from 'react-router-dom';

import * as style from './RegisterPage.module.scss';
import InputField from '../../../common/InputField/InputField';
import Button from '../../../common/Button/Button';
import ExternalLoginSection from './ExternalLoginSection/ExternalLoginSection';
import Icons from '../../../../static/icons';
import InputAutoComplete from "../../../common/InputAutoComplete/InputAutoComplete";
import Cities from '../../../../static/cities';
import ErrorMessages from '../../../../static/errorMessages';
import SuccessMessages from '../../../../static/successMessages';
import AuthService from '../../../../services/authService';
import AlertService from '../../../../services/alertService';

const BgImage = require('../../../../assets/tree-for-bg.png');
const BgShape1 = require('../../../../assets/bg-shape-1.png');
const BgShape2 = require('../../../../assets/bg-shape-2.png');
const BgShape3 = require('../../../../assets/bg-shape-3.png');

const RegisterPage = ({history}) => {
    const [user, setUser] = useState({});

    const handleChange = (event) => {
        let copyData = user;
        copyData[event.target.id] = event.target.value;
        setUser(copyData);
    };

    const handleSubmit = async () => {
        if (Object.values(user).includes('') || Object.keys(user).length < 5) {
            return AlertService.error(ErrorMessages.allFieldsAreRequired);
        }
        if (user.password !== user['repeated-password']) {
            return AlertService.error(ErrorMessages.passwordsShouldMatch);
        }
        const result = await AuthService.signUp(user);

        if (result.succeeded) {
            AlertService.success(SuccessMessages.successSignUp);
            history.push('/auth/resend-confirmation-link');
        }
        else {
            AlertService.error(result.errors[0]);
        }
    };

    return (
        <div className='pt-5 mt-5'>
            <img src={BgShape1} className='shape1'/>
            <img src={BgShape2} className='shape2'/>
            <img src={BgShape3} className='shape3'/>
            <div className='px-0 mx-0 my-5 row'>
                <div className='offset-md-1 col-md-5'>
                    <h2 className={`${style.title} row`}>
                        <span className='mx-auto mb-3'>Регистрация</span>
                        <Link to='/auth/register/store'>
                            <Button type='DarkOutline' className='w-75 ml-5'>Регистрация като магазин</Button>
                        </Link>
                    </h2>
                    <ExternalLoginSection/>
                    <div className='row'>
                        <InputField type='email'
                                    label={'Имейл'}
                                    id='email'
                                    icon={Icons.email}
                                    width={6}
                                    onChange={handleChange}/>
                        <InputField type='text'
                                    label={'Потребителско име'}
                                    id='username'
                                    icon={Icons.user}
                                    width={6}
                                    onChange={handleChange}/>
                    </div>
                    <div className='row'>
                        <InputField type='password'
                                    label={'Парола'}
                                    id='password'
                                    icon={Icons.password}
                                    width={6}
                                    onChange={handleChange}/>
                        <InputField type='password'
                                    label={'Повторете паролата'}
                                    id='repeated-password'
                                    icon={Icons.password}
                                    onChange={handleChange}/>
                    </div>
                    <div className='row'>
                        <InputAutoComplete label={'Град'}
                                           id='city'
                                           icon={Icons.map}
                                           data={Cities}
                                           width={12}
                                           onChange={handleChange}/>
                    </div>
                    <div className='text-center'>
                        <Link to='/auth/forgotten-password'><span
                            className={'dark-text'}>Забравена парола?</span></Link>
                        <Button type='Green' className='w-75' onClick={handleSubmit}>Регистрация</Button>
                        <Link to='/auth/login'>
                            <Button type='GreenOutline' className='w-75'>Имате акаунт?</Button>
                        </Link>
                    </div>
                </div>
                <div className={`col-md-6`}>
                    <img src={BgImage} className={style.imageBg}/>
                </div>
            </div>
        </div>
    )
};

export default withRouter(RegisterPage);