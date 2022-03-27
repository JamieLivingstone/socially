import { act, renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';

import Providers from '@providers';

import { useToggleFollowing } from '../use-toggle-following';

describe('useToggleFollowing', () => {
  test('follows and unfollows profile', async () => {
    moxios.stubRequest('/api/v1/profiles/john/followers', {
      status: 200,
      response: {},
    });

    const { result } = renderHook(
      () =>
        useToggleFollowing({
          isFollowing: false,
          username: 'john',
        }),
      { wrapper: Providers },
    );

    expect(result.current.following).toEqual(false);

    // Follow
    await act(() => result.current.toggle());
    expect(result.current.following).toEqual(true);
    expect(moxios.requests.mostRecent().config.method).toEqual('post');

    // Unfollow
    await act(() => result.current.toggle());
    expect(result.current.following).toEqual(false);
    expect(moxios.requests.mostRecent().config.method).toEqual('delete');
  });
});
