import { ChakraProvider as Provider, Theme, extendTheme, withDefaultColorScheme } from '@chakra-ui/react';
import React, { ReactNode } from 'react';

const theme = extendTheme(withDefaultColorScheme({ colorScheme: 'green' }), {
  initialColorMode: 'light',
  useSystemColorMode: false,
  styles: {
    global: ({ theme }: { theme: Theme }) => ({
      body: {
        bg: theme.colors.gray['100'],
      },
    }),
  },
});

export function ChakraProvider({ children }: { children: ReactNode }) {
  return <Provider theme={theme}>{children}</Provider>;
}
