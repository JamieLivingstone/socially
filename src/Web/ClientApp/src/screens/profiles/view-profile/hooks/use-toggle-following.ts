import { useToast } from '@chakra-ui/react';
import axios from 'axios';
import { useState } from 'react';
import { useMutation, useQueryClient } from 'react-query';

export function useToggleFollowing({ username, isFollowing }: { username: string; isFollowing: boolean }) {
  const queryClient = useQueryClient();
  const toast = useToast();
  const [following, setFollowing] = useState(isFollowing);

  const { mutateAsync, isLoading } = useMutation(
    async function toggleFollowingRequest() {
      try {
        await axios({
          url: `/api/v1/profiles/${username}/followers`,
          method: following ? 'delete' : 'post',
        });

        await queryClient.invalidateQueries(['profile', username]);

        setFollowing(!following);
      } catch (error) {
        toast({
          title: `Failed to ${following ? 'unfollow' : 'follow'} profile!`,
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
    following,
  };
}
