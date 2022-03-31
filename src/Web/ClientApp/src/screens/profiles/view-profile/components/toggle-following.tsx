import { Button } from '@chakra-ui/react';
import React from 'react';

import { useAuth } from '@hooks/use-auth';

import { Profile } from '../hooks/use-profile';
import { useToggleFollowing } from '../hooks/use-toggle-following';

type ToggleFollowingProps = {
  profile: Profile;
};
function ToggleFollowing({ profile }: ToggleFollowingProps) {
  const { account } = useAuth();

  const { following, isLoading, toggle } = useToggleFollowing({
    username: profile.username,
    isFollowing: profile.following,
  });

  if (!account || account?.username === profile.username) {
    return <></>;
  }

  return (
    <Button
      disabled={isLoading}
      colorScheme={following ? 'red' : 'green'}
      onClick={() => {
        toggle();
      }}
    >
      {following ? 'Unfollow' : 'Follow'}
    </Button>
  );
}

export default ToggleFollowing;
