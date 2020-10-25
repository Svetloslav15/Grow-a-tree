import React, {useEffect} from 'react';
import {Redirect, Route} from 'react-router-dom';
import AlertService from '../../../services/alertService';
import ErrorMessages from '../../../static/errorMessages';

//TODO implement strategy for checking if the user is authenticated
const PrivateRoute = ({component: Component, ...rest}) => {
    useEffect(() => {
        if (!localStorage.getItem('authToken')) {
            AlertService.warning(ErrorMessages.privateRoute)
        }
    }, []);

    return (
        <Route
            {...rest}
            render={props =>
                localStorage.getItem('authToken') ? (<Component {...props}/>
                ) : (
                    <Redirect
                        to={{
                            pathname: '/auth/login',
                            state: {from: props.location}
                        }}/>
                )
            }
        />
    )
};

export default PrivateRoute;