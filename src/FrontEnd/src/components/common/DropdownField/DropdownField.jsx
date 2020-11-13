import React from 'react';

const DropdownField = ({width, onChange, values}) => (
    <select className={`mx-auto col-md-${width}`} onSelect={onChange}>
        {
            values.map(x => <option value={x}>{x}</option>)
        }
    </select>
);

export default DropdownField;