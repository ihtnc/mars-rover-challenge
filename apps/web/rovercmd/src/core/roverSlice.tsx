import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import type { AppDispatch, RootState } from './store';
import { postInput } from 'services/sendService';

export enum Status {
  Ready = 0,
  Busy = 1,
  Error = -1
}

interface RoverState {
    status: Status,
    output: string
};

const initialState: RoverState = {
    status: Status.Ready,
    output: ''
};

export const roverSlice = createSlice({
  name: 'rover',
  initialState,
  reducers: {
    setOutput: (state, action: PayloadAction<string>) => {
      state.output = action.payload
    },
    setStatus: (state, action: PayloadAction<Status>) => {
      state.status = action.payload
    }
  },
});

export const { setOutput , setStatus } = roverSlice.actions;

export const sendInput = (command: string) => (dispatch: AppDispatch) => {
  dispatch(setStatus(Status.Busy));
  dispatch(setOutput(''));

  postInput(command)
    .then(response => {
      dispatch(setOutput(response));
      dispatch(setStatus(Status.Ready));
    })
    .catch(error => {
      dispatch(setOutput(''));
      dispatch(setStatus(Status.Error));
    });
};

export const getOutput = (state: RootState) => state.rover.output;
export const getStatus = (state: RootState) => state.rover.status;

export default roverSlice.reducer;