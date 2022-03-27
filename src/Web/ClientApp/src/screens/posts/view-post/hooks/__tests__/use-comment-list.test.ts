import { renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';
import { expect } from 'vitest';

import Providers from '@providers';

import { CommentList, useCommentList } from '../use-comment-list';

describe('useCommentList', () => {
  test('returns paginated comment list', async () => {
    const response: CommentList = {
      hasNextPage: false,
      hasPreviousPage: false,
      items: [
        {
          id: 1,
          message: 'Comment one',
          createdAt: '2022-03-14T08:28:30.959Z',
          author: {
            username: 'john',
            name: 'John Doe',
          },
        },
      ],
      pageNumber: 1,
      totalCount: 30,
      totalPages: 3,
    };

    moxios.stubRequest(new RegExp('/api/v1/posts/mock-post/comments.*'), {
      status: 200,
      response,
    });

    const { result, waitForNextUpdate } = renderHook(() => useCommentList('mock-post'), { wrapper: Providers });

    await waitForNextUpdate();

    expect(result.current).toMatchSnapshot();
  });
});
