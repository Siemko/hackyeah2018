import React, { Component } from "react";
import MapView from "./views/MapView";
import BusView from './views/BusView';

class App extends Component {
  render() {
    return (
      <>
        <BusView />
        {/* <MapView /> */}
      </>
    );
  }
}

export default App;
