import React from 'react';
import './Carousel.scss';
import Carousel from 'react-multi-carousel';
import 'react-multi-carousel/lib/styles.css';

const responsive = {
    desktop: {
        breakpoint: {max: 3000, min: 1024},
        items: 2,
    },
    tablet: {
        breakpoint: {max: 1024, min: 464},
        items: 2,
    },
    mobile: {
        breakpoint: {max: 464, min: 0},
        items: 1,
    }
};
const CarouselComponent = ({images}) => (
    <div className='tree-details-section col-md-6'>
        <Carousel responsive={responsive}>
            {images && images.length > 0 &&
            (images.map(x =>
                    <img className='tree-details-section__image-slider'
                         src={x.url} alt={x.id}/>)
            )}
        </Carousel>
    </div>
);

export default CarouselComponent;