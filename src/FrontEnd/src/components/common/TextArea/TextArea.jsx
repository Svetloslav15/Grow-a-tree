import React from 'react';
import * as style from './TextArea.module.scss';

const TextArea = ({label, type, icon, id, onChange, width, rows}) => (
    <div className={`md-form col-md-${width}`}>
        <i className={`${style.icon} ${icon} prefix`}/>
        <textarea id={id} className="md-textarea form-control" rows={rows}/>
        <label htmlFor={id}>{label}</label>
    </div>
);

export default TextArea;