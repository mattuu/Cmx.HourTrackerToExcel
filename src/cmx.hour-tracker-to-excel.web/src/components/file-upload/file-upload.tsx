import axios from 'axios';
import * as React from 'react';
import Dropzone from 'react-dropzone';
import './file-upload.css';

class FileUpload extends React.Component {
  public name: string;

  public render() {
    return (
      <Dropzone onDrop={this.acceptedFiles}>
        {({ getRootProps, getInputProps }) => (
          <section>
            <div className="dropzone" {...getRootProps()}>
              <input {...getInputProps()} />
              <p>Drag 'n' drop some files here, or click to select files</p>
            </div>
          </section>
        )}
      </Dropzone>
    );
  }

  private acceptedFiles(acceptedFiles: File[]) {
    // tslint:disable-next-line:no-console
    console.log(acceptedFiles);

    const data = new FormData();
    data.append('formFiles', acceptedFiles[0]);
    data.append('name', acceptedFiles[0].name);
    data.append('description', 'some value user types');
    // '/files' is your node.js route that triggers our middleware
    axios.post('http://localhost:41006/api/file', data).then(response => {
      // tslint:disable-next-line:no-console
      console.log(response); // do something with the response
    });
  }
}

export default FileUpload;
