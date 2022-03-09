import { Box, ChakraProvider, Flex } from '@chakra-ui/react';
import React, { Suspense } from 'react';
import { QueryClient, QueryClientProvider } from 'react-query';
import { BrowserRouter } from 'react-router-dom';

import { ErrorBoundary, Footer, Header, Loading, PrivacyBanner } from './components';
import { theme } from './constants';
import { AuthProvider } from './hooks';
import { Router } from './router';

const client = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
      retry: false,
    },
  },
});

export default function App() {
  return (
    <QueryClientProvider client={client}>
      <ChakraProvider theme={theme}>
        <ErrorBoundary>
          <BrowserRouter>
            <AuthProvider>
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
            </AuthProvider>
          </BrowserRouter>
        </ErrorBoundary>
      </ChakraProvider>
    </QueryClientProvider>
  );
}
