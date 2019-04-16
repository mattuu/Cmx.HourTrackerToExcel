import axios from 'axios';
import * as React from 'react';
import { RefObject } from 'react';
import Dropzone from 'react-dropzone';
import Notifier from '../notifier';
import './file-upload.css';

interface IState {
  message: string;
}

class FileUpload extends React.Component<{}, IState> {
  public name: string;
  public ref: RefObject<Notifier>;

  private onDrop = this.acceptedFiles.bind(this);

  constructor(props: any) {
    super(props);
    this.state = { message: '' };
    this.ref = React.createRef();
  }

  public notify(msg: string) {
    return msg;
  }

  public render() {
    return (
      <div>
        <Dropzone onDrop={this.onDrop}>
          {({ getRootProps, getInputProps }) => (
            <section>
              <div className="dropzone" {...getRootProps()}>
                <input {...getInputProps()} />
                <p>Drag 'n' drop some files here, or click to select files</p>
              </div>
            </section>
          )}
        </Dropzone>
        <Notifier ref={this.ref} message={this.state.message} />
      </div>
    );
  }

  private acceptedFiles(acceptedFiles: File[]) {
    const data = new FormData();
    data.append('formFiles', acceptedFiles[0]);
    data.append('name', acceptedFiles[0].name);
    data.append('description', 'some value user types');

    axios.post('http://localhost:41006/api/file', data).then(response => {
      const msg = `File ${acceptedFiles[0].name} uploaded successfully (${
        response.status
      })`;

      const newState = Object.assign({}, this.state, {
        message: msg
      });

      this.setState(newState);
    });
  }
}

export default FileUpload;
