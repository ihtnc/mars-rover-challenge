import { configureStore } from '@reduxjs/toolkit';
import roverReducer from './roverSlice';
import hubConnectionReducer from './hubConnectionSlice';

const store = configureStore({
  reducer: {
    rover: roverReducer,
    hubConnection: hubConnectionReducer
  }
});

export default store;

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;