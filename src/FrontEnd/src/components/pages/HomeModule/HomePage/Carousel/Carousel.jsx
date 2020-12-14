import React, {useState, useEffect} from 'react';
import * as style from './Carousel.module.scss';
import Carousel from 'react-multi-carousel';
import 'react-multi-carousel/lib/styles.css';
import TreeService from '../../../../../services/treeService';
import {Link} from 'react-router-dom';

const responsive = {
    desktop: {
        breakpoint: {max: 3000, min: 1024},
        items: 5,
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
const CarouselComponent = () => {
    const [trees, setTrees] = useState([]);

    useEffect(() => {
        TreeService.getTreesForCarousel('')
            .then((data) => {
                setTrees(data.data.data);
                console.log(data.data.data);
            })
    }, []);

    return (
        <div className={`${style.wrapper} col-md-12`}>
            <Carousel responsive={responsive}>
                {trees.length > 0 &&
                (trees.map(x =>
                    <Link to={`/trees/details/${x.treeId}`}>
                        <img className={style.imageSlider}
                             src={x.url} alt={x.treeId}/>
                    </Link>)
                )}
            </Carousel>
        </div>
    );
};
export default CarouselComponent;