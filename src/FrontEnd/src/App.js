import React from 'react';
import {ToastContainer} from 'react-toastify';
import {Route, Switch} from 'react-router-dom';

import 'mdbreact/dist/css/mdb.css';
import 'react-toastify/dist/ReactToastify.css';
import './App.scss';

import RegisterPage from './components/pages/AuthModule/RegisterPage/RegisterPage';
import Navigation from './components/common/Navigation/Navigation';
import Footer from './components/common/Footer/Footer';
import ConfirmEmailPage from "./components/pages/AuthModule/ConfirmEmailPage/ConfirmEmailPage";

const App = () => (
    <>
        <ToastContainer/>
        <Navigation/>
        <Switch>
            <Route exact path='/' component={ConfirmEmailPage}/>
            <Route exact path='/auth/register' component={RegisterPage}/>
        </Switch>
        <Footer/>
    </>
);

export default App;
