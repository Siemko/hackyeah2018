import React from "react";
import { connect } from "react-redux";
import { View, Text, TouchableOpacity, Alert, Image } from "react-native";
import { BarCodeScanner, Permissions } from "expo";
import LoadingDialog from "../components/LoadingDialog";
import RedButton from "../components/RedButton";

class StartScreen extends React.Component {
    state = {
        hasCameraPermissions: null
    };

    qrCodePress = async () => {
        const { status } = await Permissions.askAsync(Permissions.CAMERA);
        const hasCameraPermission = status === "granted"
        if (hasCameraPermission) {
            this.props.navigation.navigate("ScannerScreen")
        } else {
            Alert.alert("Brak uprawnień", "By skanować QR code musisz wyrazić zgodę na używanie kamery.")
        }
    };

    issuesPress = () => {
        this.props.showIssues()
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
                        title="QR code"
                        style={{ flex: 1 }}
                        onPress={this.qrCodePress}
                    />

                    <View style={{ height: 20 }} />

                    <RedButton
                        title="Pokaż aktualne awarie i problemy"
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
