import React, { Component } from "react";
import MapView from "./views/MapView";
import BusView from "./views/BusView";
import { Router, Route, Switch, Redirect } from "react-router-dom";
import history from "./history";
import { Link } from "react-router-dom";
import styled from "styled-components";

const StyledLink = styled(Link)`
  padding: 5px;
`;

const Nav = styled.nav`
  position: absolute;
  top: 15px;
  right: 15px;
  z-index: 1000;
  a {
    display: inline-block;
    background-color: #fff;
    font-size: 16px;
    text-transform: uppercase;
    color: #212121;
    margin-left: 15px;
    padding: 15px;
    box-shadow: 0 10px 20px rgba(0,0,0,0.19), 0 6px 6px rgba(0,0,0,0.23);
    font-weight: bolder;
    &:hover {
      text-decoration: none;
      color: #212121;
    }
  }
`
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
