import React from "react";
import { View, Text, TouchableOpacity, Alert, StyleSheet } from "react-native";
import { BarCodeScanner, Permissions } from "expo";
import RedButton from "../components/RedButton";

class ScannerScreen extends React.Component {
    
    handleBarCodeScanned = ({ type, data }) => {
        this.props.navigation.navigate("MapScreen", {
            routeId: data
        })
    };

    backPress = () => {
        this.props.navigation.goBack();
    };

    render() {
        return (
            <BarCodeScanner
                onBarCodeScanned={this.handleBarCodeScanned}
                barCodeTypes={[BarCodeScanner.Constants.BarCodeType.qr]}
                style={[StyleSheet.absoluteFill, { flex: 1 }]}>
                <View
                    style={{
                        flex: 1,
                        justifyContent: "center",
                        alignItems: "center",
                        alignContent: "center",
                    }}>
                    <Text style={{textAlign: "center", fontSize: 20, color: "white", marginBottom: 20}}>Skanuj kod QR</Text>
                    <View
                        style={{
                            width: 300,
                            height: 300,
                            borderWidth: 2,
                            borderColor: "white",
                            backgroundColor: "transparent"
                        }}
                    />
                </View>
                <View style={{ width: 300, marginBottom: 20, alignSelf: "center" }}>
                    <RedButton title="Wróć" onPress={this.backPress} />
                </View>
            </BarCodeScanner>
        );
    }
}

export default ScannerScreen;
