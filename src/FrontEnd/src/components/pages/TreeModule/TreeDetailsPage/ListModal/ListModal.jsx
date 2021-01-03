import React, {useEffect, useState} from 'react';
import './ListModal.scss';
import {getDifference} from '../../../../../helpers/getDifferenceBetweenDates';

const DefaultProfilePicture = require('../../../../../assets/user-profile.png');
const BgShapeTwo = require('../../../../../assets/bg-shape-2.png');
const BgShapeThree = require('../../../../../assets/bg-shape-3.png');

const ListModal = ({data, closeModal}) => {
    return (
        <div className='wrapper'>
            <div className='overlay-bg' onClick={closeModal}></div>
            <div className='wrapper__modal'>
                <img className='bg-shape-image' src={BgShapeTwo} alt="Background Shape Image"/>
                <img className='bg-shape-image' src={BgShapeThree} alt="Background Shape Image"/>
                <i className="fas fa-times close-button" onClick={closeModal}></i>

                <p className='wrapper__modal__title'>Последни поливания</p>
                <ul className='wrapper__modal__items'>
                    {
                        data.map(x => (
                            <li className='wrapper__modal__items__item'>
                                <img src={x.userProfilePictureUrl} alt={x.userUserName}
                                     className='wrapper__modal__items__item__image'/>
                                <p className='wrapper__modal__items__item__name'>{x.userUserName}</p>
                                <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>
                                         {getDifference(new Date(x.wateredOn), new Date())}
                                    </b>
                                </p>
                            </li>
                        ))
                    }
                </ul>
            </div>
        </div>
    )
};

export default ListModal;