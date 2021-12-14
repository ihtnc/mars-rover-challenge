import { render } from 'core/testUtilities';
import CommandOutput from './index';

test('renders CommandOutput', () => {
  const { container } = render(<CommandOutput />);
  expect(container.firstChild).toMatchSnapshot();
});
