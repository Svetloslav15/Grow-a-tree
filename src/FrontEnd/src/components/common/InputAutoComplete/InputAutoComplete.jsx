import React from 'react';
import * as style from './InputAutoComplete.module.scss';

const InputAutoComplete = ({label, id, icon, data}) => (
    <div className="md-form col-md-6">
        <i className={`${style.icon} ${icon} prefix`}/>
        <input list='data-list-items' id={id} className='form-control'/>
        <datalist id='data-list-items'>
            {data.map(x => <option value='\u0410\u0439\u0442\u043e\u0441'/>)}
        </datalist>
    </div>
);

export default InputAutoComplete;