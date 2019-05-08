import 'animate.css/animate.min.css';
import * as React from 'react';
import './App.css';
import FileUpload from './components/file-upload/FileUpload';
import { Route } from "react-router";

import Login from "./components/login/login";

interface IProps {
  history?: any;
}

class App extends React.Component {
  public render() {
    return (
      <div className="App">
        <Route path="/" component={Login} />
      </div>
    );
  }
  }
}

export default App;
