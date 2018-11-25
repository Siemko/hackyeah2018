import React from "react";
import { connect } from "react-redux";
import { View, Text, TouchableOpacity, Alert, Image } from "react-native";
import { BarCodeScanner, Permissions, Linking } from "expo";
import LoadingDialog from "../components/LoadingDialog";
import RedButton from "../components/RedButton";

class StartScreen extends React.Component {
    state = {
        hasCameraPermissions: null
    };

    contactPress = () => {
        Linking.openURL("tel:+123456789");
    }

    qrCodePress = async () => {
        var { status } = await Permissions.askAsync(Permissions.CAMERA);
        const hasCameraPermission = status === "granted";

        var { status } = await Permissions.askAsync(Permissions.LOCATION);
        const hasLocationPermission = status === "granted";

        if (!hasCameraPermission) {
            Alert.alert(
                "Brak uprawnień",
                "By skanować QR code musisz wyrazić zgodę na używanie kamery."
            );
        } else if (!hasLocationPermission) {
            Alert.alert(
                "Brak uprawnień",
                "By włączyć mapę musisz wyrazić zgodę na używanie lokalizacji."
            );
        } else {
            this.props.navigation.navigate("ScannerScreen");
        }
    };

    busPress = () => {
        this.props.navigation.navigate("BusNumberScreen")
    }

    issuesPress = () => {
        this.props.showIssues();
    };

    render() {
        return (
            <View
                style={{
                    flex: 1,
                    paddingTop: 20,
                    alignContent: "center",
                    alignItems: "center",
                    justifyContent: "center",
                    backgroundColor: "#f3f3f3"
                }}>
                <View
                    style={{
                        alignSelf: "center"
                    }}>
                    <Image
                        source={require("../images/orlen_logo.png")}
                        style={{
                            height: 200,
                            width: 300,
                            alignSelf: "center",
                            marginBottom: 100
                        }}
                        resizeMode="contain"
                    />

                    <RedButton
                        title="Transport wielkogabarytowy"
                        style={{ flex: 1 }}
                        onPress={this.qrCodePress}
                    />

                    <View style={{ height: 20 }} />

                    <RedButton
                        onPress={this.busPress}
                        title="Rozkład jazdy autobusów"
                        style={{ flex: 1 }}
                    />

                    <View style={{ height: 20 }} />

                    <RedButton
                        onPress={this.contactPress}
                        title="Kontakt do koordynatora"
                        style={{ flex: 1 }}
                    />
                </View>
                {this.props.loadingVisible && <LoadingDialog />}
            </View>
        );
    }
}

const mapStateToProps = state => ({
    loadingVisible: state.loading.loadingVisible
});

const mapDispatchToProps = dispatch => ({
    showIssues: () => dispatch.issues.getIssues()
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(StartScreen);
