import React from 'react';
import * as style from './TextArea.module.scss';

const TextArea = ({label, icon, id, onChange, width, rows}) => (
    <div className={`${style.wrapper} md-form col-md-${width}`}>
        <i className={`${style.icon} ${icon} prefix`}/>
        <textarea id={id} className="md-textarea form-control" onChange={onChange} rows={rows}/>
        <label htmlFor={id}>{label}</label>
    </div>
);

export default TextArea;