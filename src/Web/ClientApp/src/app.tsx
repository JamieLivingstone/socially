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

        <Box bg="gray.100" p={2} flex={1}>
          <Box maxW="1400px" m="0 auto">
            <Suspense fallback={<Loading />}>
              <Router />
            </Suspense>
          </Box>
        </Box>

        <Footer />
      </Flex>
    </Providers>
  );
}
