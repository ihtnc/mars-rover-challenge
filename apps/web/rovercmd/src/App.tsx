import './App.css';
import CommandInput from 'features/CommandInput/';
import CommandOutput from 'features/CommandOutput/';
import HubConnector from 'features/HubConnector';
import { useAppSelector } from 'core/hooks';
import { getStatus } from 'core/roverSlice';
import { Status } from 'core/statusEnum';

const hasError = (status: Status) => status === Status.Error;

function App() {
  const status = useAppSelector(getStatus);

  return (
    <div className="App">

      {hasError(status)
        ? <section className='App-error'>Error!</section>
        : null}

      <header className="App-header">
        Mars Rover Challenge Web App
      </header>
      <section className="App-sub-header">
        <p>A web-based wrapper for the <code>MarsRoverChallenge.Send</code> library.</p>
        <p>
          This library is an implementation of the <a href='https://code.google.com/archive/p/marsrovertechchallenge/'
            target='_blank'
            rel="noreferrer">
            Mars Rover
          </a> challenge.
        </p>
      </section>
      <section className="App-section">
        <CommandInput />
      </section>
      <section className="App-section">
        <CommandOutput />
      </section>
      <section className="App-section">
        <HubConnector />
      </section>
    </div>
  );
}

export default App;
