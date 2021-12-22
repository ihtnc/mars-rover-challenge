import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import type { RootState } from './store';

interface HubConnectionState {
    connectionId: string | null
};

const initialState: HubConnectionState = {
    connectionId: null
};

export const hubConnectionSlice = createSlice({
  name: 'hubConnection',
  initialState,
  reducers: {
    setConnectionId: (state, action: PayloadAction<string | null>) => {
      state.connectionId = action.payload
    }
  },
});

export const { setConnectionId } = hubConnectionSlice.actions;

export const getConnectionId = (state: RootState) => state.hubConnection.connectionId;

export default hubConnectionSlice.reducer;