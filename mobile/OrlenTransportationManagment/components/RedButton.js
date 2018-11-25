import React from "react";
import { TouchableOpacity, Text } from "react-native";

export default class RedButton extends React.Component {
    render() {
        return (
            <TouchableOpacity
                style={{ backgroundColor: "#A90023", borderRadius: 5 }}
                onPress={this.props.onPress}>
                <Text style={{ padding: 20, textAlign: "center", color: "#f3f3f3", fontSize: 20 }}>{this.props.title}</Text>
            </TouchableOpacity>
        );
    }
}
