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
    return (
      <div>
        ISSUES: {this.state.issues.count}
        {this.state.issues.map(issue => {
          return (
            <div>
              {issue.name}:{issue.value}
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
            this.setState(prevState => ({
              issues: [
                ...prevState.issues,
                {
                  id: this.state.selectedIssueType.id,
                  value: this.state.issueTypeValue
                }
              ],
              selectedIssueType: null,
              issueTypeValue: ""
            }));
          }}
        >
          Add
        </button>
      </div>
    );
  }
}

export default IssueEditor;
