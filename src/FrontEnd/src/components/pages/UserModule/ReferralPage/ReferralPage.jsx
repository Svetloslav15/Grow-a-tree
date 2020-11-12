import React from 'react';
import Layout from '../Layout/Layout';
import * as style from './ReferralPage.module.scss';
import QRCode from "react-qr-code";
import InputField from "../../../common/InputField/InputField";
import Button from "../../../common/Button/Button";

const ReferralPage = () => {
    return (
        <Layout>
            <div className='p-5 col-md-5'>
                <p className={style.title}># Покани приятел и получи бонус точки </p>
                <QRCode value='https://github.com/Svetloslav15?tab=repositories'/>
                <div className='pt-2'>
                    <InputField
                        type='text'
                        label='Линк за покана'
                        id='referral-link'
                        value='https://github.com/Svetloslav15?tab=repositories'
                        disabled={true}
                    />
                    <div className={style.buttonsSection}>
                        <Button type='DarkOutline'>Сподели</Button>
                        <Button type='Dark'>Копирай</Button>
                    </div>
                </div>
            </div>
        </Layout>
    )
};

export default ReferralPage;