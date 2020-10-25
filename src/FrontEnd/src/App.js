import React from 'react';
import {ToastContainer} from 'react-toastify';
import {Route, Switch} from 'react-router-dom';

import 'mdbreact/dist/css/mdb.css';
import 'react-toastify/dist/ReactToastify.css';
import './App.scss';

import RegisterPage from './components/pages/AuthModule/RegisterPage/RegisterPage';
import Navigation from './components/common/Navigation/Navigation';
import Footer from './components/common/Footer/Footer';
import ConfirmEmailPage from './components/pages/AuthModule/ConfirmEmailPage/ConfirmEmailPage';
import PrivateRoute from "./components/common/PrivateRoute/PrivateRoute";
import ForgottenPassword from './components/pages/AuthModule/ForgottenPasswordPage/ForgottenPasswordPage';
import ResendConfirmationLinkPage from './components/pages/AuthModule/ResendConfirmationLinkPage/ResendConfirmationLinkPage';
import HomePage from './components/pages/HomeModule/HomePage/HomePage';
import LoginPage from './components/pages/AuthModule/LoginPage/LoginPage';

const App = () => (
    <>
        <ToastContainer/>
        <Navigation/>
        <Switch>
            <Route exact path='/' component={HomePage}/>
            <Route exact path='/auth/confirm' component={ConfirmEmailPage}/>
            <Route exact path='/auth/register' component={RegisterPage}/>
            <Route exact path='/auth/login' component={LoginPage}/>
            <Route exact path='/auth/forgotten-password' component={ForgottenPassword}/>
            <Route exact path='/auth/resend-confirmation-link' component={ResendConfirmationLinkPage}/>
        </Switch>
        <Footer/>
    </>
);

export default App;
