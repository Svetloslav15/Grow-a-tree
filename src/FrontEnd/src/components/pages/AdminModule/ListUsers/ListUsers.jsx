import React, {useEffect, useState} from 'react';
import {useSelector, useDispatch} from 'react-redux';
import usersService from '../../../../services/usersService';
import {ADD_USERS} from "../../../../store/actions/actionTypes";

const ListUsers = () => {
    const users = useSelector(state => state.users.users);
    const currUser = useSelector(state => state.auth);
    const saveUsersToStore = useDispatch();
    console.log(users);
    useEffect(() => {
        usersService.getAuthorizedUsers('?page=1&perPage=1000', currUser.accessToken)
            .then(data => {
                if (data.data.succeeded) {
                    saveUsersToStore({type: ADD_USERS, payload: data.data.data});
                }
            });
    }, []);

    return (
        <div>
            {users.length && users.map(x => <div>{x.userName}</div>)}
        </div>
    )
};

export default ListUsers;