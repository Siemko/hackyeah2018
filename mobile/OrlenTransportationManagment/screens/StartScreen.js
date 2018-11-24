import React from "react";
import { View, Text, TouchableOpacity, Alert } from "react-native";
import { BarCodeScanner, Permissions } from "expo";

class StartScreen extends React.Component {
    state = {
        hasCameraPermissions: null
    }

    qrCodePress = async () => {
        const { status } = await Permissions.askAsync(Permissions.CAMERA);
        hasCameraPermission = status === "granted"

        if (hasCameraPermission === null) {
            Alert.alert("Uperawnienia", "");
        } else if (hasCameraPermission === false) {
            Alert.alert("Uperawnienia", "");
        } else {

        }
    }

    render() {
        return (
            <View
                style={{
                    flex: 1,
                    paddingTop: 20,
                    alignContent: "center",
                    alignItems: "center",
                    justifyContent: "center",
                    backgroundColor: "yellow"
                }}>
                <View
                    style={{
                        backgroundColor: "red",
                        alignSelf: "center"
                    }}>
                    <TouchableOpacity
                        onPress={this.qrCodePress}
                        style={{ padding: 20 }}>
                        <Text>Skanuj QR code</Text>
                    </TouchableOpacity>
                    <TouchableOpacity style={{ padding: 20 }}>
                        <Text>Pokaż aktualne awarie i problemy</Text>
                    </TouchableOpacity>
                </View>
            </View>
        );
    }
}

export default StartScreen;
