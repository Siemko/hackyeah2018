import React from "react";
import { Constants, Location, Permissions, MapView } from "expo";
import Circle from "../node_modules/react-native-maps/lib/components/MapCircle";
import Polyline from "../node_modules/react-native-maps/lib/components/MapPolyline";

export default class MapScreenn extends React.Component {
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
            longitudeDelta: 0.055
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
        Location.watchPositionAsync(
            { enableHighAccuracy: true },
            this.userLocationChange
        );
    }

    getLocation = async () => {
        const location = await Location.getCurrentPositionAsync({
            enableHighAccuracy: true
        });

        this.setState({
            currentLocation: {
                latitude: location.coords.latitude,
                longitude: location.coords.longitude
            }
        });
        setTimeout(this.getLocation, 1000);
    };

    userLocationChange = position => {
        if (this.state.autoCenter) {
            this.setState(prevState => ({
                region: {
                    latitudeDelta: 0.0,
                    longitudeDelta: 0.055,
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude
                }
            }));
        }
        console.log(position.coords.latitude);
    };

    onPanDrag = (coordinate, position) => {
        console.log(coordinate, position);
    };

    render() {
        return (
            <MapView
                style={{ flex: 1 }}
                showsMyLocationButton={true}
                showsUserLocation={true}
                followsUserLocation={true}
                initialRegion={{
                    latitude: this.state.region.latitude,
                    longitude: this.state.region.longitude,
                    latitudeDelta: 0.0,
                    longitudeDelta: 0.055
                }}>
                <Polyline
                    coordinates={this.state.points}
                    strokeColor="#000"
                    strokeColors={[
                        "#7F0000",
                        "#00000000",
                        "#B24112",
                        "#E5845C",
                        "#238C23",
                        "#7F0000"
                    ]}
                    strokeWidth={3}
                />
            </MapView>
        );
    }
}
