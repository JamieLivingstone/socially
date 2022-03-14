import { act, renderHook } from '@testing-library/react-hooks';
import moxios from 'moxios';
import { expect } from 'vitest';

import { TagListResponse, useTagList } from '../';
import { Providers } from '../../../../../providers';

describe('useTagList', () => {
  test('returns paginated tag list when a filter is specified', async () => {
    const response: TagListResponse = {
      hasNextPage: true,
      hasPreviousPage: false,
      items: [{ name: 'tag-one' }],
      pageNumber: 1,
      totalCount: 3,
      totalPages: 3,
    };

    moxios.stubRequest(new RegExp('/api/v1/tags.*'), {
      status: 200,
      response,
    });

    const { result, waitFor } = renderHook(() => useTagList(), { wrapper: Providers });

    expect(result.current.tags.length).toEqual(0);

    act(() => result.current.setFilter('tag-o'));

    await waitFor(() => result.current.tags.length > 0);

    expect(result.current).toMatchSnapshot();
  });
});
