import { useToast } from '@chakra-ui/react';
import axios from 'axios';
import { useMutation, useQueryClient } from 'react-query';
import { useNavigate } from 'react-router-dom';

import { useAuth } from '@hooks/use-auth';

export function useEditPost() {
  const { account } = useAuth();
  const queryClient = useQueryClient();
  const navigate = useNavigate();
  const toast = useToast();

  const { mutateAsync } = useMutation(
    async function createPostRequest(payload: { title: string; body: string; slug: string; tags: Array<string> }) {
      try {
        await axios.patch(`/api/v1/posts/${payload.slug}`, payload);
        await queryClient.invalidateQueries(['post', payload.slug]);
        navigate(`/${account?.username}/${payload.slug}`);
      } catch (error) {
        toast({
          title: 'Failed to edit post!',
          status: 'error',
          duration: 3000,
          isClosable: false,
        });
      }
    },
    { useErrorBoundary: false },
  );

  return {
    editPost: mutateAsync,
  };
}
