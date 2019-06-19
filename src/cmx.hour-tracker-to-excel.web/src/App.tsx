import * as React from 'react';
import './App.css';

import FileUpload from './components/file-upload/FileUpload';

class App extends React.Component {
  public render() {
    return (
      <div className="App">
        <header className="App-header">
          <h1 className="App-title">Welcome!</h1>
        </header>
        <br />
        <FileUpload />
      </div>
    );
  }
}

export default App;
