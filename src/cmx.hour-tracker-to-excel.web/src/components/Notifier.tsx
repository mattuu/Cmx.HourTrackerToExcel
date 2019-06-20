import * as React from 'react';
import { ToastContainer, ToastMessageAnimated } from 'react-toastr';
import 'toastr/build/toastr.min.css';
import { Status } from './Status';

const toastMessageFactory = React.createFactory(ToastMessageAnimated);

interface IProps {
  message: string;
  status: Status;
}

const toastrOptions = {
  className: 'animated',
  hideduration: 300,
  taptodismiss: 'true',
  timeout: 1000
};

class Notifier extends React.Component<IProps, {}> {
  public toastContainerRef: ToastContainer;

  public render() {
    return (
      <ToastContainer
        ref={(r: ToastContainer) => (this.toastContainerRef = r)}
        toastMessageFactory={toastMessageFactory}
        className="toast-bottom-right"
      />
    );
  }

  public componentWillReceiveProps(nextProps: IProps) {
    switch (nextProps.status) {
      case Status.Success:
        this.toastContainerRef.success(nextProps.message, '', toastrOptions);
        break;
      case Status.Failure:
        this.toastContainerRef.error(nextProps.message, '', toastrOptions);
        break;
      default:
        break;
    }
  }
}

export default Notifier;
