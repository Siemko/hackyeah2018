import React from "react";
import { View } from "react-native"
import { createStackNavigator, createAppContainer  } from "react-navigation";
import MapScreen from "./screens/MapScreen";
import StartScreen from "./screens/StartScreen"
import ScannerScreen from "./screens/ScannerScreen"

const AppNavigator = createStackNavigator({
    MapScreen: MapScreen,
    MapScreen: MapScreen,
    ScannerScreen: ScannerScreen
  },
  {
    initialRouteName: "ScannerScreen",
    headerMode: "none",
  });

const AppContainer = createAppContainer(AppNavigator);

export default AppContainer;
