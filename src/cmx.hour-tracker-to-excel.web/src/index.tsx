import * as React from "react";
import * as ReactDOM from "react-dom";
import { BrowserRouter as Router, Route } from "react-router-dom";
import App from "./App";
import "./index.css";
import registerServiceWorker from "./registerServiceWorker";
import SignIn from "./SignIn";

ReactDOM.render(
  <Router>
    <Route path="/" component={App} />
    <Route path="/signin-redirect" component={SignIn} />
  </Router>,
  document.getElementById("root") as HTMLElement
);
registerServiceWorker();
