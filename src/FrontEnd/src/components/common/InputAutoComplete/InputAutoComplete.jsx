import React, {useState, useEffect} from 'react';
import * as style from './InputAutoComplete.module.scss';

const InputAutoComplete = ({label, id, icon, data}) => {
    const [isLabelHidden, setHidden] = useState(false);
    const [currVal, setCurrentValue] = useState('');
    const [dataElements, setData] = useState([]);

    useEffect(() => {
        setData(data.map((x, i) => <option key={i} value={x}/>))
    }, []);

    const handleChange = (event) => {
        event.preventDefault();
        setCurrentValue(event.target.value);
    };

    const blur = () => {
        currVal ? setHidden(true) : setHidden(false);
    };

    return (
        <div className="md-form col-md-6">
            <i className={`${style.icon} ${icon} prefix`}/>
            <input list='data-list-items' id={id} className='form-control' onSelect={() => setHidden(true)} onChange={handleChange} onBlur={blur}/>
            {!isLabelHidden && <label htmlFor={id}>{label}</label>}
            <datalist id='data-list-items' className={style.datalist}>
                {dataElements}
            </datalist>
        </div>
    )
};

export default InputAutoComplete;