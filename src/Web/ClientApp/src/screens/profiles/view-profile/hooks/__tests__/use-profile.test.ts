import { renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';
import { expect } from 'vitest';

import Providers from '@providers';

import { Profile } from '../../types';
import { useProfile } from '../use-profile';

describe('useProfile', () => {
  test('fetches profile', async () => {
    const response: Profile = {
      commentsCount: 2,
      followersCount: 64,
      following: false,
      name: 'John Doe',
      postsCount: 29,
      username: 'john',
    };

    moxios.stubRequest('/api/v1/profiles/john', {
      status: 200,
      response,
    });

    const { result, waitForNextUpdate } = renderHook(() => useProfile('john'), { wrapper: Providers });

    await waitForNextUpdate();

    expect(result.current).toMatchSnapshot();
  });
});
