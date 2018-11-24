import { init } from "@rematch/core"
import loading from "./models/loading"
import issues from "./models/issues"

export const store = init({
	models: {loading, issues},
})
