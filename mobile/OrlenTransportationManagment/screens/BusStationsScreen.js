import React from "react";
import { View, Text, FlatList } from "react-native";
import RedButton from "../components/RedButton";

export default class BusStationsScreen extends React.Component {
    state = {
        busNumber: "",
        stations: [
            {
                name: "Kochłowice Rynek",
                times: "12:32, 21:37, 9:11, 21:37, 9:11, 21:37, 9:11",
                key: "1"
            },
            { name: "Kochłowice Rynek", times: "12:32, 21:37, 9:11", key: "2" },
            { name: "Kochłowice Rynek", times: "12:32, 21:37, 9:11", key: "3" },
            { name: "Kochłowice Rynek", times: "12:32, 21:37, 9:11", key: "4" },
            { name: "Kochłowice Rynek", times: "12:32, 21:37, 9:11", key: "5" },
            { name: "Kochłowice Rynek", times: "12:32, 21:37, 9:11", key: "6" }
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
