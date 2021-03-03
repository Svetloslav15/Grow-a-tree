import React from 'react';
import Loader from "react-loader-spinner";
import 'react-loader-spinner/dist/loader/css/react-spinner-loader.css';

const LoaderComponent  = () => (
    <Loader
        type="Puff"
        color="#002F3E"
        height={100}
        width={100}
    />
);

export default LoaderComponent;