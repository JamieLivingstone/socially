import { Divider, Grid, GridItem } from '@chakra-ui/react';
import React from 'react';
import { useParams } from 'react-router-dom';

import TrendingPosts from '../common/components/trending-posts';
import { usePost } from '../common/hooks/use-post';
import Comments from './components/comments';
import Content from './components/content';

function ViewPost() {
  const { slug } = useParams();
  const { post } = usePost(slug ?? '');

  return (
    <Grid gridTemplateColumns={{ md: '3fr 1fr' }} gridGap={4}>
      <GridItem bg="white" borderWidth="1px" p={4} borderRadius="lg">
        <Content post={post} />
        <Divider my={6} />
        <Comments slug={post.slug} />
      </GridItem>

      <GridItem display={{ sm: 'none', md: 'block' }}>
        <TrendingPosts />
      </GridItem>
    </Grid>
  );
}

export default ViewPost;
