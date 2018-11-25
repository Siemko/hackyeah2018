import { init } from "@rematch/core"
import loading from "./models/loading"
import issues from "./models/issues"
import routes from "./models/routes"

export const store = init({
	models: {loading, issues, routes},
})
