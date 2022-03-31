import { Avatar, Flex, Heading, Icon, Stack, Text } from '@chakra-ui/react';
import React from 'react';
import { IoPersonAddOutline } from 'react-icons/io5';
import { MdList, MdOutlineInsertComment } from 'react-icons/md';
import { useParams } from 'react-router-dom';

import PostList from '@screens/posts/common/components/post-list';

import ToggleFollowing from './components/toggle-following';
import { useProfile } from './hooks/use-profile';

function Profile() {
  const { username } = useParams();
  const { profile } = useProfile(username ?? '');

  return (
    <>
      <Stack
        bg="white"
        mb={2}
        p={4}
        spacing={4}
        alignItems="center"
        borderWidth="1px"
        borderRadius="lg"
        position="relative"
        mt="48px"
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

        <ToggleFollowing profile={profile} />
      </Stack>

      <PostList title="My posts" options={{ author: username }} />
    </>
  );
}

export default Profile;
