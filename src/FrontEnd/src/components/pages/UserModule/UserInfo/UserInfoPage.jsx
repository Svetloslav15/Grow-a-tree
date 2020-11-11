import React, {useState, useEffect} from 'react';
import {useDispatch} from 'react-redux';

import * as style from './UserInfoPage.module.scss';
import Icons from '../../../../static/icons';
import SuccessMessages from "../../../../static/successMessages";
import AuthService from "../../../../services/authService";
import AlertService from "../../../../services/alertService";
import InputField from '../../../common/InputField/InputField';
import Button from '../../../common/Button/Button';
import {CHANGE_IS_USER_NAV_LOCKED, CHANGE_IS_USER_NAV_OPENED} from '../../../../store/actions/actionTypes';
import Sidebar from '../Sidebar/Sidebar';

const BgShape3 = require('../../../../assets/bg-shape-3.png');
const BgShape4 = require('../../../../assets/bg-shape-4.png');
const UserImage = require('../../../../assets/user-profile.png');

const UserInfoPage = () => {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch({type: CHANGE_IS_USER_NAV_LOCKED, data: true});
        return () => {
            dispatch({type: CHANGE_IS_USER_NAV_LOCKED, data: false});
            dispatch({type: CHANGE_IS_USER_NAV_OPENED, data: false});
        }
    }, []);

    return (
        <div className='row'>
            <img src={BgShape3} className='shape3'/>
            <img src={BgShape4} className='shape4'/>
            <Sidebar/>
            <div className={`${style.wrapper} col-md-8`}>
                <div className='col-md-12 row'>
                    <div className='col-md-3'>
                        <img className={style.profileImage} src={UserImage} alt='User Image'/>
                    </div>
                    <div className='col-md-7'>
                        <p className={style.username}>@Svetloslav</p>
                        <p className={style.name}>(Svetloslav Novoselski)</p>
                        <div className='col-md-12 row'>
                            <InputField type='text'
                                        label={'Име'}
                                        id='firstName'
                                        icon={Icons.user}
                                        width={6}
                                        onChange={null}/>
                            <InputField type='text'
                                        label={'Фамилия'}
                                        id='lastName'
                                        icon={Icons.email}
                                        width={6}
                                        onChange={null}/>
                            <InputField type='text'
                                        label={'Град'}
                                        id='town'
                                        icon={Icons.email}
                                        width={6}
                                        onChange={null}/>
                            <InputField type='text'
                                        label={'Телефон'}
                                        id='phoneNumber'
                                        icon={Icons.email}
                                        width={6}
                                        onChange={null}/>
                            <InputField type='text'
                                        label={'Потребителско име'}
                                        id='username'
                                        icon={Icons.email}
                                        width={6}
                                        onChange={null}/>
                            <div className='col-md-6 text-center mt-3'>
                                <Button type='DarkOutline'>Запази</Button>
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
        </div>
    )
};
export default UserInfoPage;