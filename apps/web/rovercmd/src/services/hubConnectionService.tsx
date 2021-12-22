import { HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { baseUrl } from 'core/apiConfig';

export interface RoverHubConnection {
    onclose: (callback: ((error?: Error | undefined) => void)) => void,
    onreconnecting: (callback: ((error?: Error | undefined) => void)) => void,
    onreconnected: (callback: ((connectionId: String | undefined) => void)) => void,
    on: (methodName: string, newMethod: (...args: any[]) => void) => void,

    start: () => Promise<void>,

    state: HubConnectionState,
    connectionId: string | null
}

export const getHubConnection = (): RoverHubConnection => {
    const newConnection = new HubConnectionBuilder()
        .withUrl(`${baseUrl}/hub`)
        .withAutomaticReconnect()
        .build();

    return newConnection;
}