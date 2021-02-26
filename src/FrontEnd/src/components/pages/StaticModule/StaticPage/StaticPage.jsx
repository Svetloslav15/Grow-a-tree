import React, {useEffect, useState} from 'react';
import {useLocation} from 'react-router-dom';

import * as styles from './StaticPage.module.scss';
import SectionComponent from './SectionComponent/SectionComponent';
import staticPagesData from '../../../../static/staticPagesData';

const StaticPage = (props) => {
    const location = useLocation();
    const [data, setData] = useState(null);

    useEffect(() => {
        const currentUrlPage = location.pathname.split('/').reverse()[0];
        for (let index = 0; index < staticPagesData.length; index++) {
            if (staticPagesData[index].route === currentUrlPage) {
                setData(staticPagesData[index]);
                break;
            }
        }
    }, [props]);
    return (
        <div className={styles.wrapper}>
            <h1>{data && data.title}</h1>
            <p>{data && data.headerText}</p>
            <div>
                {data && data.data.map(section => <SectionComponent {...section}/>)}
            </div>
        </div>
    )
}

export default StaticPage;
