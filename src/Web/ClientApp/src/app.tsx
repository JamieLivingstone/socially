import { Box, Flex } from '@chakra-ui/react';
import React, { Suspense } from 'react';

import Footer from '@components/footer';
import Header from '@components/header';
import Loading from '@components/loading';
import Providers from '@providers';

import Router from './router';

export default function App() {
  return (
    <Providers>
      <Flex direction="column" minH="100vh">
        <Header />

        <Box maxW="1400px" w="100%" p={4} m="0 auto" flex={1}>
          <Suspense fallback={<Loading />}>
            <Router />
          </Suspense>
        </Box>

        <Footer />
      </Flex>
    </Providers>
  );
}
