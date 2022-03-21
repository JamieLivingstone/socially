import { Avatar, Box, Button, Divider, Flex, Grid, GridItem, Heading, Icon, Stack, Text } from '@chakra-ui/react';
import React from 'react';
import { IoPersonAddOutline, MdList, MdOutlineInsertComment } from 'react-icons/all';
import { useParams } from 'react-router-dom';

import { PostList } from '../../../components';
import { useAuth } from '../../../hooks';
import { useProfile, useToggleFollowing } from './hooks';

function Profile() {
  const { account } = useAuth();
  const { username } = useParams();
  const { profile } = useProfile(username ?? '');

  const toggleFollowing = useToggleFollowing({
    username: username ?? '',
    isFollowing: profile.following,
  });

  return (
    <>
      <Stack
        bg="white"
        mb={4}
        p={4}
        spacing={4}
        alignItems="center"
        borderWidth="1px"
        borderRadius="lg"
        position="relative"
        mt="65px"
      >
        <Avatar name={profile.name} username={profile.username} size="xl" mt="-65px" />

        <Heading as="h1" size="lg">
          {profile.name}
        </Heading>

        <Flex gap={4} flexDirection={{ sm: 'column', md: 'row' }}>
          <Flex alignItems="center" gap={2}>
            <Icon as={MdList} w={5} h={5} />
            <Text>{profile.postsCount} posts published</Text>
          </Flex>

          <Flex alignItems="center" gap={2}>
            <Icon as={MdOutlineInsertComment} w={5} h={5} />
            <Text>{profile.commentsCount} comments written</Text>
          </Flex>

          <Flex alignItems="center" gap={2}>
            <Icon as={IoPersonAddOutline} w={5} h={5} />
            <Text>{profile.followersCount} followers</Text>
          </Flex>
        </Flex>

        {account && username !== account.username && (
          <Button
            disabled={toggleFollowing.isLoading}
            colorScheme={toggleFollowing.following ? 'red' : 'green'}
            onClick={async () => {
              await toggleFollowing.toggle();
            }}
          >
            {toggleFollowing.following ? 'Unfollow' : 'Follow'}
          </Button>
        )}
      </Stack>

      <PostList options={{ author: username }} />
    </>
  );
}

export default Profile;
