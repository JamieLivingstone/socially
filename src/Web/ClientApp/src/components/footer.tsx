import { Box, ButtonGroup, Flex, IconButton, Text } from '@chakra-ui/react';
import React from 'react';
import { FaGithub } from 'react-icons/fa';

function Footer() {
  return (
    <Box as="footer" bg="white" w="100%">
      <Flex maxW="1400px" m="0 auto" p={2} alignItems="center">
        <Text fontSize="sm" color="subtle">
          &copy; {new Date().getFullYear()} Socially. All rights reserved.
        </Text>

        <ButtonGroup variant="ghost" ml="auto">
          <IconButton
            as="a"
            href="https://github.com/JamieLivingstone/socially"
            aria-label="GitHub"
            icon={<FaGithub fontSize="1.25rem" />}
            color="subtle"
          />
        </ButtonGroup>
      </Flex>
    </Box>
  );
}

export default Footer;
