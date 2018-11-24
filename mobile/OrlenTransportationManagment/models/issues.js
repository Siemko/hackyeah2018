import produce from "immer"
import IssuesService from "../api/IssuesService";

export default {
	state: {
        loadingVisible: false
    },
	reducers: {
        setLoadingVisible(state, payload) {
            return produce(state, draft => {
                draft.loadingVisible = payload
            });
        }
    },
	effects: (dispatch) => ({
        async getIssues() {
            dispatch.loading.setLoadingVisible(true)
            const issuesService = new IssuesService()
            const issues = await issuesService.getIssues()
            console.warn(issues)
            dispatch.loading.setLoadingVisible(false)
        }
    }),
}
