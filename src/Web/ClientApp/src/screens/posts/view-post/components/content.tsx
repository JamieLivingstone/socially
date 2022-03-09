import { Box, Flex, HStack, Heading, Tag, TagLabel, Text, VStack } from '@chakra-ui/react';
import React from 'react';
import ReactTimeago from 'react-timeago';

import { Avatar, Markdown, TagList } from '../../../../components';
import { Post } from '../types';

type ContentProps = {
  post: Post;
};

export function Content({ post }: ContentProps) {
  return (
    <VStack alignItems="left" gap={2}>
      <Avatar name={post.author.name} username={post.author.username}>
        <Flex gap={1}>
          <Text fontSize="xs">Posted</Text>
          <ReactTimeago component={Text} fontSize="xs" date={post.createdAt} />
        </Flex>
      </Avatar>

      <Heading fontSize="5xl" as="h1">
        {post.title}
      </Heading>

      <TagList tags={post.tags} />

      <Markdown source={post.body} />
    </VStack>
  );
}
