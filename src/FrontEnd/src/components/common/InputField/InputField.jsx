import React, {useRef, useEffect} from 'react';
import * as style from './InputField.module.scss';

const InputField = ({label, type, id, icon, onChange, width, value, disabled}) => {
    const btnRef = useRef({});

    useEffect(() => {
        console.log(btnRef.current);
        btnRef.current.click();
    }, []);

    return (
        <div className={`md-form col-md-${width}`}>
            {icon ? <i className={`${style.icon} ${icon} prefix`}/> : ''}
            <input type={type} id={id} className="form-control" onChange={onChange} value={value} disabled={disabled}/>
            <label ref={btnRef} htmlFor={id}>{label}</label>
        </div>
    );
};

export default InputField;