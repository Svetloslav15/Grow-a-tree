import React, {useEffect} from 'react';
import * as style from './Carousel.module.scss';
import Carousel from 'react-multi-carousel';
import 'react-multi-carousel/lib/styles.css';

const responsive = {
    desktop: {
        breakpoint: { max: 3000, min: 1024 },
        items: 5,
    },
    tablet: {
        breakpoint: { max: 1024, min: 464 },
        items: 2,
    },
    mobile: {
        breakpoint: { max: 464, min: 0 },
        items: 1,
    }
};
const CarouselComponent = () => {
    return (
        <div className={`${style.wrapper} col-md-12`}>
            <Carousel responsive={responsive}>
                <img className={style.imageSlider} src="https://www.gardeningknowhow.com/wp-content/uploads/2017/07/hardwood-tree.jpg" alt=""/>
                <img className={style.imageSlider} src="https://www.gardeningknowhow.com/wp-content/uploads/2017/07/hardwood-tree.jpg" alt=""/>
                <img className={style.imageSlider} src="https://www.gardeningknowhow.com/wp-content/uploads/2017/07/hardwood-tree.jpg" alt=""/>
                <img className={style.imageSlider} src="https://www.gardeningknowhow.com/wp-content/uploads/2017/07/hardwood-tree.jpg" alt=""/>
                <img className={style.imageSlider} src="https://www.gardeningknowhow.com/wp-content/uploads/2017/07/hardwood-tree.jpg" alt=""/>
                <img className={style.imageSlider} src="https://www.gardeningknowhow.com/wp-content/uploads/2017/07/hardwood-tree.jpg" alt=""/>
                <img className={style.imageSlider} src="https://www.gardeningknowhow.com/wp-content/uploads/2017/07/hardwood-tree.jpg" alt=""/>
                <img className={style.imageSlider} src="https://www.gardeningknowhow.com/wp-content/uploads/2017/07/hardwood-tree.jpg" alt=""/>
                <img className={style.imageSlider} src="https://www.gardeningknowhow.com/wp-content/uploads/2017/07/hardwood-tree.jpg" alt=""/>
                <img className={style.imageSlider} src="https://www.gardeningknowhow.com/wp-content/uploads/2017/07/hardwood-tree.jpg" alt=""/>
            </Carousel>
        </div>
    );
};

export default CarouselComponent;