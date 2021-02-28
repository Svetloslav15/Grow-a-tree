import React, {useState, useEffect} from 'react';
import {useSelector} from 'react-redux';

import * as styles from './LeafGame.module.scss';
import Layout from '../../../common/Layout/Layout';
import Button from '../../../common/Button/Button';
import TreeService from '../../../../services/treeService';
import AlertService from '../../../../services/alertService';

const LeafGame = () => {
    const [isGameStarted, setGameStarted] = useState(false);
    const [data, setData] = useState([]);
    const currUser = useSelector(state => state.auth);
    const [activeItem, setActiveItem] = useState(1);
    const [activeOptions, setActiveOptions] = useState([]);

    useEffect(() => {
        if (!isGameStarted || (activeItem % 4 === 0)) {
            fetchData();
        }
    }, [activeItem]);

    const fetchData = async () => {
        const response = await TreeService.getAuthorizedGuessGameOptions('', currUser.accessToken);
        if (response.data.succeeded) {
            setData(data.concat(response.data.data));
            setActiveOptions(response.data.data[(activeItem - 1) % 4].options.split(','));
        }
    }

    const answerQuestion = async (element) => {
        const valueItem  = element.target.textContent;
        const currentData = {
            answer: valueItem,
            questionId: data[activeItem - 1].id
        };
        const response = await TreeService.postAuthorizedGuessGameAnswer(currentData, currUser.accessToken);
        if (response.succeeded) {
            await AlertService.success(`Вие отговорихте, че това е листо на ${valueItem}.`);
            setActiveItem(activeItem + 1);
        }
        else {
            await AlertService.error(response.errors[0]);
        }
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