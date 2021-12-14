import { render as reactRender, RenderResult } from '@testing-library/react'
import { configureStore } from '@reduxjs/toolkit'
import { Provider } from 'react-redux'
import roverReducer from './roverSlice';

function render(
  ui: React.ReactElement<any>,
  {
    preloadedState,
    store = configureStore({ reducer: { rover: roverReducer }, preloadedState }),
    ...renderOptions
  }: any = {}
): RenderResult {
  function Wrapper({ children }: any) {
    return <Provider store={store}>{children}</Provider>
  }
  return reactRender(ui, { wrapper: Wrapper, ...renderOptions })
}

// re-export everything
export * from '@testing-library/react'
// override render method
export { render }