import "animate.css/animate.css";
import * as React from "react";
import "./App.css";
import Authenticator from "./components/Authenticator";
import FileUpload from "./components/file-upload/FileUpload";

class App extends React.Component<any, any> {
  private authenticator = new Authenticator();

  constructor(props: any) {
    super(props);
    this.state = {};
  }

  public componentDidMount() {
    const url = this.authenticator.getUrl();
    // window.location.href = url;

    this.setState({ url });
  }

  public render() {
    return (
      <div className="App">

        <header className="App-header">
          <h1 className="App-title">Welcome!</h1>
          <pre>{this.state.url}</pre>
        </header>
        <br />
        <FileUpload />
      </div>
    );
  }
}

export default App;
