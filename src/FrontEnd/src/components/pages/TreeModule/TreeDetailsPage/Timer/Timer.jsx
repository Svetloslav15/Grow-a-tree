import React, {useEffect, useState} from 'react';
import './Timer.scss';
import {getDifference} from '../../../../../helpers/getDifferenceBetweenDates';

const Timer = ({data}) => {
    const [formattedTime, setFormattedTime] = useState({});
    const [isFirstFetched, setIsFirstFetched] = useState(false);

    useEffect(() => {

        let timeout;
        if (!isFirstFetched && data) {
            setIsFirstFetched(true);
            timeout = setInterval(calculateTime, 1000);
        }
        return () => {
            clearTimeout(timeout);
        };
    }, [data]);

    const calculateTime = () => {
        if (data) {
            setFormattedTime(getDifference(new Date(data.wateredOn), new Date(), true));
        }
    };

    return (
        <div className='timer'>
            <h4 className='timer__title'>Време от последно поливане</h4>
            <div className='timer__section'>
                <div className='timer__section__item'>
                    <span className='timer__section__value'>{formattedTime && formattedTime.hours}</span>
                    <span className='timer__section__key'>часа</span>
                </div>
                <span className='timer__section__delimiter'>:</span>
                <div className='timer__section__item'>
                    <span className='timer__section__value'>{formattedTime && formattedTime.minutes}</span>
                    <span className='timer__section__key'>минути</span>
                </div>
                <span className='timer__section__delimiter'>:</span>
                <div className='timer__section__item'>
                    <span className='timer__section__value'>{formattedTime && formattedTime.seconds}</span>
                    <span className='timer__section__key'>секунди</span>
                </div>
            </div>

        </div>
    );
}

export default Timer;