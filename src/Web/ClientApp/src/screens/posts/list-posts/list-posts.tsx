import { Grid, GridItem } from '@chakra-ui/react';
import React from 'react';
import { useParams } from 'react-router-dom';

import PostList from '@components/post-list';
import TrendingPosts from '@components/trending-posts';

function ListPosts() {
  const { tag } = useParams();

  return (
    <Grid gridTemplateColumns={{ md: '3fr 1fr' }} gridGap={2}>
      <GridItem borderRadius="lg">
        <PostList options={{ tag }} />
      </GridItem>

      <GridItem display={{ sm: 'none', md: 'block' }}>
        <TrendingPosts />
      </GridItem>
    </Grid>
  );
}

export default ListPosts;
