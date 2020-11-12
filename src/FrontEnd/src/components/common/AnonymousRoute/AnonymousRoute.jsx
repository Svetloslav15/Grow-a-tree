import React, {useEffect, useState} from 'react';
import {Redirect, Route} from 'react-router-dom';
import AlertService from '../../../services/alertService';
import ErrorMessages from '../../../static/errorMessages';
import Cookies from 'js-cookie';
import CookieNames from '../../../static/cookieNames';

const AnonymousRoute = ({component: Component, ...rest}) => {
    const [hasToken, setHasToken] = useState(false);

    useEffect(() => {
        if (Cookies.get(CookieNames.currentUser)) {
            setHasToken(true);
            AlertService.warning(ErrorMessages.anonymousRoute);
        }
    }, []);

    return (
        <Route
            {...rest}
            render={props =>
                !hasToken ? (<Component {...props}/>
                ) : (
                    <Redirect
                        to={{
                            pathname: '/',
                            state: {from: props.location}
                        }}/>
                )
            }
        />
    )
};

export default AnonymousRoute;