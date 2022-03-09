import axios from 'axios';
import { useQuery } from 'react-query';

import { Post } from '../types';

export function usePost(slug: string) {
  const { data } = useQuery(
    ['post', slug],
    async function fetchPostRequest() {
      const { data } = await axios.get<Post>(`/api/v1/posts/${slug}`);

      return data;
    },
    { suspense: true },
  );

  return {
    post: data as Post,
  };
}
