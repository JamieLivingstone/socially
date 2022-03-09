import axios from 'axios';
import { useInfiniteQuery } from 'react-query';

type CommentList = {
  hasNextPage: boolean;
  hasPreviousPage: false;
  items: Array<{
    author: {
      name: string;
      username: string;
    };
    createdAt: string;
    id: number;
    message: string;
  }>;
  pageNumber: number;
  totalCount: number;
  totalPages: number;
};

export function useCommentList(slug: string) {
  const { data, hasNextPage, fetchNextPage, isFetchingNextPage } = useInfiniteQuery(
    ['comments', slug],
    async function fetchCommentListRequest({ pageParam = 1 }) {
      const { data } = await axios.get<CommentList>(
        `/api/v1/posts/${slug}/comments?pageNumber=${pageParam}&pageSize=10`,
      );

      return data;
    },
    {
      getNextPageParam: (lastPage, pages) => {
        return lastPage.hasNextPage ? pages.length + 1 : false;
      },
      suspense: false,
    },
  );

  return {
    pages: data?.pages ?? [],
    hasNextPage: hasNextPage,
    fetchNextPage,
    isFetchingNextPage,
  };
}
