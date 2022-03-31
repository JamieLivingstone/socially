import axios from 'axios';
import { useQuery } from 'react-query';

export type Post = {
  author: {
    name: string;
    username: string;
  };
  body: string;
  createdAt: string;
  liked: boolean;
  likesCount: number;
  slug: string;
  tags: Array<{ name: string }>;
  title: string;
  updatedAt: string;
};

export function usePost(slug: string) {
  const { data } = useQuery(
    ['post', slug],
    async function fetchPostRequest() {
      const { data } = await axios.get<Post>(`/api/v1/posts/${slug}`);

      return data;
    },
    {
      suspense: true,
      useErrorBoundary: true,
    },
  );

  return {
    post: data as Post,
  };
}
