import React from 'react';
import { MDBInput } from 'mdbreact';

const InputField = ({label, type, id, icon, onChange, width, value, disabled}) => {
    return (
        <div className={`md-form col-md-${width}`}>
            <MDBInput type={type}
                      id={id}
                      onChange={onChange}
                      value={value}
                      disabled={disabled}
                      icon={icon}
                      label={label}
            />
        </div>
    );
};

export default InputField;