import { injectable } from "inversify";
import {
  // AccessToken,
  create as createOAuthClient,
  OAuthClient,
  Token
} from "simple-oauth2";

const credentials = {
  auth: {
    authorizePath: "approve_app",
    tokenHost: `${process.env.REACT_APP_AUTH_BASE_URL}`,
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
  // private accessToken: AccessToken | undefined;

  constructor() {
    this.oauthClient = createOAuthClient(credentials);
  }

  public get isAuthenticated(): boolean {
    // return this.accessToken !== undefined && !this.accessToken.expired;
    return sessionStorage.getItem("access_token") !== undefined;
  }

  public get authorizeURL(): string {
    return this.oauthClient.authorizationCode.authorizeURL({
      redirect_uri: REDIRECT_URI
    });
  }

  public async authenticate(code: string): Promise<Token> {
    const tokenConfig = {
      code,
      redirect_uri: REDIRECT_URI
    };
    const result = await this.oauthClient.authorizationCode.getToken(
      tokenConfig
    );

    // this.accessToken = this.oauthClient.accessToken.create(result);

    sessionStorage.setItem("access_token", result.access_token);

    return result;
  }
}
