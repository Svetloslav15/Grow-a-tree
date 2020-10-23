import React from 'react';
import {Link} from 'react-router-dom';
import * as style from './Footer.module.scss';
import SocialMediaIcon from "./SocialMediaIcon/SocialMediaIcon";
import LinksSection from "./LinksSection/LinksSection";

const LogoImage = require('../../../assets/logo.png');
const FacebookImage = require('../../../assets/facebook.png');
const InstagramImage = require('../../../assets/instagram.png');

const data = {
    facebook: {
        src: FacebookImage,
        link: 'https://www.facebook.com/',
        alt: 'Grow A Tree Facebook'
    },
    instagram: {
        src: InstagramImage,
        link: 'https://www.instagram.com/',
        alt: 'Grow A Tree Instagram'
    }
};

const Footer = () => (
  <div className={`${style.wrapper}`}>
      <div className='row'>
          <div className={style.imageWrapper}>
              <img className={`p-3 w-100`} src={LogoImage} alt="Grow A Tree Logo"/>
          </div>
          <LinksSection/>
          <div className={`${style.iconsWrapper} col-md-4`}>
              <SocialMediaIcon props={data.facebook}/>
              <SocialMediaIcon props={data.instagram}/>
          </div>
      </div>
      <p className='text-center mt-sm-1'>Copyright © Grow A Tree 2020-{new Date().getFullYear()}</p>
  </div>
);

export default Footer;