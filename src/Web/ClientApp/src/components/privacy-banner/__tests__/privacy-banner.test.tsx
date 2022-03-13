import { fireEvent, render } from '@testing-library/react';
import React from 'react';

import { PrivacyBanner } from '../privacy-banner';

describe('<PrivacyBanner />', () => {
  test('shows privacy banner and closes on button acceptance', async () => {
    const { queryByText, findByRole } = render(<PrivacyBanner />);

    expect(queryByText(/your privacy/i)).not.toBeNull();

    fireEvent.click(await findByRole('button'));

    expect(queryByText(/your privacy/i)).toBeNull();
  });
});
