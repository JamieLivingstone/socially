import { renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';

import { Providers } from '../../../../../providers';
import { useCreateComment } from '../use-create-comment';

describe('useCreateComment', () => {
  test('creates comment', async () => {
    moxios.stubRequest('/api/v1/posts/mock-post/comments', {
      status: 200,
      response: {},
    });

    const { result } = renderHook(() => useCreateComment(), { wrapper: Providers });

    await result.current.createComment({
      slug: 'mock-post',
      message: 'Test comment!',
    });

    expect(moxios.requests.mostRecent().config.data).toMatchSnapshot();
  });
});
