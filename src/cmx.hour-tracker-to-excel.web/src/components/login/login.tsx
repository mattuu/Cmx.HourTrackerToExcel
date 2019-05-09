// import { OAuth2 } from "oauth";
import * as React from "react";
import * as Oauth from "simple-oauth2";

interface IState {
  authorizeUrl: string;
  statusCode: number | undefined;
  message: string | undefined;
}

const credentials = {
  auth: {
    authorizePath: "approve_app",
    tokenHost: "https://api.sandbox.freeagent.com/v2/",
    tokenPath: "token_endpoint"
  },
  client: {
    id: "9Ks9-3v6_TX1f2FwiTPn1w",
    secret: "MKc9oIU0GUhE-HGDOeEXBw"
  }
};

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
    const oauthClient = Oauth.create(credentials);

    const authorizationUri = oauthClient.authorizationCode.authorizeURL({
      redirect_uri: "http://localhost:3000/signin-redirect"
    });
    this.setState({ authorizeUrl: authorizationUri });

    // const oauth2 = new OAuth2(
    //   "9Ks9-3v6_TX1f2FwiTPn1w",
    //   "MKc9oIU0GUhE-HGDOeEXBw",
    //   "https://api.sandbox.freeagent.com/v2/",
    //   "approve_app",
    //   "token_endpoint",
    //   { Accept: "application/json" }
    // );
    // const url = oauth2.getAuthorizeUrl({
    //   redirect_url: "http://localhost:3000",
    //   response_type: "code"
    // });
    // this.setState({ authorizeUrl: url });

    // window.setTimeout(() => {
    //   window.location.href = url;
    // }, 1000);

    // oauth2.get(url, "", e => {
    //   const state: IState = this.state;
    //   state.message = e.data;
    //   state.statusCode = e.statusCode;
    //   this.setState(state);
    // });

    // oauth2.getOAuthAccessToken(
    //   "",
    //   {
    //     grant_type: "authorization_code"
    //   },
    //   // tslint:disable-next-line:variable-name
    //   (e, access_token, refresh_token, results) => {
    //     // tslint:disable-next-line:no-console
    //     console.log(e, access_token, refresh_token, results);
    //     const state: IState = this.state;
    //     state.message = e.data;
    //     state.statusCode = e.statusCode;
    //     this.setState(state);
    //     // tslint:disable-next-line:no-console
    //     console.log("bearer: ", access_token);
    //   }
    // );

    // oauth2.getOAuthAccessToken(
    //   "",
    //   { grant_type: "client_credentials" },
    //   function(e, access_token, refresh_token, results) {
    //     console.log("bearer: ", access_token);
    //     done();
    //   }
    // );
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
