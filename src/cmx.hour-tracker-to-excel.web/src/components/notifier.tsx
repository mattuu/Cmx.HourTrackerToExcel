import * as React from 'react';
import { ToastContainer, ToastMessageAnimated } from 'react-toastr';
import 'toastr/build/toastr.min.css';

const toastMessageFactory = React.createFactory(ToastMessageAnimated);

interface IProps {
  message: string;
}

class Notifier extends React.Component<IProps, {}> {
  public ref: ToastContainer;

  public set message(val: string) {
    this.ref.info(val, '');
  }

  public render() {
    return (
      <ToastContainer
        ref={(r: ToastContainer) => (this.ref = r)}
        toastMessageFactory={toastMessageFactory}
        className="toast-top-center"
      />
    );
  }

  public componentWillReceiveProps(nextProps: IProps) {
    this.ref.success(nextProps.message, '', {
      hideduration: 300,
      taptodismiss: 'true'
    });
  }
}

export default Notifier;
