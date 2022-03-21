import axios from 'axios';
import { useState } from 'react';
import { useInfiniteQuery } from 'react-query';

export type PostListResponse = {
  hasNextPage: boolean;
  hasPreviousPage: false;
  items: Array<{
    slug: string;
    title: string;
    createdAt: string;
    author: {
      name: string;
      username: string;
    };
    tags: [
      {
        name: string;
      },
    ];
    commentsCount: number;
    likesCount: number;
  }>;
  pageNumber: number;
  totalCount: number;
  totalPages: number;
};

type OrderBy = 'created' | 'commentsCount' | 'likesCount';

export type UsePostListOptions = {
  author?: string;
  orderBy?: OrderBy;
  pageSize?: number;
  tag?: string;
};

export function usePostList(options: UsePostListOptions) {
  const [orderBy, setOrderBy] = useState<OrderBy>(options.orderBy ?? 'created');

  const { data, hasNextPage, fetchNextPage, isFetchingNextPage } = useInfiniteQuery(
    ['post-list', orderBy, options],
    async function fetchPostRequest() {
      const { data } = await axios.get<PostListResponse>(`/api/v1/posts`, {
        params: {
          author: options.author,
          orderBy: orderBy,
          pageSize: options.pageSize ?? 10,
          tag: options.tag,
        },
      });

      return data;
    },
    {
      suspense: true,
      getNextPageParam(lastPageResponse) {
        return lastPageResponse.hasNextPage ? lastPageResponse.pageNumber + 1 : false;
      },
    },
  );

  return {
    pages: data?.pages ?? [],
    hasNextPage: hasNextPage,
    fetchNextPage,
    isFetchingNextPage,
    setOrderBy,
  };
}
