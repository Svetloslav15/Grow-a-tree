import React from 'react';
import * as style from './Footer.module.scss';

const LogoImage = require('../../../assets/logo.png');
const FacebookImage = require('../../../assets/facebook.png');

const Footer = () => (
  <div className={`${style.wrapper}`}>
      <div className='row'>
          <div className={style.imageWrapper}>
              <img className={`p-3 w-100`} src={LogoImage} alt="Grow A Tree Logo"/>
          </div>
          <div className={`${style.linksWrapper} col-md-4 ml-md-5 pt-sm-3 row`}>
              <a href="#" className={`${style.link} col-md-12`}>За платформата</a>
              <a href="#" className={`${style.link} col-md-12`}>Често задавани въпроси</a>
              <a href="#" className={`${style.link} col-md-12`}>Екип</a>
          </div>
          <div className={`${style.iconsWrapper}`}>
              <img className={`${style.icon}`} src={FacebookImage} alt='Grow A Tree Facebook'/>
              <img className={`${style.icon}`} src={FacebookImage} alt='Grow A Tree Instagram'/>
          </div>
      </div>
      <p className='text-center mt-sm-1'>Copyright © Grow A Tree 2020-{new Date().getFullYear()}</p>
  </div>
);

export default Footer;