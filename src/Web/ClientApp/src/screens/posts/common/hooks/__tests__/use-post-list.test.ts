import { renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';

import Providers from '@providers';

import { PostListResponse, usePostList } from '../use-post-list';

describe('usePostList', () => {
  test('returns paginated post list', async () => {
    const response: PostListResponse = {
      hasNextPage: true,
      hasPreviousPage: false,
      items: [
        {
          slug: 'post-one',
          title: 'Post one',
          createdAt: '2022-03-14T08:28:30.959Z',
          author: {
            name: 'John Doe',
            username: 'John',
          },
          tags: [{ name: 'tag-one' }],
          commentsCount: 12,
          likesCount: 265,
        },
      ],
      pageNumber: 1,
      totalCount: 1,
      totalPages: 3,
    };

    moxios.stubRequest(new RegExp('/api/v1/posts.*'), {
      status: 200,
      response,
    });

    const { result, waitForNextUpdate } = renderHook(
      () =>
        usePostList({
          orderBy: 'created',
          pageSize: 1,
        }),
      { wrapper: Providers },
    );

    await waitForNextUpdate();

    expect(result.current).toMatchSnapshot();
  });
});
