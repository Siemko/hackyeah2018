import React, { PureComponent } from "react";
import wretch from "wretch";
class IssueEditor extends PureComponent {
  state = {
    issues: this.props.issues,
    id: this.props.id,
    issueTypes: null,
    selectedIssueType: null,
    issueTypeValue: ""
  };

  async componentDidMount() {
    const issueTypes = await wretch(
      "https://orlenapi.azurewebsites.net/IssueType"
    )
      .get()
      .json();
    this.setState({ issueTypes });
  }

  render() {
    if (!this.state.issueTypes) return null;
    console.log(this.state.id);
    return (
      <div>
        Ograniczenia: {this.state.issues.length}
        {this.state.issues.map(issue => {
          return (
            <div key={issue.name}>
              {issue.name} {issue.value}
            </div>
          );
        })}
        <select
          value={
            this.state.selectedIssueType
              ? this.state.selectedIssueType.id
              : this.state.issueTypes[0].id
          }
          onChange={e => {
            const t = this.state.issueTypes.find(it => it.id == e.target.value);
            this.setState({ selectedIssueType: t });
          }}
        >
          {this.state.issueTypes.map(iss => {
            return (
              <option key={iss.id} value={iss.id}>
                {iss.name}
              </option>
            );
          })}
        </select>
        <input
          value={this.state.issueTypeValue}
          onChange={e => {
            this.setState({ issueTypeValue: e.target.value });
          }}
        />
        <button
          onClick={() => {
            this.setState(
              prevState => ({
                issues: [
                  ...prevState.issues,
                  {
                    id: this.state.selectedIssueType
                      ? this.state.selectedIssueType.id
                      : 1,
                    value: this.state.issueTypeValue
                  }
                ],
                selectedIssueType: null,
                issueTypeValue: ""
              }),
              () => {
                let promisesArr = [];
                this.state.issues.forEach(issue => {
                  promisesArr.push(
                    wretch("https://orlenapi.azurewebsites.net/Section/issue")
                      .post({
                        issueTypeId: issue.id,
                        value: issue.value,
                        sectionId: this.state.id
                      })
                      .res()
                  );
                });
                Promise.all(promisesArr);
              }
            );
          }}
        >
          Dodaj
        </button>
        <button
          onClick={async () => {
            wretch(`https://orlenapi.azurewebsites.net/Section/clear-issues/${this.state.id}`)
              .delete()
              .res();
          }}
        >
          Usuń ograniczenia
        </button>
      </div>
    );
  }
}

export default IssueEditor;
