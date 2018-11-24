import React from "react";
import { createStackNavigator, createAppContainer  } from "react-navigation";
import MapScreen from "./screens/MapScreen";
import StartScreen from "./screens/StartScreen"
import ScannerScreen from "./screens/ScannerScreen"

const AppNavigator = createStackNavigator({
    MapScreen: MapScreen,
    StartScreen: StartScreen,
    ScannerScreen: ScannerScreen
  },
  {
    initialRouteName: "StartScreen",
    headerMode: "none",
  });

const AppContainer = createAppContainer(AppNavigator);

export default AppContainer;
