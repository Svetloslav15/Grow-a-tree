import React, {useState} from 'react';
import * as style from './ResendConfirmationLinkPage.module.scss';
import Icons from "../../../../static/icons";
import SuccessMessages from "../../../../static/successMessages";
import AuthService from "../../../../services/authService";
import AlertService from "../../../../services/alertService";
import InputField from '../../../common/InputField/InputField';
import Button from '../../../common/Button/Button';

const BgShape3 = require('../../../../assets/bg-shape-3.png');
const BgShape4 = require('../../../../assets/bg-shape-4.png');

const ResendConfirmationLinkPage = () => {
    const [email, setEmail] = useState('');

    const handleChange = (event) => {
        setEmail(event.target.value);
    };

    const handleSubmit = async () => {
        const result = await AuthService.forgottenPassword({email});
        return result.succeeded ? AlertService.success(SuccessMessages.successSendLinkForForgottenPassword) : AlertService.error(result.errors[0]);
    };
    return (
        <>
            <img src={BgShape3} className='shape3'/>
            <img src={BgShape4} className='shape4'/>
            <div className={`col-md-12 ${style.sectionWrapper}`}>
                <div className='col-md-6 my-5 mx-auto text-center'>
                    <h2 className={style.title}># Успешно се регистрирахте</h2>
                    <p className={style.title}>Вашият профил очаква удобрение. Проверете вашия имейл</p>
                    <InputField type='email'
                                label={'Имейл'}
                                id='email'
                                icon={Icons.email}
                                width={12}
                                onChange={handleChange}/>
                    <p className={style.title}>Не сте получили имейл?</p>
                    <Button type={'DarkOutline'} className={'mb-5'} onClick={handleSubmit}>
                        <i class="fas fa-paper-plane mr-2"/>
                        Изпрати нов линк
                    </Button>
                </div>
            </div>
        </>
    )
};

export default ResendConfirmationLinkPage;