import { Box, Button, Center, Heading } from '@chakra-ui/react';
import React from 'react';

function Error() {
  return (
    <Center height="100vh">
      <Box textAlign="center">
        <Heading as="h1" size="lg" mb={6}>
          Something went wrong!
        </Heading>

        <Button onClick={() => location.reload()}>Try again</Button>
      </Box>
    </Center>
  );
}

export default Error;
