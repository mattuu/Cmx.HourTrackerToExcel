import * as React from "react";
import { container } from "src/inversify.config";
import { AuthenticationService } from "src/services/AuthenticationService";
import { TYPES } from "src/types";
import SignOut from "./auth/SignOut";
import FileUpload from "./file-upload/FileUpload";

const authService: AuthenticationService = container.get<AuthenticationService>(
  TYPES.AuthenticationService
);

interface IState {
  isAuthenticated: boolean;
}

class Home extends React.Component<any, IState> {
  constructor(props: any) {
    super(props);
    this.state = { isAuthenticated: false };
  }

  public componentDidMount() {
    const auth = authService.isAuthenticated;
    this.setState({ isAuthenticated: auth });

    // tslint:disable-next-line:no-console
    console.log(authService.isAuthenticated);
    if (!authService.isAuthenticated) {
      this.props.history.push("signin");
    }
  }

  public render() {
    if (this.state.isAuthenticated === true) {
      return (
        <section>
          <pre>isAuthenticated: {this.state.isAuthenticated}</pre>
          <SignOut />
          <FileUpload />
        </section>
      );
    }

    return (
      <section>
        <h1>Home!</h1>
        <pre>isAuthenticated: {this.state.isAuthenticated}</pre>
      </section>
    );
  }
}

export default Home;
