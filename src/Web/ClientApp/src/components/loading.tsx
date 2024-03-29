import { Center, Spinner } from '@chakra-ui/react';
import React from 'react';

function Loading() {
  return (
    <Center height="100vh">
      <Spinner size="xl" color="green" speed="0.5s" />
    </Center>
  );
}

export default Loading;
