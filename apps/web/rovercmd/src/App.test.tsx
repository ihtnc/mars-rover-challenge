import { render } from 'core/testUtilities';
import App from './App';

test('renders App', () => {
  const { container } = render(<App />);
  expect(container.firstChild).toMatchSnapshot();
});
