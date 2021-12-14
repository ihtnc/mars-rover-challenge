import { configureStore } from '@reduxjs/toolkit';
import roverReducer from './roverSlice';

const store = configureStore({
  reducer: {
    rover: roverReducer
  }
});

export default store;

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;