import produce from "immer"
import BusesService from "../api/BusesService";

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
        async getBusNumbers() {
            dispatch.loading.setLoadingVisible(true)
            const busesService = new BusesService()
            const issues = await busesService.getBusNumbers()
            console.warn(issues)
            dispatch.loading.setLoadingVisible(false)
        }
    }),
}
