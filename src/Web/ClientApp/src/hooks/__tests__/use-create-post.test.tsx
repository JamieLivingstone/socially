import { act, renderHook } from '@testing-library/react-hooks';
import * as moxios from 'moxios';

import { CreatePostResponse, useCreatePost } from '../use-create-post';
import { wrapper } from './utils';

describe('useCreatePost', () => {
  beforeEach(() => {
    moxios.install();
  });

  afterEach(() => {
    moxios.uninstall();
  });

  describe('given a valid payload', () => {
    test('creates post and redirects', async () => {
      const response: CreatePostResponse = {
        slug: 'mock-slug',
      };

      moxios.stubRequest(new RegExp('/api/v1/posts'), {
        status: 201,
        response,
      });

      const { result } = renderHook(() => useCreatePost(), { wrapper });

      await act(() =>
        result.current.mutateAsync({
          title: 'Post title',
          body: 'Post body',
          tags: ['tag-one'],
        }),
      );

      expect(moxios.requests.count()).toEqual(1);
      expect(location.pathname).toContain(response.slug);
    });
  });
});
