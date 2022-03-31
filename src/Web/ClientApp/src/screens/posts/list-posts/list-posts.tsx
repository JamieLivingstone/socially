import { Grid, GridItem } from '@chakra-ui/react';
import React from 'react';
import { useParams } from 'react-router-dom';

import PostList from '../common/components/post-list';
import TrendingPosts from '../common/components/trending-posts';

function ListPosts() {
  const { tag } = useParams();

  return (
    <Grid gridTemplateColumns={{ md: '3fr 1fr' }} gridGap={4}>
      <GridItem borderRadius="lg">
        <PostList title={tag ? `#${tag}` : 'Global feed'} options={{ tag }} />
      </GridItem>

      <GridItem display={{ sm: 'none', md: 'block' }}>
        <TrendingPosts />
      </GridItem>
    </Grid>
  );
}

export default ListPosts;
