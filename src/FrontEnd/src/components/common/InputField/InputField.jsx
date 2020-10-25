import React from 'react';
import * as style from './InputField.module.scss';

const InputField = ({label, type, id, icon, onChange, width}) => (
    <div className={`md-form col-md-${width}`}>
        <i className={`${style.icon} ${icon} prefix`}/>
        <input type={type} id={id} className="form-control" onChange={onChange}/>
        <label htmlFor={id}>{label}</label>
    </div>
);

export default InputField;