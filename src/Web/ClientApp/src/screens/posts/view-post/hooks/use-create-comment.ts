import { useToast } from '@chakra-ui/react';
import axios from 'axios';
import { useMutation, useQueryClient } from 'react-query';

export function useCreateComment() {
  const toast = useToast();
  const queryClient = useQueryClient();

  const { mutateAsync } = useMutation(
    async function createPostRequest(payload: { message: string; slug: string }) {
      try {
        await axios.post(`/api/v1/posts/${payload.slug}/comments`, payload);
        await queryClient.invalidateQueries(['comments', payload.slug]);
      } catch {
        toast({
          title: 'Failed to create comment!',
          status: 'error',
          duration: 3000,
          isClosable: false,
        });
      }
    },
    { retry: false },
  );

  return {
    createCommentAsync: mutateAsync,
  };
}
