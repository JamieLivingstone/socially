import { Box, Flex, Heading, Text, VStack } from '@chakra-ui/react';
import React from 'react';
import ReactTimeago from 'react-timeago';

import Avatar from '@components/avatar';
import Markdown from '@components/markdown';
import TagList from '@components/tag-list';
import { useAuth } from '@hooks/use-auth';
import { Post } from '@hooks/use-post';

import { Actions } from './actions';

type ContentProps = {
  post: Post;
};

export function Content({ post }: ContentProps) {
  const { account } = useAuth();

  return (
    <VStack alignItems="left" gap={2}>
      <Flex>
        <Avatar name={post.author.name} username={post.author.username}>
          <Flex gap={1}>
            <Text fontSize="xs">Posted</Text>
            <ReactTimeago component={Text} fontSize="xs" date={post.createdAt} />
          </Flex>
        </Avatar>

        <Box marginLeft="auto">{post.author.username === account?.username && <Actions post={post} />}</Box>
      </Flex>

      <Heading fontSize="5xl" as="h1">
        {post.title}
      </Heading>

      <TagList tags={post.tags} />

      <Markdown source={post.body} />
    </VStack>
  );
}
