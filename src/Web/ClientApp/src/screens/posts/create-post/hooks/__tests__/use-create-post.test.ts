import { renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';

import { Providers } from '../../../../../providers';
import { CreatePostResponse, useCreatePost } from '../use-create-post';

describe('useCreatePost', () => {
  test('creates post and navigates to slug', async () => {
    const response: CreatePostResponse = {
      slug: 'test-post',
    };

    moxios.stubRequest('/api/v1/posts', {
      status: 201,
      response,
    });

    const { result } = renderHook(() => useCreatePost(), { wrapper: Providers });

    await result.current.mutateAsync({
      title: 'test-post',
      body: 'Test post body',
      tags: ['tag-one'],
    });

    expect(window.location.href).contains(response.slug);
  });
});
