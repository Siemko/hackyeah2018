import React, { Component } from "react";
import MapGL, { Marker, StaticMap, Popup } from "react-map-gl";
import "mapbox-gl/dist/mapbox-gl.css";
import { MAPBOX_TOKEN } from "../utils/constants";
import { Circle, ExclamationTriangle } from "styled-icons/fa-solid";
import { Smile } from "styled-icons/fa-regular";
import DeckGL, { GeoJsonLayer } from "deck.gl";
import { ClimbingBoxLoader } from "react-spinners";
import wretch from "wretch";
import styled from "styled-components";
import logo from "../logo.png";

const LoadingWrapper = styled.div`
  display: flex;
  width: 100%;
  height: 100%;
  align-items: center;
  justify-content: center;
  flex-direction: column;
`;

const PopupWrapper = styled.div`
  padding: 2em;
  background: #f3f3f3;
`;

const BusList = styled.div`
    position: absolute;
    left: 15px;
    top: 15px;
    bottom: 15px;
    width: 300px;
    background-color: #fff;
    z-index: 99999;
`

const BusItem = styled.div`
    padding: 15px;
`

const NewBus = styled.div`
    cursor: pointer;
    background-color: #a90023;
    color: #fff;
    text-align: center;
    padding: 15px;
    font-weight: bolder;
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    border-radius: 2px;
    box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24);
`

const AddBusStop = styled.div`
    background-color: #a90023;
    border-radius: 2px;
    color: #fff;
    padding: 5px;
    box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24);
    cursor: pointer;
`

class Bus extends Component {
    state = {
        viewport: {
            latitude: 52.589333,
            longitude: 19.677518,
            zoom: 14
        },
        points: null,
        sections: null,
        isLoading: true,
        showPopup: false,
        popupObject: null,
        showTooltip: false,
        tooltipObject: null,
        busList: [],
        busStopList: [],
    };

    async componentDidMount() {
        try {
            const points = await wretch("https://orlenapi.azurewebsites.net/Point")
                .get()
                .json();
            let sections = await wretch("https://orlenapi.azurewebsites.net/Section")
                .get()
                .json();
            sections = sections.map(section => ({
                ...section,
                start: points.find(x => x.id === section.startId),
                end: points.find(x => x.id === section.endId)
            }));
            const route = await wretch("https://orlenapi.azurewebsites.net/Route/1")
                .get()
                .json();
            this.setState({ points, sections, isLoading: false, route });
        } catch (error) {
            this.setState({ isLoading: false, hasError: true });
        }
    }

