import { Box, ButtonGroup, IconButton, Stack, Text } from '@chakra-ui/react';
import React from 'react';
import { FaGithub } from 'react-icons/all';

export function Footer() {
  return (
    <Box as="footer">
      <Stack p={4} justify="space-between" direction={{ base: 'column-reverse', md: 'row' }} align="center">
        <Text fontSize="sm" color="subtle">
          &copy; {new Date().getFullYear()} Socially. All rights reserved.
        </Text>

        <ButtonGroup variant="ghost">
          <IconButton
            as="a"
            href="https://github.com/JamieLivingstone/socially"
            aria-label="GitHub"
            icon={<FaGithub fontSize="1.25rem" />}
            color="subtle"
          />
        </ButtonGroup>
      </Stack>
    </Box>
  );
}
