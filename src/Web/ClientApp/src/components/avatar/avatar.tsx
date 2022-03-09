import { Box, Avatar as ChakraAvatar, Flex, Text } from '@chakra-ui/react';
import React from 'react';
import { Link } from 'react-router-dom';

import { routes } from '../../constants';

type AvatarProps = {
  name: string;
  username: string;
  children?: React.ReactNode;
  iconOnly?: boolean;
};

export function Avatar({ name, username, children, iconOnly = false }: AvatarProps) {
  return (
    <Flex alignItems="center">
      <Link to={routes.getProfileRoute(username)}>
        <ChakraAvatar mr={2} size="sm" name={name} />
      </Link>

      {!iconOnly && (
        <Box>
          <Link to={routes.getProfileRoute(username)}>
            <Text fontWeight="600">{name}</Text>
          </Link>

          {children}
        </Box>
      )}
    </Flex>
  );
}
