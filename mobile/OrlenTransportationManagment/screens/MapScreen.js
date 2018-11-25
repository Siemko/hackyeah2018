import React from "react";
import { Constants, Location, Permissions, MapView } from "expo";
import { View } from "react-native";
import { connect } from "react-redux";
import Circle from "../node_modules/react-native-maps/lib/components/MapCircle";
import Polyline from "../node_modules/react-native-maps/lib/components/MapPolyline";
import RedButton from "../components/RedButton";

class MapScreenn extends React.Component {
    state = {
        permissionsGranded: false,
        currentLocation: {
            latitude: 52.589333,
            longitude: 19.677518
        },
        region: {
            latitude: 52.589333,
            longitude: 19.677518,
            latitudeDelta: 0.0,
            longitudeDelta: 0.015
        },
        autoCenter: true,
        points: [
            {
                latitude: 52.589333,
                longitude: 19.677518
            },
            {
                latitude: 53.41058,
                longitude: -2.97794
            },
            {
                latitude: 48.864716,
                longitude: 2.349014
            }
        ]
    };

    async componentDidMount() {
        const { status } = await Permissions.askAsync(Permissions.LOCATION);
        this.setState({ permissionsGranded: status === "granted" });
        await this.getLocation();
        const routeId = this.props.navigation.getParam("routeId", "0");
        this.props.getRoute(routeId);
    }

    getLocation = async () => {
        const location = await Location.getCurrentPositionAsync({
            enableHighAccuracy: true
        });
        this.userLocationChange(location);
        setTimeout(this.getLocation, 1000);
    };

    userLocationChange = position => {
        if (this.state.autoCenter) {
            this.setState({
                region: {
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude,
                    latitudeDelta: 0.0,
                    longitudeDelta: 0.015
                }
            });
        }
    };

    render() {
        return (
            <View style={{ flex: 1 }}>
                <MapView
                    style={{ flex: 1 }}
                    initialRegion={{
                        latitude: this.state.region.latitude,
                        longitude: this.state.region.longitude,
                        latitudeDelta: 0.0,
                        longitudeDelta: 0.015
                    }}
                    region={{
                        latitude: this.state.region.latitude,
                        longitude: this.state.region.longitude,
                        latitudeDelta: 0.0,
                        longitudeDelta: 0.015
                    }}>
                    <Circle
                        center={{
                            latitude: this.state.region.latitude,
                            longitude: this.state.region.longitude
                        }}
                        radius={10}
                        strokeWidth={5}
                        fillColor="white"
                        strokeColor="#0074d9"
                    />
                    <Polyline
                        coordinates={
                            this.props.route ? this.props.route.points : []
                        }
                        strokeColor="#2ecc40"
                        strokeWidth={3}
                    />
                </MapView>
                <View style={{ margin: 20 }}>
                    <RedButton
                        title="Wróć"
                        onPress={() => this.props.navigation.goBack()}
                    />
                </View>
            </View>
        );
    }
}

const mapStateToProps = state => ({
    route: state.routes.route
});

const mapDispatchToProps = dispatch => ({
    getRoute: code => dispatch.routes.getRoute(code)
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(MapScreenn);
