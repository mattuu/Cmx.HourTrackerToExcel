import * as queryString from "query-string";
import * as React from "react";
import { withRouter } from "react-router-dom";
import Authenticator from "./components/Authenticator";

class SignIn extends React.Component<any, any> {
  private authenticator = new Authenticator();

  public componentDidMount() {
    // const code = this.props.location.query.code;
    const params = queryString.parse(this.props.location.search);

    // tslint:disable-next-line:no-console
    console.log("signIn", params.code);

    const x = this.authenticator.getResponse(`${params.code}`);

    // tslint:disable-next-line:no-console
    console.log(x);
  }

  public render() {
    return "";
  }
}
export default withRouter(SignIn);
