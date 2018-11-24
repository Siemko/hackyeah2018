import React from "react";
import { connect } from "react-redux";
import { View, Text, TouchableOpacity, Alert } from "react-native";
import { BarCodeScanner, Permissions } from "expo";
import LoadingDialog from "../components/LoadingDialog";

class StartScreen extends React.Component {

    state = {
        hasCameraPermissions: null
    };

    qrCodePress = async () => {
        this.props.showIssues()
        // const { status } = await Permissions.askAsync(Permissions.CAMERA);
        // hasCameraPermission = status === "granted";

        // if (hasCameraPermission === null) {
        //     Alert.alert("Uperawnienia", "");
        // } else if (hasCameraPermission === false) {
        //     Alert.alert("Uperawnienia", "");
        // } else {
        // }
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
