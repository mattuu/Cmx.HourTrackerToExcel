import axios from "axios";
import * as qs from "query-string";
import * as React from "react";
import { container } from "src/inversify.config";
import { AuthenticationService } from "src/services/AuthenticationService";
import { TYPES } from "src/types";

const authService: AuthenticationService = container.get<AuthenticationService>(
  TYPES.AuthenticationService
);

export class SigninRedirect extends React.Component<any, any> {
  constructor(params: any) {
    super(params);
    this.state = {};
  }

  public async componentDidMount() {
    const query: any = qs.parse(this.props.location.search);

    try {
      const token = await authService.authenticate(query.code);
      // tslint:disable-next-line:no-console
      console.log(token);
      this.setState({ accessToken: token.accessToken });

      axios.post(`${process.env.REACT_APP_API_URL}/app`, { ...token });
    } catch (error) {
      // tslint:disable-next-line:no-console
      console.log(error);
    }
  }

  public render() {
    return <h1>Authorization complete! {this.state.accessToken}</h1>;
  }
}

export default SigninRedirect;
