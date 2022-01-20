import { ThemeConfig, extendTheme, withDefaultColorScheme } from '@chakra-ui/react';

const config: ThemeConfig = {
  initialColorMode: 'light',
  useSystemColorMode: false,
};

export const theme = extendTheme(withDefaultColorScheme({ colorScheme: 'green' }), { config });
