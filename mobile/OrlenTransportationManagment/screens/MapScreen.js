import React from "react";
import { MapView } from "expo";

export default class MapScreen extends React.Component {
  state = {
    camera: {
      zoom: 2
    }
  };

  render() {
    return (
      <MapView
        style={{ flex: 1 }}
        initialRegion={{
            latitude: 52.589333,
            longitude: 19.677518,
            latitudeDelta: 0.0000,
            longitudeDelta: 0.0550,
        }}
      />
    );
  }
}
