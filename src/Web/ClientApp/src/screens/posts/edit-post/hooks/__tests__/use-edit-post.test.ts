import { renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';

import Providers from '@providers';

import { useEditPost } from '../use-edit-post';

describe('useEditPost', () => {
  test('edits post and navigates to slug', async () => {
    moxios.stubRequest('/api/v1/posts/test-post', {
      status: 200,
      response: {},
    });

    const { result } = renderHook(() => useEditPost(), { wrapper: Providers });

    await result.current.editPost({
      title: 'Test Post',
      body: 'Test post body',
      slug: 'test-post',
      tags: ['tag-one'],
    });

    expect(moxios.requests.mostRecent().config.data).toMatchSnapshot();
    expect(window.location.href).contains('test-post');
  });
});
