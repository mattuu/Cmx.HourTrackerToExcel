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
  }
}

export default FileUpload;
