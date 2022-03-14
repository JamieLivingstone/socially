import React, { ReactNode } from 'react';
import { BrowserRouter } from 'react-router-dom';

import { AuthProvider } from './auth-provider';
import { ChakraProvider } from './chakra-provider';
import { ErrorBoundary } from './error-boundary';
import { ReactQueryProvider } from './react-query-provider';

export function Providers({ children }: { children: ReactNode }) {
  return (
    <ReactQueryProvider>
      <ChakraProvider>
        <ErrorBoundary>
          <AuthProvider>
            <BrowserRouter>{children}</BrowserRouter>
          </AuthProvider>
        </ErrorBoundary>
      </ChakraProvider>
    </ReactQueryProvider>
  );
}
