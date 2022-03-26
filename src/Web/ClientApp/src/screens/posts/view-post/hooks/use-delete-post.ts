import { useToast } from '@chakra-ui/react';
import axios from 'axios';
import { useMutation, useQueryClient } from 'react-query';
import { useNavigate } from 'react-router-dom';

import { routes } from '../../../../constants';

export function useDeletePost() {
  const toast = useToast();
  const queryClient = useQueryClient();
  const navigate = useNavigate();

  const { mutateAsync } = useMutation(
    async function deletePostRequest(payload: { slug: string }) {
      try {
        await axios.delete(`/api/v1/posts/${payload.slug}`);
        await queryClient.invalidateQueries(['posts']);
        navigate(routes.HOME);
      } catch {
        toast({
          title: 'Failed to delete post!',
          status: 'error',
          duration: 3000,
          isClosable: false,
        });
      }
    },
    { retry: false },
  );

  return {
    deletePost: mutateAsync,
  };
}
