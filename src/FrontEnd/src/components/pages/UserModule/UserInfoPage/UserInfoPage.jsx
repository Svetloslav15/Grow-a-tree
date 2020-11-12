import React, {useState, useEffect} from 'react';
import {useDispatch, useSelector} from 'react-redux';
import Cookies from 'js-cookie';

import * as style from './UserInfoPage.module.scss';
import Icons from '../../../../static/icons';
import SuccessMessages from '../../../../static/successMessages';
import AlertService from "../../../../services/alertService";
import UsersService from '../../../../services/usersService';
import CookieNames from '../../../../static/cookieNames';

import InputField from '../../../common/InputField/InputField';
import Button from '../../../common/Button/Button';
import {CHANGE_IS_USER_NAV_LOCKED, CHANGE_IS_USER_NAV_OPENED} from '../../../../store/actions/actionTypes';
import Sidebar from '../Sidebar/Sidebar';
import Layout from '../Layout/Layout';
import Cities from "../../../../static/cities";
import InputAutoComplete from "../../../common/InputAutoComplete/InputAutoComplete";

const BgShape3 = require('../../../../assets/bg-shape-3.png');
const BgShape4 = require('../../../../assets/bg-shape-4.png');

const UserInfoPage = () => {
    const dispatch = useDispatch();
    const stateUserData = useSelector(state => state.auth);
    const [currUser, setCurrUser] = useState({});

    useEffect(() => {
        dispatch({type: CHANGE_IS_USER_NAV_LOCKED, data: true});
        UsersService.getAuthorized.getUserById(stateUserData.id, stateUserData.accessToken)
            .then((res) => {
                setCurrUser(res.data.data)
            });
        return () => {
            dispatch({type: CHANGE_IS_USER_NAV_LOCKED, data: false});
            dispatch({type: CHANGE_IS_USER_NAV_OPENED, data: false});
        }
    }, []);

    const handleChange = (event) => {
        currUser[event.target.id] = event.target.value;
        setCurrUser({...currUser});
    };

    const handleSubmit = async () => {
        const response = await UsersService.postAuthorized.editUser(currUser, stateUserData.accessToken);

        if (response.data.succeeded) {
            AlertService.success(SuccessMessages.successEditYourInfo);
        }
        else {
            AlertService.error(response.data.errors[0]);
        }
    };

    return (
        <Layout>
            <div className={`${style.wrapper} col-md-9`}>
                <div className='col-md-12 row'>
                    <div className='col-md-3'>
                        <img className={style.profileImage} src={currUser.profilePictureUrl} alt={currUser.userName}/>
                    </div>
                    <div className='col-md-7'>
                        <p className={style.username}>@{currUser.userName}</p>
                        <p className={style.name}>({currUser.firstName} {currUser.lastName})</p>
                        <div className='col-md-12 row'>
                            <InputField type='text'
                                        label={'Име'}
                                        id='firstName'
                                        icon={Icons.user}
                                        width={6}
                                        value={currUser.firstName}
                                        onChange={handleChange}/>
                            <InputField type='text'
                                        label={'Фамилия'}
                                        id='lastName'
                                        icon={Icons.user}
                                        width={6}
                                        value={currUser.lastName}
                                        onChange={handleChange}/>
                            <InputAutoComplete label={'Град'}
                                               id='city'
                                               icon={Icons.map}
                                               data={Cities}
                                               width={6}
                                               value={currUser.city}
                                               onChange={handleChange}/>
                            <InputField type='text'
                                        label={'Телефон'}
                                        id='phoneNumber'
                                        icon={Icons.user}
                                        width={6}
                                        value={currUser.phoneNumber}
                                        onChange={handleChange}/>
                            <InputField type='text'
                                        label={'Потребителско име'}
                                        id='userName'
                                        icon={Icons.user}
                                        value={currUser.userName}
                                        width={6}
                                        onChange={handleChange}/>
                            <div className='col-md-6 text-center mt-3'>
                                <Button type='DarkOutline'
                                        onClick={handleSubmit}>
                                    Запази
                                </Button>
                            </div>
                        </div>
                    </div>
                    <div className='col-md-2'>
                        <p className={style.points}>Ниво: 5</p>
                        <p className={style.points}>Точки: 9646</p>
                    </div>
                </div>
                <div></div>
            </div>
        </Layout>
    )
};
export default UserInfoPage;