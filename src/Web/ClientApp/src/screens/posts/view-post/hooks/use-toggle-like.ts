import { useToast } from '@chakra-ui/react';
import axios from 'axios';
import { useState } from 'react';
import { useMutation, useQueryClient } from 'react-query';

export function useToggleLike({ slug, isLiked }: { slug: string; isLiked: boolean }) {
  const queryClient = useQueryClient();
  const toast = useToast();
  const [liked, setLiked] = useState(isLiked);

  const { mutateAsync, isLoading } = useMutation(
    async function toggleLikeRequest() {
      try {
        await axios({
          url: `/api/v1/posts/${slug}/likes`,
          method: liked ? 'delete' : 'post',
        });

        await queryClient.invalidateQueries(['post', slug]);

        setLiked(!liked);
      } catch (error) {
        toast({
          title: `Failed to ${liked ? 'unlike' : 'like'} post!`,
          status: 'error',
          duration: 3000,
          isClosable: false,
        });
      }
    },
    { useErrorBoundary: false },
  );

  return {
    toggle: mutateAsync,
    isLoading,
    liked,
  };
}
