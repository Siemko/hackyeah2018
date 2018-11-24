import React from "react"
import { StyleSheet, View, ActivityIndicator, Dimensions } from "react-native"

export default (LoadingView = ({ transparent, ...props }) => {
    const {height, width} = Dimensions.get("window")
    return (
		<View style={[styles.container, {backgroundColor: transparent ? "#f7f9fc66" : "#f7f9fc66", width: width * 2, height: height * 2, left: -(width / 2), top: -(height / 2)}]}>
			<ActivityIndicator
				translucent={false}
				{...props}
				size="large"
				color="blue"
				style={{ zIndex: 1 }}
			/>
		</View>
	)
})

const styles = StyleSheet.create({
	container: {
        elevation: 20,
		zIndex: 20,
		position: "absolute",
		top: 0,
		bottom: 0,
		left: 0,
		right: 0,
		height: "100%",
		width: "100%",
		alignItems: "center",
		justifyContent: "center",
		backgroundColor: "#f7f9fc66",
	},
})
