import React from 'react';

const DropdownField = ({width, onChange, values, label, id, defaultValue}) => (
    <select id={id}
            className={`mx-auto mb-3 col-md-${width} custom-select`}
            onChange={onChange}
            defaultValue={label}>
        <option value={label}>{label}</option>
        {
            defaultValue ? <option selected={true} value={defaultValue}>{defaultValue}</option> : ''
        }
        {
            values.map((x, i) => <option key={i} value={x}>{x}</option>)
        }
    </select>
);

export default DropdownField;