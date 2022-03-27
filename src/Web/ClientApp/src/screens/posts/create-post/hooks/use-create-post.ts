import { useToast } from '@chakra-ui/react';
import axios from 'axios';
import { useMutation } from 'react-query';
import { useNavigate } from 'react-router-dom';

import { useAuth } from '@hooks/use-auth';

export type CreatePostResponse = {
  slug: string;
};

export function useCreatePost() {
  const { account } = useAuth();
  const navigate = useNavigate();
  const toast = useToast();

  const { mutateAsync } = useMutation(
    async function createPostRequest(payload: { title: string; body: string; tags: Array<string> }) {
      try {
        const { data } = await axios.post<CreatePostResponse>('/api/v1/posts', payload);

        navigate(`/${account?.username}/${data.slug}`);
      } catch (error) {
        toast({
          title: 'Failed to create post!',
          status: 'error',
          duration: 3000,
          isClosable: false,
        });
      }
    },
    { useErrorBoundary: false },
  );

  return {
    createPost: mutateAsync,
  };
}
