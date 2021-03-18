import React, {useEffect, useState} from 'react';
import {useSelector, useDispatch} from 'react-redux';
import usersService from '../../../../services/usersService';
import alertService from '../../../../services/alertService';
import {ADD_USERS} from '../../../../store/actions/actionTypes';
import Layout from "../../../common/Layout/Layout";
import * as styles from './ListUsers.module.scss';
import Button from "../../../common/Button/Button";

const ListUsers = () => {
    const users = useSelector(state => state.users.users);
    const currUser = useSelector(state => state.auth);
    const saveUsersToStore = useDispatch();

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = () => {
        usersService.getAuthorizedUsers('?page=1&perPage=1000', currUser.accessToken)
            .then(data => {
                if (data.data.succeeded) {
                    saveUsersToStore({type: ADD_USERS, payload: data.data.data});
                }
            });
    }

    const handleLockUser = (user) => {
        usersService.postAuthorizedToggleLockoutUser({userId: user.id}, currUser.accessToken)
            .then(data => {
                if (data.succeeded) {
                    fetchData();
                } else {
                    alertService.error(data.errors[0]);
                }
            });
    }

    const handleAdminRights = (user) => {
        usersService.postAuthorizedToggleAdminRights({userId: user.id}, currUser.accessToken)
            .then(data => {
                if (data.succeeded) {
                    fetchData();
                } else {
                    alertService.error(data.errors[0]);
                }
            });
    }

    return (
        <Layout>
            <div className={styles.pageWrapper}>
                <h1># Потребители</h1>
                <table className="table">
                    <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Потребителско име</th>
                        <th scope="col">Имейл</th>
                        <th scope="col">Действия</th>
                    </tr>
                    </thead>
                    <tbody>
                    {
                        users.length && users.map((user, index) =>
                            <tr>
                                <th scope="row">{index + 1}</th>
                                <td className='font-weight-bold'>{user.userName}</td>
                                <td>{user.email}</td>
                                <td>
                                    {
                                        user.lockoutEnabled ?
                                            <Button
                                                type='OutlineGreen'
                                                onClick={() => handleLockUser(user)}
                                            >
                                                Отключи профил
                                            </Button>
                                            :
                                            <Button
                                                type='Green'
                                                onClick={() => handleLockUser(user)}
                                            >
                                                Заключи профил
                                            </Button>
                                    }
                                    {
                                        user.isAdmin ?
                                            <Button
                                                type='Green'
                                                onClick={() => handleAdminRights(user)}
                                            >
                                                Премахни администраторски права
                                            </Button>
                                            :
                                            <Button
                                                type='OutlineGreen'
                                                onClick={() => handleAdminRights(user)}
                                            >
                                                Направи администратор
                                            </Button>
                                    }
                                </td>
                            </tr>)
                    }

                    </tbody>
                </table>
            </div>
        </Layout>
    )
};

export default ListUsers;