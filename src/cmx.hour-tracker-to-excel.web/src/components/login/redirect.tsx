import * as React from "react";

interface IProps {
  login_redirect_url: string | undefined;
}

export class LoginRedirect extends React.Component<IProps> {
  public render() {
    window.setTimeout(() => {
      window.location.href = this.props.login_redirect_url || "";
    }, 5000);

    return null;
  }
}
