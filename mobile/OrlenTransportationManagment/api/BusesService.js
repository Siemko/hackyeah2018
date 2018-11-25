import wretch from "wretch"

export default class BusesService {

    wretch = wretch().url("https://orlenapi.azurewebsites.net")

    getBusNumbers = async () => {
        return await this.wretch.url("/Bus").get().json()
    }
    
}