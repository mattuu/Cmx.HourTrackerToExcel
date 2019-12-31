import axios from 'axios';
import { saveAs } from 'file-saver';
import * as React from 'react';
import { RefObject } from 'react';
import Dropzone from 'react-dropzone';
import { Status } from '../Status';
import Notifier from './../Notifier';
import './file-upload.css';

interface IState {
  message: string;
  status: Status;
  busy: boolean;
}

class FileUpload extends React.Component<{}, IState> {
  public name: string;
  public notifierRef: RefObject<Notifier>;

  private onDrop = this.acceptedFiles.bind(this);

  constructor(props: any) {
    super(props);
    this.state = { message: '', status: Status.Unknown, busy: false };
    this.notifierRef = React.createRef();
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
                {!this.state.busy && (
                  <p>Drag 'n' drop some files here, or click to select files</p>
                )}
                {this.state.busy && <p>Processing file. Please wait&hellip;</p>}
              </div>
            </section>
          )}
        </Dropzone>
        <Notifier
          ref={this.notifierRef}
          message={this.state.message}
          status={this.state.status}
        />
      </div>
    );
  }

  private acceptedFiles(acceptedFiles: File[]) {
    const data = new FormData();
    data.append('formFiles', acceptedFiles[0]);
    data.append('name', acceptedFiles[0].name);
    data.append('description', 'some value user types');

    const state = Object.assign({}, this.state, {
      busy: true
    });

    this.setState(state);

    axios
      .post(process.env.REACT_APP_API_URL + '/file', data, {
        responseType: 'blob'
      })
      .then(
        response => {
          const msg = `File ${acceptedFiles[0].name} uploaded successfully (${
            response.status
            })`;

          const fileName = response.headers['x-filename'];
          saveAs(response.data, fileName);

          const newState = Object.assign({}, this.state, {
            busy: false,
            message: msg,
            status: Status.Success
          });

          this.setState(newState);
        },
        (error: any) => {
          const msg = `There was an error when processing request. ${
            error.message
            }`;
          const newState = Object.assign({}, this.state, {
            busy: false,
            message: msg,
            status: Status.Failure
          });

          this.setState(newState);
        }
      );
  }
}

export default FileUpload;
