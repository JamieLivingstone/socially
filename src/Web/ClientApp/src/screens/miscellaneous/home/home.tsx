import { Box, Flex, Icon, Stack, Text } from '@chakra-ui/react';
import React from 'react';
import { BiLike, MdOutlineInsertComment } from 'react-icons/all';
import { Link } from 'react-router-dom';
import ReactTimeago from 'react-timeago';

import { Avatar, TagList } from '../../../components';
import { usePostList } from '../../../hooks';

function Home() {
  const { pages } = usePostList({
    orderBy: 'created',
  });

  return (
    <Box>
      {pages.map((page) =>
        page.items.map((post) => (
          <Link to={`/${post.author.username}/${post.slug}`} key={post.slug}>
            <Stack bg="white" p={4} mb={2} spacing={2} borderRadius="lg" borderWidth="1px">
              <Avatar username={post.author.username} name={post.author.name}>
                <Flex gap={1}>
                  <Text fontSize="xs">Posted</Text>
                  <ReactTimeago component={Text} fontSize="xs" date={post.createdAt} />
                </Flex>
              </Avatar>

              <Text fontSize="2xl" fontWeight={500}>
                {post.title}
              </Text>

              <TagList tags={post.tags} />

              <Flex alignItems="center" gap={4}>
                <Flex alignItems="center" gap={1}>
                  <Icon as={BiLike} w={5} h={5} />
                  <Text>{post.likesCount} likes</Text>
                </Flex>

                <Flex alignItems="center" gap={1}>
                  <Icon as={MdOutlineInsertComment} w={5} h={5} />
                  <Text>{post.commentsCount} comments</Text>
                </Flex>
              </Flex>
            </Stack>
          </Link>
        )),
      )}
    </Box>
  );
}

export default Home;