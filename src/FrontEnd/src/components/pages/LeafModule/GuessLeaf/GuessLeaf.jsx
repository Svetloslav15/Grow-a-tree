import React, {useState, useEffect} from 'react';
import * as styles from './GuessLeaf.module.scss';

import Layout from '../../../common/Layout/Layout';
import Loader from '../../../common/Loader/Loader';
import FileInput from '../../../common/FileInput/FileInput';
import TreeService from '../../../../services/treeService';
import AlertService from '../../../../services/alertService';
import ContentTypes from "../../../../static/contentTypes";

const GuessLeaf = () => {
    const [isProcessingOrder, toggleProcessingOrder] = useState(false);
    const [result, setResult] = useState(null);

    useEffect(() => {

    }, []);

    const handleFileInput = async (data) => {
        toggleProcessingOrder(true);
        const formData = new FormData();
        formData.append('Image', data.target.files[0]);
        const response = await TreeService.postFormPredictTreeLeaf(formData, ContentTypes.FormData);
        console.log(response);
        if (!response.succeeded) {
            await AlertService.error(response.errors[0]);
            setResult('');
        }
        else {
            setResult(response.data.treeName);
        }
        toggleProcessingOrder(false);
    }

    return (
        <Layout>
            <div className='col-md-10 mx-auto py-3'>
                <h1 className={`text-center my-2`}># Познай листото</h1>
                <p className='text-center'>
                    Чудиш се какъв е вида на дадено дърво.
                    Постави снимка на листото и ние ще ти помогнем да разбереш.
                </p>
                {
                    result &&
                    <p className='text-center my-2'>
                        Листото, което ни предоставихте е на <span className='font-weight-bold h5'>{result}</span>.
                    </p>
                }

                {
                    isProcessingOrder ?
                        <div className={`${styles.loaderContainer} col-md-12 text-center`}>
                            <Loader/>
                        </div>
                        :
                        <FileInput onChange={handleFileInput}
                                   isMultiple={false}/>
                }
            </div>
        </Layout>
    )
};

export default GuessLeaf;