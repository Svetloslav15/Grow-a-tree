import React from 'react';
import 'mdbreact/dist/css/mdb.css';
import './App.scss';

import HomePage from './components/pages/HomePage/HomePage';
import Footer from './components/common/Footer/Footer';

const App = () => (
    <div>
        <HomePage/>
        <Footer/>
    </div>
);

export default App;
