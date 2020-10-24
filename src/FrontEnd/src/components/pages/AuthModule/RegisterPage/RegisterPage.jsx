import React, {useState, useEffect} from 'react';
import { toast } from 'react-toastify';

import * as style from './RegisterPage.module.scss';
import InputField from '../../../common/InputField/InputField';
import Button from '../../../common/Button/Button';
import ExternalLoginSection from './ExternalLoginSection/ExternalLoginSection';
import Icons from '../../../../static/icons';
import InputAutoComplete from "../../../common/InputAutoComplete/InputAutoComplete";
import Cities from '../../../../static/cities';
import ErrorMessages from '../../../../static/errorMessages';
import SuccessMessages from '../../../../static/successMessages';

const BgImage = require('../../../../assets/tree-for-bg.png');
const BgShape1 = require('../../../../assets/bg-shape-1.png');
const BgShape2 = require('../../../../assets/bg-shape-2.png');
const BgShape3 = require('../../../../assets/bg-shape-3.png');

const RegisterPage = () => {
    const [user, setUser] = useState({});

    const handleChange = (event) => {
        user[event.target.id] = event.target.value;
        setUser(user);
    };

    const handleSubmit = () => {
        if (Object.values(user).includes('') || Object.keys(user).length < 5) {
            return toast.error(ErrorMessages.allFieldsAreRequired);
        }
        if (user.password !== user['repeated-password']) {
            return toast.error(ErrorMessages.passwordsShouldMatch);
        }
        else {
            toast.success(SuccessMessages.successSignUp);
        }
    };

    return (
        <React.Fragment>
            <img src={BgShape1} className='shape1'/>
            <img src={BgShape2} className='shape2'/>
            <img src={BgShape3} className='shape3'/>
            <div className={`px-0 mx-0 my-5 row`}>
                <div className={`offset-md-1 col-md-5`}>
                    <h2 className={style.title}>Регистрация</h2>
                    <ExternalLoginSection/>
                    <div className='row'>
                        <InputField type='email'
                                    label={'Имейл'}
                                    id='email'
                                    icon={Icons.email}
                                    onChange={handleChange}/>
                        <InputField type='text'
                                    label={'Потребителско име'}
                                    id='username'
                                    icon={Icons.user}
                                    onChange={handleChange}/>
                    </div>
                    <div className='row'>
                        <InputField type='password'
                                    label={'Парола'}
                                    id='password'
                                    icon={Icons.password}
                                    onChange={handleChange}/>
                        <InputField type='password'
                                    label={'Повторете паролата'}
                                    id='repeated-password'
                                    icon={Icons.password}
                                    onChange={handleChange}/>
                    </div>
                    <div className='row'>
                        <InputAutoComplete label={'Град'}
                                           id='town'
                                           icon={Icons.map}
                                           data={Cities}
                                           onChange={handleChange}/>
                    </div>
                    <div className='text-center'>
                        <Button type='Green' className='w-75' onClick={handleSubmit}>Вход</Button>
                        <Button type='GreenOutline' className='w-75'>Имате акаунт?</Button>
                    </div>
                </div>
                <div className={`col-md-6`}>
                    <img src={BgImage} className={style.imageBg}/>
                </div>
            </div>
        </React.Fragment>
    )
};

export default RegisterPage;