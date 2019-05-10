import * as React from "react";

class SignOut extends React.Component<any, any> {
  private onSignOutClick: any;

  constructor(props: any) {
    super(props);

    this.onSignOutClick = this.signOutClick.bind(this);
  }

  public render() {
    return <button onClick={this.onSignOutClick}>Sing-Out</button>;
  }

  private signOutClick() {
    sessionStorage.removeItem("access_token");
  }
}

export default SignOut;
