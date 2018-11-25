import React from "react";
import { View, Text, FlatList } from "react-native";
import RedButton from "../components/RedButton";

export default class BusStationsScreen extends React.Component {
    state = {
        busNumber: "",
        stations: [
            {
                name: "Stroszek Zajezdnia",
                times: "3:27, 4:27, 4:52, 19:06, 20:06",
                key: "1"
            },
            { name: "Dąbrowa Miejska Centrum Handlowe", times: "3:33, 4:33, 5:58, 19:14, 20:14", key: "2" },
            { name: "Bytom Urząd Miasta", times: "3:42, 4:42, 5:07, 19:23, 20:23", key: "3" },
            { name: "Chorzów Batory Dworzec PKP", times: "4:00, 5:00, 5:25, 19:41, 20:41", key: "4" },
            { name: "Katowice Dworzec PKP", times: "4:32, 5:32, 5:57, 20:21, 21:12", key: "5" },
            { name: "Zawodzie Zajezdnia", times: "5:00, 6:00, 6:23, 20:51, 21:48", key: "6" }
        ]
    };

    componentDidMount() {
        const busNumber = this.props.navigation.getParam("number", "");
        this.setState({ busNumber });
    }

    renderRow = item => {
        return (
            <View>
                <Text style={{ padding: 5, fontSize: 20, fontWeight: "bold" }}>
                    Przystanek: {item.name}
                </Text>
                <View
                    style={{
                        flexWrap: "wrap",
                        flex: 1,
                        flexDirection: "row",
                        alignItems: "flex-start"
                    }}>
                    {item.times.split(",").map((value, index) => {
                        return (
                            <Text
                                key={index.toString()}
                                style={{
                                    margin: 5,
                                    padding: 10,
                                    color: "#f3f3f3",
                                    backgroundColor: "#A90023",
                                    borderRadius: 5
                                }}>
                                {value}
                            </Text>
                        );
                    })}
                </View>
            </View>
        );
    };

    render() {
        return (
            <View style={{ flex: 1, marginTop: 20 }}>
                <Text
                    style={{
                        fontWeight: "bold",
                        fontSize: 30,
                        textAlign: "center"
                    }}>{`Autobus numer ${this.state.busNumber}`}</Text>
                <FlatList
                    data={this.state.stations}
                    renderItem={({ item }) => this.renderRow(item)}
                />
                <View style={{ margin: 20 }}>
                <RedButton title="Wróć" onPress={() => this.props.navigation.goBack()} />
                </View>
            </View>
        );
    }
}
