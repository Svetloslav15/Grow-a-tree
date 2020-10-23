import React from 'react';
import 'mdbreact/dist/css/mdb.css';
import './App.scss';

import RegisterPage from './components/pages/AuthModule/RegisterPage/RegisterPage';
import Navigation from './components/common/Navigation/Navigation';
import Footer from './components/common/Footer/Footer';

const App = () => (
    <div>
        <Navigation/>
        <RegisterPage/>
        <Footer/>
    </div>
);

export default App;
