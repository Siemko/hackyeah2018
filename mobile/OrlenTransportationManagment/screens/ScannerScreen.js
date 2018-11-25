import React from "react";
import { View, Text, TouchableOpacity, Alert, StyleSheet } from "react-native";
import { BarCodeScanner, Permissions } from "expo";

class ScannerScreen extends React.Component {
    handleBarCodeScanned = ({ type, data }) => {
        Alert.alert(`${type}`, `${data}`);
    };

    render() {
        return (
            <BarCodeScanner
                onBarCodeScanned={this.handleBarCodeScanned}
                barCodeTypes={[BarCodeScanner.Constants.BarCodeType.qr]}
                style={StyleSheet.absoluteFill}
            />
        );
    }
}

export default ScannerScreen;
