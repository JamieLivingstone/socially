import axios from 'axios';
import { useInfiniteQuery } from 'react-query';

type PostList = {
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

type UsePostListOptions = {
  author?: string;
  orderBy?: OrderBy;
  pageSize?: number;
  tag?: string;
};

export function usePostList(options: UsePostListOptions) {
  const { data, hasNextPage, fetchNextPage, isFetchingNextPage } = useInfiniteQuery(
    ['post-list', options],
    async function fetchPostRequest() {
      const { data } = await axios.get<PostList>(`/api/v1/posts`, {
        params: {
          author: options.author,
          orderBy: options.orderBy ?? 'created',
          pageSize: options.pageSize ?? 10,
          tag: options.tag,
        },
      });

      return data;
    },
    { suspense: true },
  );

  return {
    pages: data?.pages ?? [],
    hasNextPage: hasNextPage,
    fetchNextPage,
    isFetchingNextPage,
  };
}
