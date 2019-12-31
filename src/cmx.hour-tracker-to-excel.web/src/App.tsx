/* tslint:disable */

import 'animate.css/animate.css';
import * as React from 'react';
import { create } from 'simple-oauth2';
import './App.css';
import FileUpload from './components/file-upload/FileUpload';

// const authorizationUri = oauth2.authorizationCode.authorizeURL({
//   redirect_uri: 'http://localhost:3000/callback',
//   scope: '<scope>', // also can be an array of multiple scopes, ex. ['<scope1>, '<scope2>', '...']
//   state: '<state>'
// });

const credentials = {
  auth: {
    authorizeHost: 'https://api.sandbox.freeagent.com',
    authorizePath: '/v2/approve_app',
    tokenHost: 'https://api.sandbox.freeagent.com',
    tokenPath: '/v2/token_endpoint'
  },
  client: {
    id: '',
    secret: ''
  }
};

// const tokenConfig = {
//   code: '<code>',
//   redirect_uri: 'http://localhost:3000/callback',
//   scope: 'all users', // also can be an array of multiple scopes, ex. ['<scope1>, '<scope2>', '...']
// };

// const authorizationTokenConfig: AuthorizationTokenConfig = {

// }

class App extends React.Component<any, any> {

  constructor(props: any) {
    super(props);

    this.state = {};


    // const url = create(credentials).authorizationCode.authorizeURL({
    //   redirect_uri: 'http://localhost:3000/callback',
    //   scope: 'all users', // also can be an array of multiple scopes, ex. ['<scope1>, '<scope2>', '...']
    //   state: '<state>'
    // });


    // oauthClient.clientCredentials.getToken(tokenConfig);
    // oauthClient.accessToken.create()
  }

  public componentDidMount() {
    const client = create(credentials);

    try {
      const url = client.authorizationCode.authorizeURL({ client_id: '', redirect_uri: 'http://google.com' });
      this.setState({ url: 'blah!' + url });
    } catch (e) {
      console.log(e);
      const url = new URL(credentials.auth.authorizePath, credentials.auth.authorizeHost);
      this.setState({ url: url.href });
    }
  }

  public render() {
    return (
      <div className="App">
        <header className="App-header">
          <h1 className="App-title">Welcome!</h1>
        </header>
        <br />
        <FileUpload />
        <pre>DEBUG: {this.state.url}</pre>
      </div>
    );
  }
}

export default App;
