import React, {useState, useRef, useEffect} from 'react';
import {Map, Marker, GoogleApiWrapper} from 'google-maps-react';
import * as style from './Map.module.scss';
import Button from "../../../../common/Button/Button";

const MapContainer = ({google, handleCoordinates}) => {
    const [marker, setMarker] = useState({position: {lat: 0, lng: 0}});
    const [isSelected, setIsSelected] = useState(false);

    const closeButton = useRef();
    const openModalButton = useRef();

    useEffect(() => {
        navigator.geolocation.getCurrentPosition(getCurrentUserLocation);
    }, []);

    const getCurrentUserLocation = (position) => {
        setMarker({position: {lat: position.coords.latitude, lng: position.coords.longitude}});
    };

    const getNewCoordinates = (t, map, coord) => {
        const {latLng} = coord;
        const lat = latLng.lat();
        const lng = latLng.lng();
        setMarker({
            position: {lat, lng}
        });
        handleCoordinates(lat.toString(), lng.toString());
        setIsSelected(true);
        closeButton.current.click();
    };
    return (
        <>
            <div className='row col-md-12'>
                <div className='col-md-7'>
                    <Button type="Dark"
                            onClick={() => openModalButton.current.click()}>
                            Изберете кординати
                    </Button>
                    <button ref={openModalButton} data-toggle="modal" data-target="#mapContainerModal" className='d-none'/>
                </div>
                {
                    isSelected && (
                        <div className='col-md-5'>
                            <p><span className='font-weight-bold'>Latitude</span>: {marker.position.lat.toFixed(4)}</p>
                            <p><span className='font-weight-bold'>Longitude</span>: {marker.position.lng.toFixed(4)}</p>
                        </div>)
                }

            </div>
            <div className="modal fade" id="mapContainerModal" role="dialog"
                 aria-labelledby="exampleModalLabel"
                 aria-hidden="true">
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="exampleModalLabel">Изберете адрес</h5>
                            <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div className={`modal-body ${style.mapContainer}`}>
                            <Map google={google}
                                 zoom={14}
                                 onClick={getNewCoordinates}
                                 center={{
                                     lat: marker.position.lat,
                                     lng: marker.position.lng
                                 }}>
                                <Marker position={marker.position}/>
                            </Map>
                        </div>
                        <button type="hidden" ref={closeButton} className="d-none btn btn-secondary"
                                data-dismiss="modal"/>
                    </div>
                </div>
            </div>
        </>
    );
};

export default GoogleApiWrapper({
    apiKey: (process.env.REACT_APP_GOOGLE_MAPS_KEY)
})(MapContainer)