    renderLines = () => {
        const better = this.state.sections.map(section => {
            return {
                type: "Feature",
                geometry: {
                    type: "LineString",
                    coordinates: [
                        [section.start.longitude, section.start.latitude],
                        [section.end.longitude, section.end.latitude]
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
            features: [...better]
        };

        const routeCoords = this.state.route.points.map(point => {
            return [point.longitude, point.latitude];
        });

        const better2 = {
            type: "Feature",
            geometry: {
                type: "LineString",
                coordinates: [...routeCoords]
            }
        };

        const route = {
            type: "FeatureCollection",
            features: [better2]
        };

        return [
            new GeoJsonLayer({
                id: "ALL SECTIONS",
                data: r,
                opacity: 1,
                stroked: false,
                filled: true,
                lineWidthMinPixels: 5,
                parameters: {
                    depthTest: false
                },
                pickable: true,
                onHover: info => {
                    if (info.picked && !this.state.showPopup) {
                        const toolObj = {
                            lat: info.lngLat[1],
                            lon: info.lngLat[0],
                            issues: info.object.geometry.properties.issues
                        };
                        this.setState({ showTooltip: true, tooltipObject: toolObj });
                    } else this.setState({ showTooltip: false, tooltipObject: null });
                },
                onClick: info => {
                    this.setState({
                        showPopup: true,
                        popupObject: info,
                        showTooltip: false,
                        tooltipObject: null
                    });
                },
                getLineColor: f => {
                    if (f.geometry.properties.issues.length > 0) return [255, 220, 0];
                    else if (!f.geometry.properties.issues.length) return [46, 204, 64];
                }
            }),

            new GeoJsonLayer({
                id: "ROUTE",
                data: route,
                opacity: 1,
                stroked: false,
                filled: true,
                lineWidthMinPixels: 5,
                parameters: {
                    depthTest: false
                },
                getLineColor: () => {
                    return [1, 1, 1];
                }
            })
        ];
    };

    addBusStop = () => {
        console.log(this.state.markersObject)
        this.setState(prevState => ({
            busStopList: [...prevState.busStopList, prevState.markersObject]
        }))
    }

    render() {
        if (this.state.isLoading)
            return (
                <LoadingWrapper>
                    <img alt="logo" src={logo} style={{ marginBottom: "2em" }} />
                    <ClimbingBoxLoader size={18} color="#a90023" />
                </LoadingWrapper>
            );
        if (this.state.hasError) return <div>ERROR!</div>;
        return (
            <>
                <BusList>
                    {this.state.busList.map((bus, index) => {
                        return (
                            <BusItem key={index}>
                                <h3>Linia {bus.number}</h3>
                            </BusItem>
                        )
                    })}
                    {this.state.busStopList.map((busStop, index) => {
                        return (
                            <BusItem key={index}>
                                <h3>Przystanek {busStop.name}</h3>
                            </BusItem>
                        )
                    })}
                    <NewBus>Nowa linia</NewBus>
                </BusList>
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
                    >
                        <StaticMap
                            mapStyle="mapbox://styles/mapbox/navigation-preview-day-v2"
                            mapboxApiAccessToken={MAPBOX_TOKEN}
                        />
                    </DeckGL>
                    {this.state.points.map(point => {
                        return (
                            <Marker
                                key={point.id}
                                latitude={point.latitude}
                                longitude={point.longitude}
                                offsetLeft={-10}
                                offsetTop={-10}
                            >
                                <Circle
                                    color="#fff"
                                    size="20"
                                    onMouseOver={() => {
                                        const toolObj = {
                                            lat: point.latitude,
                                            lon: point.longitude,
                                            issues: [],
                                            info: point.name
                                        };
                                        this.setState({ showTooltip: true, tooltipObject: toolObj });
                                    }}
                                    onClick={() => {
                                        this.setState({ showMarkers: true, markersObject: point, showTooltip: false, tooltipObject: null })
                                    }}
                                />
                            </Marker>
                        );
                    })}
                    {this.state.showPopup && (
                        <Popup
                            latitude={this.state.popupObject.lngLat[1]}
                            longitude={this.state.popupObject.lngLat[0]}
                            closeButton={true}
                            closeOnClick={false}
                            anchor="bottom"
                            onClose={() =>
                                this.setState({ showPopup: false, popupObject: null })
                            }
                        >
                            <PopupWrapper>
                                {this.state.popupObject.object.geometry.properties.id}
                            </PopupWrapper>
                        </Popup>
                    )}
                    {this.state.showMarkers && (
                        <Popup
                            latitude={this.state.markersObject.latitude}
                            longitude={this.state.markersObject.longitude}
                            closeButton={true}
                            closeOnClick={false}
                            anchor="top"
                            onClose={() =>
                                this.setState({ showMarkers: false, markersObject: null })
                            }
                        >
                            <PopupWrapper>
                                <h4 className="text-center">Punkt {this.state.markersObject.name}</h4>
                                <AddBusStop onClick={this.addBusStop}>Dodaj przystanek</AddBusStop>
                            </PopupWrapper>
                        </Popup>
                    )}
                    {this.state.showTooltip && (
                        <Popup
                            latitude={this.state.tooltipObject.lat}
                            longitude={this.state.tooltipObject.lon}
                            closeButton={false}
                            closeOnClick={true}
                            anchor="top"
                            onClose={() =>
                                this.setState({ showTooltip: false, tooltipObject: null })
                            }
                        >
                            <div>
                                {this.state.tooltipObject.issues.length === 0 && (
                                    <Smile color="#2ECC40" size={24} />
                                )}
                                {this.state.tooltipObject.issues.length > 0 && (
                                    <ExclamationTriangle color="#FFDC00" size={24} />
                                )}
                            </div>
                        </Popup>
                    )}
                </MapGL>
            </>
        );
    }
}

export default Bus;
