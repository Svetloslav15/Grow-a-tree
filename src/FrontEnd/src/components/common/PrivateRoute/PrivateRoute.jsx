import React, {useEffect, useState} from 'react';
import {Redirect, Route} from 'react-router-dom';
import AlertService from '../../../services/alertService';
import ErrorMessages from '../../../static/errorMessages';
import Cookies from 'js-cookie';
import CookieNames from '../../../static/cookieNames';

const PrivateRoute = ({component: Component, ...rest}) => {
    const [hasToken, setHasToken] = useState(true);

    useEffect(() => {
        if (!Cookies.get(CookieNames.currentUser)) {
            setHasToken(false);
            AlertService.warning(ErrorMessages.privateRoute);
        }
    }, []);

    return (
        <Route
            {...rest}
            render={props =>
                hasToken ? (<Component {...props}/>
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