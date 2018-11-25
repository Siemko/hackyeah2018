import React, { PureComponent } from "react";
import wretch from "wretch";
class IssueEditor extends PureComponent {
  state = {
    issues: this.props.issues,
    id: this.props.id
  };

  async componentDidMount() {
    const issueTypes = await wretch("https://orlenapi.azurewebsites.net/IssueType")
      .get()
      .json();
  }

  render() {
    return (
      <div>
        ISSUES: {this.state.issues.count}
        {this.state.issues.map(issue => {
          return <div>ISZJU</div>;
        })}
        <button>Add</button>
      </div>
    );
  }
}

export default IssueEditor;
