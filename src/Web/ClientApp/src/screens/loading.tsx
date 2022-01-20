import { Center, Spinner } from '@chakra-ui/react';
import React from 'react';

export function Loading() {
  return (
    <Center height="calc(100vh)">
      <Spinner size="xl" speed="0.5s" />
    </Center>
  );
}

export default Loading;
