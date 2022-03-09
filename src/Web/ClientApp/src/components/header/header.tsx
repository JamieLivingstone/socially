import { Grid, GridItem, Heading } from '@chakra-ui/react';
import React from 'react';
import { Link } from 'react-router-dom';

import { routes } from '../../constants';
import { Nav } from './nav';

export function Header() {
  return (
    <Grid gridTemplateColumns="1fr 3fr" as="header" bg="white" p={2}>
      <GridItem display="flex" alignItems="center" gap={2}>
        <Heading color="green" fontSize="xl" as={Link} to={routes.HOME}>
          Socially
        </Heading>
      </GridItem>

      <GridItem display="flex" alignItems="center" justifySelf="flex-end" gap={2}>
        <Nav />
      </GridItem>
    </Grid>
  );
}
