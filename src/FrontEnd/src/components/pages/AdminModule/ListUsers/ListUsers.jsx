import React, {useEffect, useState} from 'react';
import {useSelector, useDispatch} from 'react-redux';
import usersService from '../../../../services/usersService';
import {ADD_USERS} from '../../../../store/actions/actionTypes';
import Layout from "../../../common/Layout/Layout";
import * as styles from './ListUsers.module.scss';
import Button from "../../../common/Button/Button";

const ListUsers = () => {
    const users = useSelector(state => state.users.users);
    const currUser = useSelector(state => state.auth);
    const saveUsersToStore = useDispatch();

    useEffect(() => {
        usersService.getAuthorizedUsers('?page=1&perPage=1000', currUser.accessToken)
            .then(data => {
                console.log(data.data);
                if (data.data.succeeded) {
                    saveUsersToStore({type: ADD_USERS, payload: data.data.data});
                }
            });
    }, []);
    console.log(users);

    const handleLockUser = () => {

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
                                            <Button type='OutlineGreen'>Отключи профил</Button>
                                            :
                                            <Button type='Green'>Заключи профил</Button>
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