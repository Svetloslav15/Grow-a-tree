import React from 'react';
import {Link} from 'react-router-dom';
import * as style from './Footer.module.scss';

const LogoImage = require('../../../assets/logo.png');
const FacebookImage = require('../../../assets/facebook.png');
const InstagramImage = require('../../../assets/instagram.png');

const Footer = () => (
  <div className={`${style.wrapper}`}>
      <div className='row'>
          <div className={style.imageWrapper}>
              <img className={`p-3 w-100`} src={LogoImage} alt="Grow A Tree Logo"/>
          </div>
          <div className={`${style.linksWrapper} col-md-4 ml-md-5 pt-sm-3 row`}>
              <Link to="/about" className={`${style.link} col-md-12`}>За платформата</Link>
              <Link to="/faq" className={`${style.link} col-md-12`}>Често задавани въпроси</Link>
              <Link to="/team" className={`${style.link} col-md-12`}>Екип</Link>
          </div>
          <div className={`${style.iconsWrapper} col-md-4`}>
              <a href='https://www.facebook.com/' target='_blank'>
                  <img className={`${style.icon}`} src={FacebookImage} alt='Grow A Tree Facebook'/>
              </a>
              <a href='https://www.instagram.com/' target='_blank'>
                <img className={`${style.icon}`} src={InstagramImage} alt='Grow A Tree Instagram'/>
              </a>
          </div>
      </div>
      <p className='text-center mt-sm-1'>Copyright © Grow A Tree 2020-{new Date().getFullYear()}</p>
  </div>
);

export default Footer;