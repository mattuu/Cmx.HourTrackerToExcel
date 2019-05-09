import "animate.css/animate.min.css";
import * as React from "react";
import { Route } from "react-router";
import "./App.css";

import Login from "./components/login/login";
import { SigninRedirect } from "./components/signin-redirect";

interface IProps {
  history?: any;
}

class App extends React.Component<IProps> {
  public render() {
    return (
      <div className="App">
        <Route path="/" component={Login} />
        <Route path="/signin-redirect" component={SigninRedirect} />
      </div>
    );
  }
}

export default App;
