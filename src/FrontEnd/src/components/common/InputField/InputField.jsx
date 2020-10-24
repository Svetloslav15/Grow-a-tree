import React from 'react';
import * as style from './InputField.module.scss';

const InputField = ({label, type, id}) => (
    <div className="md-form col-md-6">
        <i className={`${style.icon} fas fa-envelope prefix`}/>
        <input type={type} id={id} className="form-control"/>
        <label htmlFor={id}>{label}</label>
    </div>
);

export default InputField;