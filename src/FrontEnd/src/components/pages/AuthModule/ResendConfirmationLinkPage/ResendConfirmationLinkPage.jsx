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
        const result = await AuthService.resendConfirmationLink({email});
        return result.succeeded ? AlertService.success(SuccessMessages.successConfirmEmailLinkSent) : AlertService.error(result.errors[0]);
    };
    return (
        <div className='pt-5 mt-5'>
            <img src={BgShape3} className='shape3'/>
            <img src={BgShape4} className='shape4'/>
            <div className={`col-md-12 ${style.sectionWrapper}`}>
                <div className='col-md-5 my-5 mx-auto'>
                    <h2 className={style.title}># Успешно се регистрирахте</h2>
                    <p className={style.title}>Вашият профил очаква потвърждение. Проверете вашия имейл.</p>
                    <InputField type='email'
                                label={'Имейл'}
                                id='email'
                                icon={Icons.email}
                                width={12}
                                onChange={handleChange}/>
                    <div className='text-center col-md-12'>
                        <p className={style.title}>Не сте получили имейл?</p>
                        <Button type={'DarkOutline'} className={'mb-5'} onClick={handleSubmit}>
                            <i className="fas fa-paper-plane mr-2"/>
                            Изпрати нов линк
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    )
};

export default ResendConfirmationLinkPage;