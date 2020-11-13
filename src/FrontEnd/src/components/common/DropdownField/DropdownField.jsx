import React from 'react';

const DropdownField = ({width, onChange, values, label, id}) => (
    <select id={id}
            className={`mx-auto mb-3 col-md-${width} custom-select`}
            onChange={onChange}
            defaultValue={label}>
        <option value={label}>{label}</option>
        {
            values.map((x, i) => <option key={i} value={x}>{x}</option>)
        }
    </select>
);

export default DropdownField;