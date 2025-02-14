import React, {useState, useEffect} from 'react';
import * as style from './InputAutoComplete.module.scss';

const InputAutoComplete = ({width, label, id, icon, data, onChange, value}) => {
    const [isLabelHidden, setHidden] = useState(false);
    const [currVal, setCurrentValue] = useState(value);
    const [previousVal, setPreviousValue] = useState('');
    const [dataElements, setData] = useState([]);

    useEffect(() => {
        if (value !== currVal) {
            setCurrentValue(value);
        }
        setData(data.map((x, i) => <option key={i} value={x}/>));
    }, [value]);

    const handleChange = (event) => {
        if (data.filter(x => x.toLowerCase().includes(event.target.value.toLowerCase())).length === 0) {
            setCurrentValue(previousVal);
        }
        else {
            setPreviousValue(currVal);
            setCurrentValue(event.target.value);
        }
        onChange(event);
    };

    const blur = () => {
        currVal ? setHidden(true) : setHidden(false);
    };

    return (
        <div className={`${style.wrapper} md-form col-md-${width}`}>
            {icon && (<i className={`${style.icon} ${icon} prefix`}/>)}
            <input list='data-list-items' id={id} className='form-control'
                   onSelect={() => setHidden(true)}
                   onChange={handleChange}
                   onBlur={blur}
                   value={currVal}/>
            {!isLabelHidden && !value && <label htmlFor={id}>{label}</label>}
            <datalist id='data-list-items' className={style.datalist}>
                {dataElements}
            </datalist>
        </div>
    )
};

export default InputAutoComplete;