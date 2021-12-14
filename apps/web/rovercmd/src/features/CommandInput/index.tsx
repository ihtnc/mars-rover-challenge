import { useState } from 'react';
import { useAppDispatch, useAppSelector } from 'core/hooks';
import { sendInput, Status, getStatus } from 'core/roverSlice';
import './style.css';

const isBusy = (status: Status) => status === Status.Busy;

function CommandInput() {
    const status = useAppSelector(getStatus);

    const dispatch = useAppDispatch();
    const [currentValue, setCurrentValue] = useState('');

    return (
      <div className="CommandInput">
        <fieldset>
          <legend>Enter command(s) to send to the rover(s):</legend>
          <textarea
            rows={10}
            onInput={e => {
              const target = e.currentTarget as HTMLTextAreaElement;
              setCurrentValue(target.value);
            }}
            value={currentValue} />

        </fieldset>
        <button
          disabled={isBusy(status)}
          onClick={() => dispatch(sendInput(currentValue))}>Send!</button>
      </div>
    );
  }

  export default CommandInput;