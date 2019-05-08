import { OAuth2 } from "oauth";

export class AuthenticationService {
  private _oauth2: OAuth2;

  constructor() {
    this._oauth2 = new OAuth2(
      "9Ks9-3v6_TX1f2FwiTPn1w",
      "MKc9oIU0GUhE-HGDOeEXBw",
      "https://api.sandbox.freeagent.com/v2/",
      "approve_app",
      "token_endpoint",
      { Accept: "application/json" }
    );
    const url = this._oauth2.getAuthorizeUrl({
      redirect_url: "http://localhost:3000",
      response_type: "code"
    });
  }

  public isAuthenticated(): boolean {
    return false;
  }
}
