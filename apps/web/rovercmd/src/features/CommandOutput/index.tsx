import { useAppSelector } from 'core/hooks';
import { getOutput } from 'core/roverSlice';
import './style.css';

function CommandOutput() {
    const outputValue = useAppSelector(getOutput);

    return (
      <div className="CommandOutput">
        <fieldset>
          <legend>Output:</legend>
          <textarea
            rows={5}
            value={outputValue}
            readOnly />
        </fieldset>
      </div>
    );
  }

  export default CommandOutput;