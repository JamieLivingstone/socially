import axios from 'axios';
import { useState } from 'react';
import { useQuery } from 'react-query';
import { useDebouncedCallback } from 'use-debounce';

export type TagListResponse = {
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  items: Array<{ name: string }>;
  pageNumber: number;
  totalCount: number;
  totalPages: number;
};

export function useTagList() {
  const [filter, setFilter] = useState('');

  const { data, isLoading, isError, error, refetch } = useQuery(
    ['tagList', filter],
    async function fetchTagListRequest() {
      const { data } = await axios.get<TagListResponse>('/api/v1/tags', {
        params: {
          filter,
          pageNumber: 1,
          pageSize: 10,
        },
      });

      return data;
    },
    { enabled: filter.length > 0, suspense: false },
  );

  return {
    setFilter: useDebouncedCallback(setFilter, 300),
    tags: data?.items ?? [],
    isLoading,
    isError,
    error,
    refetch,
  };
}
