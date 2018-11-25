import React, { Component } from "react";
import MapView from "./views/MapView";
import BusView from "./views/BusView";
import { Router, Route, Switch, Redirect } from "react-router-dom";
import history from "./history";
import { Nav } from "reactstrap";
import { Link } from "react-router-dom";
import styled from "styled-components";

const StyledLink = styled(Link)`
  padding: 5px;
`;
class App extends Component {
  render() {
    return (
      <>
        <Router history={history}>
          <>
            <Nav style={{zIndex: 10}}>
              <StyledLink to="/map">Transporty</StyledLink>
              <StyledLink to="/bus">Bus</StyledLink>
            </Nav>
            <Switch>
              <Route path="/map" component={MapView} />
              <Route path="/bus" component={BusView} />
              <Redirect to="/map" />
            </Switch>
          </>
        </Router>
      </>
    );
  }
}

export default App;
