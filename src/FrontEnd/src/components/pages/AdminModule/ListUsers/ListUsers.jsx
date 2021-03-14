import React, {useEffect, useState} from 'react';
import {useSelector} from 'react-redux';

const ListUsers = () => {
    const users = useSelector(state => state.users);

    return (
        <div>
            List users
        </div>
    )
};

export default ListUsers;