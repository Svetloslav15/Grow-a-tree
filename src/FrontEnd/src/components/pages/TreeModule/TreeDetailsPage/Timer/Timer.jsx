import React from 'react';
import './Timer.scss';

const Timer = () => (
    <div className='timer'>
        <h4 className='timer__title'>Време от последно поливане</h4>
        <div className='timer__section'>
            <div className='timer__section__item'>
                <span className='timer__section__value'>4</span>
                <span className='timer__section__key'>часа</span>
            </div>
            <span className='timer__section__delimiter'>:</span>
            <div className='timer__section__item'>
                <span className='timer__section__value'>30</span>
                <span className='timer__section__key'>минути</span>
            </div>
            <span className='timer__section__delimiter'>:</span>
            <div className='timer__section__item'>
                <span className='timer__section__value'>59</span>
                <span className='timer__section__key'>секунди</span>
            </div>
        </div>

    </div>
);

export default Timer;