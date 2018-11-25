import produce from "immer"
import RoutesService from "../api/RoutesService";

export default {
	state: {
        route: null
    },
	reducers: {
        setRoute(state, payload) {
            return produce(state, draft => {
                draft.route = payload
            });
        }
    },
	effects: (dispatch) => ({
        async getRoute(code) {
            dispatch.loading.setLoadingVisible(true)
            const routesService = new RoutesService()
            const route = await routesService.getRoute(code)
            this.setRoute(route.route)
            dispatch.loading.setLoadingVisible(false)
        }
    }),
}
