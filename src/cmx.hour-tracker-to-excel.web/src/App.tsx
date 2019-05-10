import "animate.css/animate.min.css";
import * as React from "react";
import { Route } from "react-router";
import { BrowserRouter as Router } from "react-router-dom";

import Login from "./components/auth/Login";
import SigninRedirect from "./components/auth/SigninRedirect";
import SignOut from "./components/auth/SignOut";
import Home from "./components/Home";

class App extends React.Component {
  public render() {
    return (
      <div className="App">
        <Router>
          <Route path="/" component={Home} />
          <Route path="/signin" component={Login} />
          <Route path="/signin-redirect" component={SigninRedirect} />
          <Route path="/signout" component={SignOut} />
        </Router>
      </div>
    );
  }
}

export default App;
