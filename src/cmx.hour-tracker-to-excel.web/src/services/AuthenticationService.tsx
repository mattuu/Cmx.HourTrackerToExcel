import { injectable } from "inversify";
import { create as createOAuthClient, OAuthClient, Token } from "simple-oauth2";

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

const REDIRECT_URI = `${process.env.REACT_APP_BASE_URL}/signin-redirect`;
@injectable()
export class AuthenticationService {
  private oauthClient: OAuthClient;

  constructor() {
    this.oauthClient = createOAuthClient(credentials);
  }

  public isAuthenticated(): boolean {
    return this.oauthClient.clientCredentials !== undefined;
  }

  public get authorizeURL(): string {
    return this.oauthClient.authorizationCode.authorizeURL({
      redirect_uri: REDIRECT_URI
    });
  }

  public authenticate(code: string): Promise<Token> {
    return this.oauthClient.authorizationCode.getToken({
      code,
      redirect_uri: REDIRECT_URI
    });
    // return this.oauthClient.accessToken.create({ code });
  }
}
