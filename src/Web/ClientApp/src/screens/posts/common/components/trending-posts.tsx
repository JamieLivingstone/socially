import { Box, Heading, Text } from '@chakra-ui/react';
import React from 'react';
import { Link } from 'react-router-dom';

import TagList from '@components/tag-list';

import { usePostList } from '../../common/hooks/use-post-list';

function TrendingPosts() {
  const { pages } = usePostList({
    pageSize: 5,
    orderBy: 'likesCount',
  });

  return (
    <Box bg="white" borderWidth="1px" borderRadius="lg" position="sticky" top={2}>
      <Heading fontSize="xl" as="h2" p={4}>
        Trending posts
      </Heading>

      {pages.map((page) =>
        page.items.map((post) => (
          <Link to={`/${post.author.username}/${post.slug}`} key={post.slug}>
            <Box borderTopWidth={1} p={4}>
              <Text mb={1}>{post.title}</Text>
              <TagList tags={post.tags} />
            </Box>
          </Link>
        )),
      )}
    </Box>
  );
}

export default TrendingPosts;
