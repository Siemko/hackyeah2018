import wretch from "wretch"

export default class RoutesService {

    wretch = wretch().url("https://orlenapi.azurewebsites.net")

    getRoute = async (code) => {
        return await this.wretch.url(`/route/${code}`).get().json()
    }

}