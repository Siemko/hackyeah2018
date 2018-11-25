import React from "react";
import { Constants, Location, Permissions, MapView } from "expo";
import { Ionicons, Feather } from '@expo/vector-icons';
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
        regionn: {
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
        this.getLocation();
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

                <View style={{height: 100, flexDirection: "row", alignContent: "center", alignItems: "center", justifyContent: "center"}}>
                    <Ionicons name="ios-snow" size={80} color="blue" style={{width: 80, height: 80, alignSelf: "center"}}/>
                    <Feather name="alert-triangle" size={80} color="orange" style={{width: 80, height: 80, alignSelf: "center"}}/>
                    <Ionicons name="ios-construct" size={80} color="black" style={{width: 80, height: 80, alignSelf: "center"}}/>
                </View>

                <MapView
                    style={{ flex: 1 }}
                    initialRegion={{
                        latitude: this.state.regionn.latitude,
                        longitude: this.state.regionn.longitude,
                        latitudeDelta: 0.0,
                        longitudeDelta: 0.050
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
