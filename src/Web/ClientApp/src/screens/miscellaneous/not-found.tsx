import { Box, Button, Center, Heading, Text } from '@chakra-ui/react';
import React from 'react';

function NotFound() {
  return (
    <Center textAlign="center" height="100vh">
      <Box>
        <Heading display="inline-block" as="h1" size="2xl">
          404
        </Heading>

        <Text fontWeight="bold" mt={3} mb={2}>
          Page Not Found
        </Text>

        <Text mb={6}>The resource you're looking for does not exist</Text>

        <Button
          color="white"
          variant="solid"
          onClick={() => {
            window.location.href = '/';
          }}
        >
          Go to Home
        </Button>
      </Box>
    </Center>
  );
}

export default NotFound;
