import { render } from 'core/testUtilities';

jest.mock('features/HubConnector');

import App from './App';

test('renders App', () => {
  const { container } = render(<App />);
  expect(container.firstChild).toMatchSnapshot();
});
