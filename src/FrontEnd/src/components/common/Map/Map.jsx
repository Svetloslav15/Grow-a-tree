import React, {useState, useRef, useEffect} from 'react';
import {Map, Marker, GoogleApiWrapper} from 'google-maps-react';
import * as style from './Map.module.scss';

const MapContainer = ({handleCoordinates, coordinates, isStatic, className, markers}) => {
    const [marker, setMarker] = useState({position: {lat: 0, lng: 0}});

    useEffect(() => {
        console.log(markers);
        if (coordinates) {
            setMarker({position: {lat: coordinates.latitude, lng: coordinates.longitude}});
        } else {
            navigator.geolocation.getCurrentPosition(getCurrentUserLocation);
        }
    }, []);

    const getCurrentUserLocation = (position) => {
        setMarker({position: {lat: position.coords.latitude, lng: position.coords.longitude}});
    };

    const getNewCoordinates = async (t, map, coord) => {
        if (isStatic) {
            return
        }

        const {latLng} = coord;
        const lat = await latLng.lat();
        const lng = await latLng.lng();
        setMarker({
            position: {lat, lng}
        });

        if (handleCoordinates) {
            handleCoordinates(lat.toString(), lng.toString());
        }
    };

    return (
        <div className={className}>

            {marker.position.lat !== 0 ? <Map google={window.google}
                                              zoom={14}
                                              onClick={getNewCoordinates}
                                              initialCenter={{
                                                  lat: marker.position.lat,
                                                  lng: marker.position.lng
                                              }}>
                <Marker position={marker.position}/>
                {markers.map((mark, index) => <Marker key={index} position={{
                    lat: mark.latitude,
                    lng: mark.longitude
                }}/>)}
            </Map> : ''}
        </div>
    );
};

export default GoogleApiWrapper({
    apiKey: (process.env.REACT_APP_GOOGLE_MAPS_KEY)
})(MapContainer)