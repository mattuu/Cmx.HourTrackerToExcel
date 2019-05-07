import axios from "axios";
import { saveAs } from "file-saver";
import * as React from "react";
import { RefObject } from "react";
import Dropzone from "react-dropzone";
import Notifier from "../notifier";
import "./file-upload.css";

interface IState {
  message: string;
}

class FileUpload extends React.Component<{}, IState> {
  public name: string;
  public ref: RefObject<Notifier>;

  private onDrop = this.acceptedFiles.bind(this);

  constructor(props: any) {
    super(props);
    this.state = { message: "" };
    this.ref = React.createRef();

    // tslint:disable-next-line:no-console
    console.log(process.env.REACT_APP_API_URL);
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
    data.append("formFiles", acceptedFiles[0]);
    data.append("name", acceptedFiles[0].name);
    data.append("description", "some value user types");

    axios
      .post(process.env.REACT_APP_API_URL + "/file", data, { responseType: "blob" })
      .then(response => {
        // tslint:disable-next-line:no-console
        console.log(response);

        const msg = `File ${acceptedFiles[0].name} uploaded successfully (${
          response.status
        })`;

        const fileName = response.headers["x-filename"];
        saveAs(response.data, fileName);

        const newState = Object.assign({}, this.state, {
          message: msg
        });

        this.setState(newState);
      });
  }
}

export default FileUpload;
