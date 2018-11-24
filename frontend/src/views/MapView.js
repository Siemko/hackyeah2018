import React from "react";
import Map from "../components/Map";
import styled from "styled-components";

const MapWrapper = styled.div`
  width: 100%;
  min-height: 500px;
  height: 100vh;
`;
const MapView = () => {
  return (
    <MapWrapper>
      <Map />
    </MapWrapper>
  );
};

export default MapView;
