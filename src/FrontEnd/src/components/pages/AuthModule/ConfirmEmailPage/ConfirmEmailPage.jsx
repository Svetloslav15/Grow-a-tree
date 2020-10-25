import React, {useState, useEffect} from 'react';
import * as style from './ConfirmEmailPage.module.scss';
import Icons from '../../../../static/icons';
import InputField from '../../../common/InputField/InputField';
import Button from '../../../common/Button/Button';

import AuthService from '../../../../services/authService';
import SuccessMessages from "../../../../static/successMessages";
import AlertService from "../../../../services/alertService";

const BgShape3 = require('../../../../assets/bg-shape-3.png');
const BgShape4 = require('../../../../assets/bg-shape-4.png');

const ConfirmEmailPage = (props) => {
    const [token, setToken] = useState(props.location.search.slice(7));
    const [email, setEmail] = useState('');

    const handleChange = (event) => {
        setEmail(event.target.value);
    };

    const handleSubmit = async () => {
        const result = await AuthService.confirmEmail({email, token});
        return result.succeeded ? AlertService.success(SuccessMessages.successConfirmEmail) : AlertService.error(result.errors[0]);
    };

    return (
        <>
            <img src={BgShape3} className='shape3'/>
            <img src={BgShape4} className='shape4'/>
            <div className={`col-md-12 ${style.sectionWrapper}`}>
                <div className='col-md-4 my-5 mx-auto text-center'>
                    <h2 className={style.title}># Потвърди имейл</h2>

                    <InputField type='email'
                                label={'Имейл'}
                                id='email'
                                icon={Icons.email}
                                width={12}
                                onChange={handleChange}/>
                    <Button type={'DarkOutline'} onClick={handleSubmit}>Потвърди</Button>
                </div>
            </div>
        </>
    )
};

export default ConfirmEmailPage;