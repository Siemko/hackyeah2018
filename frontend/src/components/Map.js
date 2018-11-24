import React, { Component } from "react";
import MapGL, { Marker } from "react-map-gl";
import "mapbox-gl/dist/mapbox-gl.css";
import { MAPBOX_TOKEN } from "../utils/constants";
import { MapMarkerAlt, TimesCircle } from "styled-icons/fa-solid";
import DATA from "../geo.json";
import DeckGL, { GeoJsonLayer, MapView } from "deck.gl";

class Map extends Component {
  state = {
    viewport: {
      latitude: 52.589333,
      longitude: 19.677518,
      zoom: 15
    },
    points: []
  };

  renderLines = () => {
    const roads = DATA;
    return [
      new GeoJsonLayer({
        id: "geojson",
        data: roads,
        opacity: 1,
        lineColor: [255, 0, 0],
        lineWidthMinPixels: 2
      })
    ];
  };

  render() {
    return (
      <DeckGL
        {...this.state.viewport}
        layers={this.renderLines()}
        onClick={event => {
          this.setState(prevState => ({
            points: [...prevState.points, { position: event.lngLat }]
          }));
        }}
        controller
      >
        <MapView controller>
          <MapGL
            reuseMaps
            preventStyleDiffing
            {...this.state.viewport}
            mapStyle="mapbox://styles/mapbox/streets-v10"
            width="100%"
            height="100%"
            onViewportChange={viewport => this.setState({ viewport })}
            mapboxApiAccessToken={MAPBOX_TOKEN}
          />
        </MapView>
        {this.state.points.map(point => {
          return (
            <Marker
              key={point.position[0]}
              latitude={point.position[1]}
              longitude={point.position[0]}
              offsetLeft={-6}
              offsetTop={-18}
            >
              <TimesCircle color="red" size="20" />
            </Marker>
          );
        })}
      </DeckGL>
    );
  }
}

export default Map;
