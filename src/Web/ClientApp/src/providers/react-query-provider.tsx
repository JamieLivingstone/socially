import axios from 'axios';
import React, { ReactNode } from 'react';
import { QueryClient, QueryClientProvider } from 'react-query';

const RETRY_STATUS_CODES = [408, 413, 429, 500, 502, 503, 504];

const client = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
      retry: (failureCount, error) => {
        if (
          axios.isAxiosError(error) &&
          error.response &&
          failureCount < 3 &&
          RETRY_STATUS_CODES.includes(error.response.status)
        ) {
          return true;
        }

        return false;
      },
    },
  },
});

export function ReactQueryProvider({ children }: { children: ReactNode }) {
  return <QueryClientProvider client={client}>{children}</QueryClientProvider>;
}
