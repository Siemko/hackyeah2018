import produce from "immer"

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
        showLoading(visible) {
            this.setLoadingVisible(visible)
        }
    }),
}
