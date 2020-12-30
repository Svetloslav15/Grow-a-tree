import React, {useEffect, useState} from 'react';
import './ListModal.scss';

const DefaultProfilePicture = require('../../../../../assets/user-profile.png');
const BgShapeTwo = require('../../../../../assets/bg-shape-2.png');
const BgShapeThree = require('../../../../../assets/bg-shape-3.png');

const ListModal = ({closeModal}) => {
    return (
        <div className='wrapper'>
            <div className='overlay-bg' onClick={closeModal}></div>
            <div className='wrapper__modal'>
                <img className='bg-shape-image' src={BgShapeTwo} alt="Background Shape Image"/>
                <img className='bg-shape-image' src={BgShapeThree} alt="Background Shape Image"/>
                <i className="fas fa-times close-button" onClick={closeModal}></i>

                <p className='wrapper__modal__title'>Последни поливания</p>
                <ul className='wrapper__modal__items'>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                    <li className='wrapper__modal__items__item'>
                        <img src={DefaultProfilePicture} alt="" className='wrapper__modal__items__item__image'/>
                        <p className='wrapper__modal__items__item__name'>Svetloslav</p>
                        <p className='wrapper__modal__items__item__description'> - поля дърво преди <b>4 мин</b></p>
                    </li>
                </ul>
            </div>
        </div>
    )
};

export default ListModal;