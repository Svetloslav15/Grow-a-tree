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
import ResetPasswordPage from './components/pages/AuthModule/ResetPasswordPage/ResetPasswordPage';
import RegisterStorePage from "./components/pages/AuthModule/RegisterStorePage/RegisterStorePage";
import UserInfoPage from './components/pages/UserModule/UserInfoPage/UserInfoPage';
import ReferralPage from './components/pages/UserModule/ReferralPage/ReferralPage';
import AnonymousRoute from './components/common/AnonymousRoute/AnonymousRoute';
import AddTreePage from './components/pages/TreeModule/AddTreePage/AddTreePage';
import EditTreePage from './components/pages/TreeModule/EditTreePage/EditTreePage';

const App = () => (
    <>
        <ToastContainer/>
        <Navigation/>
        <Switch>
            <Route exact path='/' component={HomePage}/>
            <AnonymousRoute exact path='/auth/confirm' component={ConfirmEmailPage}/>
            <AnonymousRoute exact path='/auth/register' component={RegisterPage}/>
            <AnonymousRoute exact path='/auth/register/store' component={RegisterStorePage}/>
            <AnonymousRoute exact path='/auth/login' component={LoginPage}/>
            <Route exact path='/auth/reset-password' component={ResetPasswordPage}/>
            <AnonymousRoute exact path='/auth/forgotten-password' component={ForgottenPassword}/>
            <Route exact path='/auth/resend-confirmation-link' component={ResendConfirmationLinkPage}/>
            <PrivateRoute exact path='/users/my-info' component={UserInfoPage}/>
            <PrivateRoute exact path='/users/referral' component={ReferralPage}/>
            <Route exact path='/trees/add' component={AddTreePage}/>
            <Route exact path='/trees/edit/:id' component={EditTreePage}/>
        </Switch>
        <Footer/>
    </>
);

export default App;