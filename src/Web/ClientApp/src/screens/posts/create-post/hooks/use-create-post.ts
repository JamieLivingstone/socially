import { useToast } from '@chakra-ui/react';
import axios from 'axios';
import { useMutation } from 'react-query';
import { useNavigate } from 'react-router-dom';

import { useAuth } from '../../../../hooks';

type CreatePostResponse = {
  slug: string;
};

export function useCreatePost() {
  const toast = useToast();
  const navigate = useNavigate();
  const { account } = useAuth();

  const { data, isLoading, isError, error, mutateAsync } = useMutation(
    async function createPostRequest(payload: { title: string; body: string; tags: Array<string> }) {
      try {
        const { data } = await axios.post<CreatePostResponse>('/api/v1/posts', payload);

        navigate(`/${account?.username}/${data.slug}`);
      } catch {
        toast({
          title: 'Failed to create post!',
          status: 'error',
          duration: 3000,
          isClosable: false,
        });
      }
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
