import React from 'react';
import * as style from './RegisterPage.module.scss';
import InputField from "../../../common/InputField/InputField";
import Button from "../../../common/Button/Button";
import { toast } from 'react-toastify';
import GoogleLogin from 'react-google-login';

const BgImage = require('../../../../assets/tree-for-bg.png');
const BgShape1 = require('../../../../assets/bg-shape-1.png');
const BgShape2 = require('../../../../assets/bg-shape-2.png');
const BgShape3 = require('../../../../assets/bg-shape-3.png');

const RegisterPage = () => {
    const notify = () => toast.success("Wow so easy !");
    const responseGoogle = (response) => {
        console.log(response);
    };
    return (
        <React.Fragment>
            <img src={BgShape1} className='shape1'/>
            <img src={BgShape2} className='shape2'/>
            <img src={BgShape3} className='shape3'/>
            <div className={`px-0 mx-0 my-5 row`}>
                <div className={`offset-md-1 col-md-5`}>
                    <h2 className={style.title}>Регистрация</h2>
                    <GoogleLogin
                        clientId="284390856965-ldvufmenaouvj65rbhb8d4e004vr85td.apps.googleusercontent.com"
                        buttonText="Sign In with Google"
                        onSuccess={responseGoogle}
                        onFailure={responseGoogle}
                        cookiePolicy={'single_host_origin'}
                    />
                    <div className='row'>
                        <InputField type='text' label={'Имейл'} id='email'/>
                        <InputField type='text' label={'Потребителско име'} id='username'/>
                    </div>
                    <div className='row'>
                        <InputField type='password' label={'Парола'} id='password'/>
                        <InputField type='password' label={'Повторете паролата'} id='repeated-password'/>
                    </div>
                    <div className='text-center'>
                        <Button type='Green' className='w-50' onClick={notify}>Вход</Button>
                        <Button type='GreenOutline' className='w-50'>Имате акаунт?</Button>
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