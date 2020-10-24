import React from 'react';
import 'mdbreact/dist/css/mdb.css';
import 'react-toastify/dist/ReactToastify.css';
import './App.scss';
import {ToastContainer} from 'react-toastify';

import RegisterPage from './components/pages/AuthModule/RegisterPage/RegisterPage';
import Navigation from './components/common/Navigation/Navigation';
import Footer from './components/common/Footer/Footer';

const App = () => (
    <>
        <ToastContainer/>
        <Navigation/>
        <RegisterPage/>
        <Footer/>
    </>
);

export default App;
