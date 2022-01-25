import axios from 'axios';
import { useMutation } from 'react-query';
import { useNavigate } from 'react-router-dom';

import { useAuth } from './use-auth';

export type CreatePostResponse = {
  slug: string;
};

export function useCreatePost() {
  const navigate = useNavigate();
  const { account } = useAuth();

  const { data, isLoading, isError, error, mutateAsync } = useMutation(
    async function createPostRequest(payload: { title: string; body: string; tags: Array<string> }) {
      const { data } = await axios.post<CreatePostResponse>('/api/v1/posts', payload);

      navigate(`/${account?.username}/${data.slug}`);
    },
    { retry: false },
  );

  return {
    data,
    isLoading,
    isError,
    error,
    mutateAsync,
  };
}
