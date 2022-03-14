import { ChakraProvider as ChakraProviderBase, extendTheme, withDefaultColorScheme } from '@chakra-ui/react';
import React, { ReactNode } from 'react';

const theme = extendTheme(withDefaultColorScheme({ colorScheme: 'green' }), {
  initialColorMode: 'light',
  useSystemColorMode: false,
});

export function ChakraProvider({ children }: { children: ReactNode }) {
  return <ChakraProviderBase theme={theme}>{children}</ChakraProviderBase>;
}
