import React from "react";
import { StatusBar } from "react-native"
import { createStackNavigator, createAppContainer } from "react-navigation";
import MapScreenn from "./screens/MapScreen";
import StartScreen from "./screens/StartScreen"
import ScannerScreen from "./screens/ScannerScreen"

const AppNavigator = createStackNavigator({
    MapScreen: MapScreenn,
    StartScreen: StartScreen,
    ScannerScreen: ScannerScreen
  },
  {
    initialRouteName: "StartScreen",
    headerMode: "none",
  });

const AppContainer = createAppContainer(AppNavigator);

export default AppContainer;
