import * as ClientOAuth2 from "client-oauth2";

export class Authenticator {
  private authClient = new ClientOAuth2({
    accessTokenUri: "https://api.sandbox.freeagent.com/v2/token_endpoint",
    authorizationUri: "https://api.sandbox.freeagent.com/v2/approve_app",
    clientId: "9Ks9-3v6_TX1f2FwiTPn1w",
    clientSecret: "MKc9oIU0GUhE-HGDOeEXBw",
    redirectUri: "http://locahost:3000/signin-redirect",
    scopes: ["user:email"]
  });

  public getUrl = () => {
    const response = this.authClient.code.getUri();
    return response;
  };

  public getResponse = async (url: string) => {
    const response = await this.authClient.code.getToken(url);
    return response;
  };

  public makeRequest(url: string) {
    // const x = this.authClient.code.getToken(
    //   "http://locahost:3000/signin-redirect"
    // );

    // // tslint:disable-next-line:no-console
    // console.log(x);

    //     this.authClient.token.getToken(url).then((user: any) => {
    //       // tslint:disable-next-line:no-console
    //       console.log(user);

    //       const signed = user.sign({method: 'get', url});
    //       // tslint:disable-next-line:no-console
    //       console.log(signed);

    //       // Make a request to the github API for the current user.
    //       //   return popsicle
    //       //     .request(
    //       //       user.sign({
    //       //         method: "get",
    //       //         url: "https://api.github.com/user"
    //       //       })
    //       //     )
    //       //     .then(function(res) {
    //       //       console.log(res); //=> { body: { ... }, status: 200, headers: { ... } }
    //       //     });
    //     });
  }
}

export default Authenticator;
