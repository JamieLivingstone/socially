import { Button, Flex, Heading, Icon, Stack, Text } from '@chakra-ui/react';
import React from 'react';
import { AiOutlineHeart } from 'react-icons/ai';
import { MdOutlineInsertComment } from 'react-icons/md';
import { Link } from 'react-router-dom';
import ReactTimeago from 'react-timeago';

import Avatar from '@components/avatar';
import TagList from '@components/tag-list';

import { UsePostListOptions, usePostList } from '../hooks/use-post-list';

type PostListProps = {
  title?: string;
  options?: UsePostListOptions;
};

function PostList({ title, options }: PostListProps) {
  const { pages, setOrderBy, orderBy } = usePostList(options ?? {});

  return (
    <Stack spacing={2} p={4} bg="white" borderRadius="lg" borderWidth="1px">
      <Flex>
        {title && (
          <Heading fontSize="2xl" as="h1">
            {title}
          </Heading>
        )}

        <Flex ml="auto" gap={2} alignItems="center">
          <Button size="sm" variant={orderBy === 'created' ? 'solid' : 'ghost'} onClick={() => setOrderBy('created')}>
            Latest
          </Button>

          <Button
            size="sm"
            variant={orderBy === 'likesCount' ? 'solid' : 'ghost'}
            onClick={() => setOrderBy('likesCount')}
          >
            Top
          </Button>
        </Flex>
      </Flex>

      {!pages.length || (pages[0].totalCount === 0 && <Text>No posts!</Text>)}

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
                  <Icon as={AiOutlineHeart} w={5} h={5} />
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
    </Stack>
  );
}

export default PostList;
