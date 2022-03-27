import { renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';

import Providers from '@providers';

import { useDeletePost } from '../use-delete-post';

describe('useDeletePost', () => {
  test('deletes comment', async () => {
    moxios.stubRequest('/api/v1/posts/mock-post', {
      status: 200,
      response: {},
    });

    const { result } = renderHook(() => useDeletePost(), { wrapper: Providers });

    await result.current.deletePost({
      slug: 'mock-post',
    });

    expect(moxios.requests.mostRecent().url).toEqual('/api/v1/posts/mock-post');
    expect(moxios.requests.mostRecent().config.method).toEqual('delete');
  });
});
