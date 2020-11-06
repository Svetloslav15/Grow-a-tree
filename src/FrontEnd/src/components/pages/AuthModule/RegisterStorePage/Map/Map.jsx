import React, {useState} from 'react';
import {Map, InfoWindow, Marker, GoogleApiWrapper} from 'google-maps-react';
import * as style from './Map.module.scss';

const MapContainer = ({google}) => {
    const [marker, setMarker] = useState({});

    const onMarkerClick = () => {
    };
    const onInfoWindowClose = () => {
    };
    const selectedPlace = '';

    const getCoordinates = (t, map, coord) => {
        const {latLng} = coord;
        const lat = latLng.lat();
        const lng = latLng.lng();
        setMarker({
            title: "Място",
            name: "ехооо",
            position: {lat, lng}
        })
    };
    return (
        <>
            <button type="button" className="btn btn-primary" data-toggle="modal" data-target="#basicExampleModal">
                Изберете кординати
            </button>
            <div className="modal fade" id="basicExampleModal" role="dialog"
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
                            <Map google={google} zoom={14} onClick={getCoordinates}>
                                <Marker
                                    title={marker.title}
                                    name={marker.name}
                                    position={marker.position}
                                />
                            </Map>
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-dismiss="modal">Close</button>
                            <button type="button" className="btn btn-primary">Save changes</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default GoogleApiWrapper({
    apiKey: (process.env.REACT_APP_GOOGLE_MAPS_KEY)
})(MapContainer)