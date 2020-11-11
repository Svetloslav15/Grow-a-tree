import React, {useRef, useEffect} from 'react';
import * as style from './InputField.module.scss';

const InputField = ({label, type, id, icon, onChange, width, value}) => {
    const btnRef = useRef(null);

    useEffect(() => {
        btnRef.current.click();
    }, []);

    return (
        <div className={`md-form col-md-${width}`}>
            <i className={`${style.icon} ${icon} prefix`}/>
            <input type={type} id={id} ref={btnRef} className="form-control" onChange={onChange} value={value}/>
            <label htmlFor={id}>{label}</label>
        </div>
    );
};

export default InputField;