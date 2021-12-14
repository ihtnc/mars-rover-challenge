import { render } from 'core/testUtilities';
import CommandInput from './index';

test('renders CommandInput', () => {
  const { container } = render(<CommandInput />);
  expect(container.firstChild).toMatchSnapshot();
});
