import { useToast } from '@chakra-ui/react';
import axios from 'axios';
import { useMutation, useQueryClient } from 'react-query';

export function useDeleteComment() {
  const toast = useToast();
  const queryClient = useQueryClient();

  const { mutateAsync } = useMutation(
    async function deleteCommentRequest(payload: { commentId: number; slug: string }) {
      try {
        await axios.delete(`/api/v1/posts/${payload.slug}/comments/${payload.commentId}`);
        await queryClient.invalidateQueries(['comments', payload.slug]);
      } catch {
        toast({
          title: 'Failed to delete comment!',
          status: 'error',
          duration: 3000,
          isClosable: false,
        });
      }
    },
    { retry: false },
  );

  return {
    deleteCommentAsync: mutateAsync,
  };
}
