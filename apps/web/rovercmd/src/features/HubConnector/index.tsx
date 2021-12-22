import { useState, useEffect } from 'react'
import { useAppSelector, useAppDispatch } from 'core/hooks';
import { setConnectionId, getConnectionId } from 'core/hubConnectionSlice';
import { HubConnectionState } from '@microsoft/signalr';
import { RoverHubConnection, getHubConnection } from 'services/hubConnectionService';
import './style.css';

function HubConnector() {
  const [ connection, setConnection ] = useState(null as unknown as RoverHubConnection);
  const [ status, setStatus ] = useState(HubConnectionState.Disconnected);
  const connectionId = useAppSelector(getConnectionId);
  const dispatch = useAppDispatch();

  useEffect(() => {
    initialiseHub();
  }, []);

  useEffect(() => {
    configureHub();
    connectToHub();
  }, [connection]);

  const initialiseHub = () => {
    const hubConnection = getHubConnection();
    setConnection(hubConnection);
  };

  const configureHub = () => {
    if (!connection) { return; }

    connection.onclose(updateStatus);
    connection.onreconnecting(updateStatus);
    connection.onreconnected(updateStatus);

    connection.on('ReceiveUpdate', (update: String) => {
      console?.log(`Received update! ${update}`);
    });
  };

  const connectToHub = () => {
    if (connection) {
      try {
        connection.start().finally(updateStatus);
      }
      catch {
        updateStatus();
      }
    }
  };

  const updateStatus = () => {
    if (connection) {
      setStatus(connection.state);
    }

    dispatch(setConnectionId(connection?.connectionId));
  };

  return (
    <div className="HubConnector" title={connectionId ? `id: ${connectionId}` : undefined}>
      {`${status}`}
      <a href='/' target='_self'
        hidden={status !== HubConnectionState.Disconnected}
        onClick={(e) => {
          e.preventDefault();
          connectToHub();
        }}>Reconnect?</a>
    </div>
  );
}

export default HubConnector;