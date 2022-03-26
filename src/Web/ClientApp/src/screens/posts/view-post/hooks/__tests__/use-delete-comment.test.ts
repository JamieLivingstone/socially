import { renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';

import { Providers } from '../../../../../providers';
import { useDeleteComment } from '../use-delete-comment';

describe('useDeleteComment', () => {
  test('deletes comment', async () => {
    moxios.stubRequest('/api/v1/posts/mock-post/comments/1', {
      status: 200,
      response: {},
    });

    const { result } = renderHook(() => useDeleteComment(), { wrapper: Providers });

    await result.current.deleteComment({
      commentId: 1,
      slug: 'mock-post',
    });

    expect(moxios.requests.mostRecent().url).toEqual('/api/v1/posts/mock-post/comments/1');
    expect(moxios.requests.mostRecent().config.method).toEqual('delete');
  });
});
