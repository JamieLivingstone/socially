import { Box, Grid, GridItem, Heading } from '@chakra-ui/react';
import React from 'react';
import { Link } from 'react-router-dom';

import Nav from '@components/nav';

function Header() {
  return (
    <Box as="header" bg="white">
      <Grid gridTemplateColumns="1fr 3fr" p={2} maxW="1400px" m="0 auto">
        <GridItem display="flex" alignItems="center" gap={2}>
          <Heading color="green" fontSize="xl" as={Link} to="/">
            Socially
          </Heading>
        </GridItem>

        <GridItem display="flex" alignItems="center" justifySelf="flex-end" gap={2}>
          <Nav />
        </GridItem>
      </Grid>
    </Box>
  );
}

export default Header;
