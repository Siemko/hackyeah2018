import wretch from "wretch"

export default class IssuesService {

    wretch = wretch().url("https://orlenapi.azurewebsites.net")

    getIssues = async () => {
        return await this.wretch.url("/IssueType").get().json()
    }
    
}