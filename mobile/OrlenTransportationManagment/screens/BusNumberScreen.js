import React from "react";
import { View, Text, Image } from "react-native";
import BusNumber from "../components/BusNumber";
import chunk from "lodash.chunk"

export default class BusNumberScreen extends React.Component {
    state = {
        busNumbers: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"]
    };

    busPress = (number) => {
        console.log("xD")
        this.props.navigation.navigate("BusStationsScreen", {
            number: number
        })
    }

    render() {
        return (
            <View
                style={{
                    flex: 1,
                    alignContent: "center",
                    alignItems: "center",
                    justifyContent: "center"
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
                        }}
                        resizeMode="contain"
                    />

                    <Text style={{marginVertical: 20, fontSize: 25, fontWeight: "bold", textAlign: "center"}}>Wybierz numer linii</Text>
                    <View
                        style={{
                            backgroundColor: "white",
                            padding: 5,
                            flexWrap: "wrap",
                            alignItems: "flex-start",
                            flexDirection: "row"
                        }}>

                        {
                            chunk(this.state.busNumbers, 4).map((value, index) => {
                                return <View style={{ flexDirection: "row" }} key={index.toString()}>
                                    {value.map((v, i) => {
                                        return <BusNumber lineNumber={v} onPress={() => this.busPress(v)} key={((index * i) + i).toString()} />
                                    })}
                                </View>
                            })
                        }
                    </View>
                </View>
            </View>
        );
    }
}
