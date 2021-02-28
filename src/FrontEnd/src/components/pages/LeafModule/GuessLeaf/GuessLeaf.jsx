import React, {useState, useEffect} from 'react';

import * as styles from './GuessLeaf.module.scss';

import Layout from '../../../common/Layout/Layout';
import Loader from '../../../common/Loader/Loader';
import FileInput from "../../../common/FileInput/FileInput";

const GuessLeaf = () => {
    useEffect(() => {

    }, []);

    const handleFileInput = (data) => {
        console.log(data.target.files);
    }

    return (
        <Layout>
            <div className='col-md-10 mx-auto py-3'>
                <h1 className={`text-center my-2`}># Познай листото</h1>
                <p className='text-center'>
                    Чудиш се какъв е вида на дадено дърво.
                    Постави снимка на листото и ние ще ти помогнем да разбереш.
                </p>

                <FileInput onChange={handleFileInput}
                           isMultiple={false}/>
                <div className='col-md-12 text-center'>
                    <Loader/>
                </div>
            </div>
        </Layout>
    )
};

export default GuessLeaf;