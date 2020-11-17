import React, {useState, useRef, useEffect} from 'react';
import {Map, Marker, GoogleApiWrapper} from 'google-maps-react';
import * as style from './Map.module.scss';

const MapContainer = ({handleCoordinates}) => {
    const [marker, setMarker] = useState({position: {lat: 0, lng: 0}});

    useEffect(() => {
        navigator.geolocation.getCurrentPosition(getCurrentUserLocation);
    }, []);

    const getCurrentUserLocation = (position) => {
        setMarker({position: {lat: position.coords.latitude, lng: position.coords.longitude}});
    };

    const getNewCoordinates = async (t, map, coord) => {
        const {latLng} = coord;
        const lat = await latLng.lat();
        const lng = await latLng.lng();
        setMarker({
            position: {lat, lng}
        });
        handleCoordinates(lat.toString(), lng.toString());
    };

    return (
        <>
            {marker.position.lat !== 0 ? <Map google={window.google}
                                              zoom={14}
                                              onClick={getNewCoordinates}
                                              initialCenter={{
                                                  lat: marker.position.lat,
                                                  lng: marker.position.lng
                                              }}>
                <Marker position={marker.position}/>
            </Map> : ''}
        </>
    );
};

export default GoogleApiWrapper({
    apiKey: (process.env.REACT_APP_GOOGLE_MAPS_KEY)
})(MapContainer)