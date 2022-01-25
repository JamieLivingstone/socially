import { Box, ChakraProvider } from '@chakra-ui/react';
import React, { Suspense } from 'react';
import { QueryClient, QueryClientProvider } from 'react-query';
import { BrowserRouter } from 'react-router-dom';

import { Header, PrivacyBanner } from './components';
import { theme } from './constants';
import { AuthProvider } from './hooks';
import { Router } from './router';
import { Loading } from './screens/loading';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 2,
      refetchOnWindowFocus: false,
      suspense: true,
      useErrorBoundary: true,
    },
  },
});

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <ChakraProvider theme={theme}>
        <Box bg="gray.100" minH="100vh" display="flex" flexDirection="column">
          <BrowserRouter>
            <AuthProvider>
              <Header />

              <Suspense fallback={<Loading />}>
                <Router />
              </Suspense>

              <PrivacyBanner />
            </AuthProvider>
          </BrowserRouter>
        </Box>
      </ChakraProvider>
    </QueryClientProvider>
  );
}
