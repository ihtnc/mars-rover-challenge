import { render } from 'core/testUtilities';
import { getHubConnection } from 'services/hubConnectionService';
import { HubConnectionState } from '@microsoft/signalr';
import { RoverHubConnection } from 'services/hubConnectionService';

jest.mock('services/hubConnectionService');

import HubConnector from './index';

let mockGetHubConnection: jest.Mock;
let mockHubConnection: RoverHubConnection;

beforeEach(() => {
  mockHubConnection = {
    onclose: jest.fn(),
    onreconnecting: jest.fn(),
    onreconnected: jest.fn(),
    on: jest.fn(),

    start: jest.fn(),

    state: HubConnectionState.Disconnected,
    connectionId: null
  }

  mockGetHubConnection = getHubConnection as jest.Mock;
  mockGetHubConnection.mockReturnValue(mockHubConnection);
});

test('renders initial HubConnector correctly', () => {
  const { container } = render(<HubConnector />);

  expect(container.firstChild).toMatchSnapshot();
});

test('renders HubConnector with active connection correctly', () => {
  mockHubConnection.state = HubConnectionState.Connected;
  mockHubConnection.connectionId = 'connectionId';
  const mockStart = mockHubConnection.start as jest.Mock;

  const { container } = render(<HubConnector />);

  mockStart.mockResolvedValue({});

  expect(container.firstChild).toMatchSnapshot();
});

test('renders HubConnector with reconnecting state', () => {
  mockHubConnection.state = HubConnectionState.Reconnecting;
  const mockStart = mockHubConnection.start as jest.Mock;

  const { container } = render(<HubConnector />);

  mockStart.mockResolvedValue({});

  expect(container.firstChild).toMatchSnapshot();
});

test('renders HubConnector with connection error', () => {
  mockHubConnection.state = HubConnectionState.Disconnected;
  const mockStart = mockHubConnection.start as jest.Mock;

  const { container } = render(<HubConnector />);

  mockStart.mockImplementation(() => { throw new Error("Error"); });

  expect(container.firstChild).toMatchSnapshot();
});
