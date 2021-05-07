import React from 'react';
import * as style from './TreePartial.module.scss';
import {Link} from 'react-router-dom';

const TreePartial = ({tree}) => {
    return (
        <Link to={`/trees/details/${tree.id}`} className={`col-md-5 row ${style.wrapper__items__item}`}>
            <img className={`col-md-2 ${style.wrapper__items__item__image}`} src={tree.image.url}
                 alt={tree.nickName}/>
            <div className={`col-md-10 ${style.wrapper__items__item__info}`}>
                <h3 className={style.wrapper__items__item__info__name}>{tree.nickname}</h3>
                {tree.metresAway && <p className={style.wrapper__items__item__info__description}>Намира се на <span
                    className='font-weight-bold '><i>{tree.metresAway} метра</i></span> от теб</p>}
            </div>
        </Link>
    )
}

export default TreePartial;