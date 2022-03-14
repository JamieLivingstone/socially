import { Box, Flex } from '@chakra-ui/react';
import React, { Suspense } from 'react';

import { Footer, Header, Loading, PrivacyBanner } from './components';
import { Providers } from './providers';
import { Router } from './router';

export default function App() {
  return (
    <Providers>
      <Flex direction="column" minH="100vh">
        <Header />

        <Box bg="gray.100" p={2} flex={1}>
          <Suspense fallback={<Loading />}>
            <Router />
          </Suspense>
        </Box>

        <PrivacyBanner />

        <Footer />
      </Flex>
    </Providers>
  );
}
