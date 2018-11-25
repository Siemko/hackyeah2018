import React, { PureComponent } from "react";

class IssueEditor extends PureComponent {
  state = {
    issues: this.props.issues,
    id: this.props.id
  };

  render() {
    return (
      <div>
        {this.state.issues.map(issue => {
          return <div>ISZJU</div>;
        })}
      </div>
    );
  }
}

export default IssueEditor;
