import { act, renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';

import Providers from '@providers';

import { useToggleLike } from '../use-toggle-like';

describe('useToggleLike', () => {
  test('likes and unlikes post', async () => {
    moxios.stubRequest('/api/v1/posts/test-post/likes', {
      status: 200,
      response: {},
    });

    const { result } = renderHook(
      () =>
        useToggleLike({
          slug: 'test-post',
          isLiked: false,
        }),
      { wrapper: Providers },
    );

    expect(result.current.liked).toEqual(false);

    // Like
    await act(() => result.current.toggle());
    expect(result.current.liked).toEqual(true);
    expect(moxios.requests.mostRecent().config.method).toEqual('post');

    // Unlike
    await act(() => result.current.toggle());
    expect(result.current.liked).toEqual(false);
    expect(moxios.requests.mostRecent().config.method).toEqual('delete');
  });
});
