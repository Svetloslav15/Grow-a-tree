import React, {useState, useEffect} from 'react';
import {useSelector} from 'react-redux';

import * as style from './UserInfoPage.module.scss';
import Icons from '../../../../static/icons';
import SuccessMessages from '../../../../static/successMessages';
import AlertService from "../../../../services/alertService";
import UsersService from '../../../../services/usersService';

import InputField from '../../../common/InputField/InputField';
import Button from '../../../common/Button/Button';
import Layout from '../UserLayout/UserLayout';
import Cities from "../../../../static/cities";
import InputAutoComplete from "../../../common/InputAutoComplete/InputAutoComplete";
import ChangeImage from "./ChangeImage/ChangeImage";

const UserInfoPage = () => {
    const stateUserData = useSelector(state => state.auth);
    const [currUser, setCurrUser] = useState({});

    useEffect(() => {
        UsersService.getAuthorizedUserById(stateUserData.id, stateUserData.accessToken)
            .then((res) => {
                setCurrUser(res.data.data)
            });
    }, [currUser.profilePictureUrl]);

    const handleChange = (event) => {
        currUser[event.target.id] = event.target.value;
        setCurrUser({...currUser});
    };

    const changeProfileImage = (data) => {
        currUser.profilePictureUrl = data;
        setCurrUser({...currUser});
    }

    const handleSubmit = async () => {
        const response = await UsersService.postAuthorizedEditUser(currUser, stateUserData.accessToken);
        if (response.succeeded) {
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
                        <ChangeImage changeProfileImage={changeProfileImage}/>
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