import { act, renderHook } from '@testing-library/react-hooks';
import * as moxios from 'moxios';

import { TagListResponse, useTagList } from '../use-tag-list';
import { wrapper } from './utils';

describe('useTagList', () => {
  beforeEach(() => {
    moxios.install();
  });

  afterEach(() => {
    moxios.uninstall();
  });

  describe('given no search filter', () => {
    test('does not fetch tags', () => {
      const { result } = renderHook(() => useTagList(), { wrapper });

      expect(result.current.isLoading).toEqual(false);
      expect(moxios.requests.count()).toEqual(0);
    });
  });

  describe('given a search filter', () => {
    test('fetches tags', async () => {
      const response: TagListResponse = {
        items: [{ name: 'tag-one' }],
        pageNumber: 1,
        totalPages: 1,
        totalCount: 1,
        hasPreviousPage: false,
        hasNextPage: false,
      };

      moxios.stubRequest(new RegExp('/api/v1/tags.*'), {
        status: 200,
        response,
      });

      const { result, waitForNextUpdate } = renderHook(() => useTagList(), { wrapper });

      act(() => result.current.setFilter('tag-on'));

      await waitForNextUpdate();

      expect(moxios.requests.mostRecent().url).toMatchSnapshot();
      expect(result.current.tags).toMatchSnapshot();
    });
  });
});
