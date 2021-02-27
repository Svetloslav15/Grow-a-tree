import React, {useEffect, useState} from 'react';
import {FacebookShareButton, FacebookIcon,
    TwitterIcon, TwitterShareButton,
    LinkedinShareButton, RedditShareButton,
    LinkedinIcon, RedditIcon, ViberShareButton, ViberIcon} from 'react-share';
import {useSelector} from 'react-redux';
import Layout from '../UserLayout/UserLayout';
import * as style from './ReferralPage.module.scss';
import QRCode from "react-qr-code";
import InputField from "../../../common/InputField/InputField";
import Button from "../../../common/Button/Button";
import AlertService from '../../../../services/alertService';

const ReferralPage = () => {
    const currUser = useSelector(state => state.auth);
    const [referralLink, setReferralLink] = useState('');

    useEffect(() => {
        setReferralLink(`${process.env.REACT_APP_CLIENT_BASE_URL}auth/register?referral=${currUser.id}`)
    }, [currUser]);

    const copyToClipboard = () => {
        navigator.clipboard.writeText(referralLink);
        AlertService.success('Копирано');
    };

    return (
        <Layout>
            <div className='p-5 col-md-5'>
                <p className={style.title}># Покани приятел и получи бонус точки </p>
                <QRCode value={referralLink}/>
                <div className='pt-2'>
                    <InputField
                        type='text'
                        label='Линк за покана'
                        id='referral-link'
                        value={referralLink}
                        disabled={true}
                    />
                    <div className={style.buttonsSection}>
                        <FacebookShareButton
                            url={referralLink}
                            className="m-2">
                            <FacebookIcon size={32} round={true} />
                        </FacebookShareButton>
                        <ViberShareButton
                            url={referralLink}
                            className="m-2">
                            <ViberIcon size={32} round={true} />
                        </ViberShareButton>
                        <TwitterShareButton
                            url={referralLink}
                            className="m-2">
                            <TwitterIcon size={32} round={true} />
                        </TwitterShareButton>
                        <LinkedinShareButton
                            url={referralLink}
                            className="m-2">
                            <LinkedinIcon size={32} round={true} />
                        </LinkedinShareButton>
                        <RedditShareButton
                            url={referralLink}
                            className="m-2">
                            <RedditIcon size={32} round={true} />
                        </RedditShareButton>
                        <Button type='Dark' onClick={copyToClipboard}>Копирай</Button>
                    </div>
                </div>
            </div>
        </Layout>
    )
};

export default ReferralPage;