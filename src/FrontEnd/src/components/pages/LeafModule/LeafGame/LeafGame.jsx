import React, {useState, useEffect} from 'react';
import {useSelector} from 'react-redux';

import * as styles from './LeafGame.module.scss';
import Layout from '../../../common/Layout/Layout';
import Button from '../../../common/Button/Button';
import TreeService from '../../../../services/treeService';

const LeafGame = () => {
    const [isGameStarted, setGameStarted] = useState(false);
    const [data, setData] = useState([]);
    const currUser = useSelector(state => state.auth);
    const [activeItem, setActiveItem] = useState(1);
    const [activeOptions, setActiveOptions] = useState([]);

    useEffect(() => {
        if (!isGameStarted || (activeItem % 5 === 0)) {
            fetchData();
        }
    }, [activeItem]);

    const fetchData = async () => {
        const response = await TreeService.getAuthorizedGuessGameOptions('', currUser.accessToken);
        console.log(response);
        if (response.data.succeeded) {
            setData(response.data.data);
            setActiveOptions(response.data.data[activeItem - 1].options.split(','));
            console.log(response.data.data);
        }
    }

    const answerQuestion = async (element) => {
        console.log(element.target.textContent);
    }

    return (
        <Layout>
            <div className='col-md-10 mx-auto py-3'>
                <h1 className='text-center my-2'># Играй - познай дървото</h1>
                <p className='py-2 text-center'>
                    Помогни на нашият модел да се обучи по-добре, като отговориш на въпроса:
                    Какъв е вида на дървото на снимката и получи точки за това.
                </p>
                <div className='col-md-12 text-center'>
                    {
                        !isGameStarted ?
                            <Button type='Dark'
                                    onClick={() => setGameStarted(true)}>
                                Започни
                            </Button>
                            :
                            (
                                data.length > 0 &&
                                <div className={`col-md-12 row ${styles.gameWrapper}`}>
                                    <img src={`data:image/png;base64, ${data[activeItem - 1].leafImage}`}
                                         alt={data[activeItem - 1]}
                                         className={`${styles.gameWrapper__image}`}/>
                                    <div className={`${styles.gameWrapper__items}`}>
                                        {
                                            activeOptions && activeOptions.map(x =>
                                                <div className={styles.gameWrapper__items__item}
                                                     onClick={answerQuestion}>
                                                    {x}
                                                </div>)
                                        }
                                    </div>
                                </div>
                            )
                    }
                </div>
            </div>
        </Layout>
    )
};

export default LeafGame;