import wretch from "wretch"

export default class IssuesService {

    wretch = wretch().url("https://orlenapi.azurewebsites.net")

    getIssues = async (code) => {
        return await this.wretch.url("/IssueType").post({code: code}).json()
    }
    
}