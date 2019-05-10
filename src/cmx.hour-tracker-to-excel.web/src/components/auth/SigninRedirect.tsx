import * as qs from "query-string";
import * as React from "react";
import { container } from "src/inversify.config";
import { AuthenticationService } from "src/services/AuthenticationService";
import { TYPES } from "src/types";

const authService: AuthenticationService = container.get<AuthenticationService>(
  TYPES.AuthenticationService
);

class SigninRedirect extends React.Component<any, any> {
  constructor(params: any) {
    super(params);
  }

  public async componentDidMount() {
    const query: any = qs.parse(this.props.location.search);

    try {
      await authService.authenticate(query.code);
    } catch (error) {
      // tslint:disable-next-line:no-console
      console.log(error);
    }
  }

  public render() {
    return <h1>Authenticated!</h1>;
  }
}

export default SigninRedirect;
