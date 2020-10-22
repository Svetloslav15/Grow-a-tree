import React from 'react';
import {Link} from 'react-router-dom';
import * as style from './RegisterPage.module.scss';
import InputField from "../../../common/InputField/InputField";
import Button from "../../../common/Button/Button";
const BgImage = require('../../../../assets/tree-for-bg.png');
const BgShape1 = require('../../../../assets/bg-shape-1.png');
const BgShape2 = require('../../../../assets/bg-shape-2.png');
const BgShape3 = require('../../../../assets/bg-shape-3.png');

const RegisterPage = () => (
    <React.Fragment>
        <img src={BgShape1} className='shape1'/>
        <img src={BgShape2} className='shape2'/>
        <img src={BgShape3} className='shape3'/>
        <div className={`px-0 mx-0 my-3 row`}>
            <div className={`offset-md-1 col-md-5`}>
                <h2 className={style.title}>Регистрация</h2>
                <div className='row'>
                    <InputField type='text' label={'Имейл'} id='email'/>
                    <InputField type='text' label={'Потребителско име'}/>
                </div>
                <div className='row'>
                    <InputField type='password' label={'Парола'}/>
                    <InputField type='password' label={'Повторете паролата'}/>
                </div>
                <div className='text-center'>
                    <Button type='Green' className='w-75'>Вход</Button>
                    <Button type='GreenOutline' className='w-75'>Имате акаунт?</Button>
                </div>
            </div>
            <div className={`col-md-6`}>
                <img src={BgImage} className={style.imageBg}/>
            </div>
        </div>
    </React.Fragment>
);

export default RegisterPage;