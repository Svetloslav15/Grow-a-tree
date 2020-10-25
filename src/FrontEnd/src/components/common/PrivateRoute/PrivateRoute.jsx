import React from 'react';
import {Redirect, Route} from 'react-router-dom';

//TODO implement strategy for checking if the user is authed
const PrivateRoute = ({component: Component, ...rest}) => (
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
);

export default PrivateRoute;