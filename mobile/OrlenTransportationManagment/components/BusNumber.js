import React from "react";
import { TouchableOpacity, Text } from "react-native";

export default class BusNumber extends React.Component {
    render() {
        return (
            <TouchableOpacity
                style={{
                    padding: 20,
                    backgroundColor: "#A90023",
                    borderRadius: 5,
                    margin: 5,
                    flex: 1
                }}
                {...this.props}>
                <Text style={{ color: "#f3f3f3", fontSize: 20, textAlign: "center" }}>{this.props.lineNumber}</Text>
            </TouchableOpacity>
        );
    }
}
