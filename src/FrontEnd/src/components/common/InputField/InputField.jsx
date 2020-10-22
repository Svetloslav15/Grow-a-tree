import React from 'react';
import * as style from './InputField.module.scss';

const InputField = ({label, type,}) => (
    <div className="md-form col-md-6">
        <i className={`${style.icon} fas fa-envelope prefix`}/>
        <input type={type} id="form1" className="form-control"/>
        <label for="form1">{label}</label>
    </div>
);

export default InputField;