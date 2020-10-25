import React, {useState} from 'react';
import * as style from './ConfirmEmailPage.module.scss';
import Icons from "../../../../static/icons";
import InputField from '../../../common/InputField/InputField';
import Button from "../../../common/Button/Button";

const BgShape3 = require('../../../../assets/bg-shape-3.png');
const BgShape4 = require('../../../../assets/bg-shape-4.png');

const ConfirmEmailPage = () => {

    const handleChange = () => {

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
                    <Button type={'DarkOutline'}>Потвърди</Button>
                </div>
            </div>
        </>
    )
};

export default ConfirmEmailPage;