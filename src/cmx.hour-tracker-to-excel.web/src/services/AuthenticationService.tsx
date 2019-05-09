import * as Oauth from 'simple-oauth2';

const credentials = {
  auth: {
    tokenHost: "https://api.sandbox.freeagent.com/v2/"
  },
  client: {
    id: "9Ks9-3v6_TX1f2FwiTPn1w",
    secret: "MKc9oIU0GUhE-HGDOeEXBw"
  }
};

// Initialize the OAuth2 Library
// const oauth2 = require("simple-oauth2").create(credentials);

export class AuthenticationService {
  private oauthClient: Oauth.OAuthClient;

  constructor() {
    this.oauthClient = Oauth.create(credentials);

    // this._oauth2 = new OAuth2(
    //   "9Ks9-3v6_TX1f2FwiTPn1w",
    //   "MKc9oIU0GUhE-HGDOeEXBw",
    //   "https://api.sandbox.freeagent.com/v2/",
    //   "approve_app",
    //   "token_endpoint",
    //   { Accept: "application/json" }
    // );
    // const url = this._oauth2.getAuthorizeUrl({
    //   redirect_url: "http://localhost:3000",
    //   response_type: "code"
    // });
  }

  public isAuthenticated(): boolean {
    return this.oauthClient.clientCredentials !== undefined;
  }
}
