import React, { Component } from "react";
import MapGL, { Marker, StaticMap } from "react-map-gl";
import "mapbox-gl/dist/mapbox-gl.css";
import { MAPBOX_TOKEN } from "../utils/constants";
import { MapMarkerAlt, TimesCircle } from "styled-icons/fa-solid";
import DeckGL, { GeoJsonLayer, MapView, LineLayer } from "deck.gl";
import { ClimbingBoxLoader } from "react-spinners";
import wretch from "wretch";
import styled from "styled-components";

const LoadingWrapper = styled.div`
  display: flex;
  width: 100%;
  height: 100%;
  align-items: center;
  justify-content: center;
`;
class Map extends Component {
  state = {
    viewport: {
      latitude: 52.589333,
      longitude: 19.677518,
      zoom: 14
    },
    points: null,
    sections: null,
    isLoading: true
  };

  async componentDidMount() {
    try {
      const points = await wretch("https://orlenapi.azurewebsites.net/Point")
        .get()
        .json();
      const sections = await wretch(
        "https://orlenapi.azurewebsites.net/Section"
      )
        .get()
        .json();
      this.setState({ points, sections, isLoading: false });
    } catch (error) {
      this.setState({ isLoading: false, hasError: true });
    }
  }

  renderLines = () => {
    const roads = this.state.sections.map(section => {
      return [section.start.lon, section.start.lat];
    });

    const better = this.state.sections.map(section => {
      return {
        type: "Feature",
        geometry: {
          type: "LineString",
          coordinates: [
            [section.start.lon, section.start.lat],
            [section.end.lon, section.end.lat]
          ],
          properties: {
            id: section.id,
            name: section.name,
            issues: section.issues
          }
        }
      };
    });

    const r = {
      type: "FeatureCollection",
      features: [
        // {
        //   type: "Feature",
        //   geometry: {
        //     type: "LineString",
        //     coordinates: [...roads]
        //   }
        // }
        ...better
      ]
    };
    return [
      new GeoJsonLayer({
        id: "geojson",
        data: r,
        opacity: 1,
        stroked: false,
        filled: true,
        lineWidthMinPixels: 5,
        parameters: {
          depthTest: false
        },
        pickable: true,
        onHover: info => console.log("Hovered:", info),
        onClick: info => console.log("Clicked", info),
        getLineColor: () => [0, 116, 217]
      })
    ];
  };

  render() {
    if (this.state.isLoading)
      return (
        <LoadingWrapper>
          <ClimbingBoxLoader size={24} color="#0074D9" />
        </LoadingWrapper>
      );
    if (this.state.hasError) return <div>ERROR!</div>;
    return (
      <MapGL
        {...this.state.viewport}
        width="100%"
        height="100%"
        onViewportChange={viewport => this.setState({ viewport })}
        mapboxApiAccessToken={MAPBOX_TOKEN}
      >
        <DeckGL
          controller={true}
          onViewportChange={v => this.setState({ viewport: v })}
          {...this.state.viewport}
          layers={this.renderLines()}
          onLayerClick={info => console.log(info)}
        >
          <StaticMap
            mapStyle="mapbox://styles/mapbox/navigation-preview-day-v2"
            mapboxApiAccessToken={MAPBOX_TOKEN}
          >
            {this.state.points.map(point => {
              return (
                <Marker
                  key={point.id}
                  latitude={point.lat}
                  longitude={point.lon}
                  offsetLeft={-10}
                  offsetTop={-10}
                >
                  <MapMarkerAlt color="#0074D9" size="20" />
                </Marker>
              );
            })}
          </StaticMap>
        </DeckGL>
      </MapGL>
    );
  }
}

export default Map;
