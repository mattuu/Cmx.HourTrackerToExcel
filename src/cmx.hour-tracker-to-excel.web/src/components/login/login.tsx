import * as React from "react";
import { container } from "../../inversify.config";
import { AuthenticationService } from "../../services/AuthenticationService";
import { TYPES } from "../../types";

interface IState {
  authorizeUrl: string;
  statusCode: number | undefined;
  message: string | undefined;
}

const authService: AuthenticationService = container.get<AuthenticationService>(TYPES.AuthenticationService);

class Login extends React.Component<{}, IState> {
  public name: string;

  constructor(props: any) {
    super(props);
    this.state = {
      authorizeUrl: "",
      message: undefined,
      statusCode: undefined
    };
  }

  public componentDidMount() {
    this.setState({ authorizeUrl: authService.authorizeURL, message: `${authService.isAuthenticated()}` });
  }

  public render() {
    return (
      <span style={{ textAlign: "left" }}>
        <a href={this.state.authorizeUrl}>Login</a>
        <pre>Authorize URL: {this.state.authorizeUrl}</pre>
        <pre>Status: {this.state.statusCode}</pre>
        <pre>Message: {this.state.message}</pre>
      </span>
    );
  }
}

export default Login;
