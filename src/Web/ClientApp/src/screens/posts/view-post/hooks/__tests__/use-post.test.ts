import { renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';
import { expect } from 'vitest';

import { Providers } from '../../../../../providers';
import { Post } from '../../types';
import { usePost } from '../use-post';

describe('usePost', () => {
  test('returns post', async () => {
    const response: Post = {
      author: {
        name: 'John Doe',
        username: 'john',
      },
      body: '## Post body',
      commentsCount: 4,
      createdAt: '2022-03-14T08:28:30.959Z',
      likesCount: 0,
      slug: 'mock-post',
      tags: [{ name: 'mock-tag' }],
      title: 'Mock post',
      updatedAt: '2022-05-14T05:28:30.959Z',
    };

    moxios.stubRequest('/api/v1/posts/mock-post', {
      status: 200,
      response,
    });

    const { result, waitForNextUpdate } = renderHook(() => usePost('mock-post'), { wrapper: Providers });

    await waitForNextUpdate();

    expect(result.current).toMatchSnapshot();
  });
});
