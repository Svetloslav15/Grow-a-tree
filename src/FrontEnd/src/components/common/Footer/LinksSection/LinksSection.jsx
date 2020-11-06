import React from 'react';
import {Link} from 'react-router-dom';
import * as style from './LinksSection.module.scss';

const LinksSection = () => (
    <div className={`col-md-4 ml-md-5 pt-sm-3 row`}>
        <Link to="/about" className={`${style.link} col-md-12`}>За платформата</Link>
        <Link to="/faq" className={`${style.link} col-md-12`}>Често задавани въпроси</Link>
        <Link to="/team" className={`${style.link} col-md-12`}>Екип</Link>
    </div>
);

export default LinksSection;