import axios from 'axios';
import * as FileSaver from 'file-saver';
import * as React from 'react';

class FileUpload extends React.Component {
  public name: string;
  private file: any;

  constructor(props: any) {
    super(props);

    this.handleFileChange = this.handleFileChange.bind(this);
    this.handleUploadFile = this.handleUploadFile.bind(this);
    this.handleGet = this.handleGet.bind(this);
  }

  public handleFileChange = (event: any) => {
    this.file = event.target.files[0];
  };

  public handleUploadFile = (event: any) => {
    const data = new FormData();
    data.append('file', this.file);
    data.append('name', this.file.name);
    data.append('description', 'some value user types');
    // '/files' is your node.js route that triggers our middleware

    // const blob = new Blob([response], { type: mimeType });

    // let options = this.buildRequestOptions();
    // if (params !== undefined) {
    //     let parameters = this.buildParams(params);
    //     options.params = parameters;
    // }
    // options.responseType = ResponseContentType.Blob;
    // return this._http.get(this.buildUrl(url), options)
    //     .map((response: Response) => this.processResponse<any>(response))
    //     .catch(err => this.handleError(err));

    axios({
      data,
      method: 'post',
      responseType: 'blob',
      url: 'http://localhost:41005/api/file',
      withCredentials: false
    }).then((response: any) => {
      // tslint:disable-next-line:no-console
      console.log(response); // do something with the response
      // tslint:disable-next-line:no-console
      console.log(response.headers.contentType);
      const blob = new Blob([response], { type: response.data.type });
      FileSaver.saveAs(blob, this.file.name);
    });
  };

  public handleGet = (event: any) => {
    // form
    // axios({
    //   method: "get",
    //   url: "http://localhost:41005/api/file",
    //   withCredentials: false
    // }).then((response: any) => {
    //   // tslint:disable-next-line:no-console
    //   console.log(response); // do something with the response
    // });
  };

  public downloadFile(absoluteUrl: string) {
    const link = document.createElement('a');
    link.href = absoluteUrl;
    link.download = 'true';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    // delete link;
  }

  public render() {
    return (
      <div>
        <input id="fileInput" type="file" onChange={this.handleFileChange} />
        <button type="button" onClick={this.handleUploadFile}>
          Upload
        </button>
      </div>
    );
  }
}

export default FileUpload;
