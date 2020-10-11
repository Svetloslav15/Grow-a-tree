import React from 'react';
import {h1} from './HomePage.module.scss';
import ButtonOutline from "../../common/ButtonOutline/ButtonOutline";
import ButtonFill from "../../common/ButtonFill/ButtonFill";

const HomePage = () => {
  return (
      <div>
          <h1 className={h1}>Home Page</h1>
          <ButtonFill>Вход</ButtonFill>
          <ButtonOutline>Вход</ButtonOutline>
      </div>
  );
};

export default HomePage;