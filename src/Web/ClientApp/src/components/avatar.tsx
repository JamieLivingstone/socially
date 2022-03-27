import { Box, Avatar as ChakraAvatar, Flex, Text } from '@chakra-ui/react';
import React from 'react';
import { Link } from 'react-router-dom';

type AvatarProps = {
  name: string;
  username: string;
  children?: React.ReactNode;
  iconOnly?: boolean;
};

function Avatar({ name, username, children, iconOnly = false }: AvatarProps) {
  return (
    <Flex alignItems="center">
      <Link to={`/${username}`}>
        <ChakraAvatar mr={2} size="sm" name={name} />
      </Link>

      {!iconOnly && (
        <Box>
          <Link to={`/${username}`}>
            <Text fontWeight="600">{name}</Text>
          </Link>

          {children}
        </Box>
      )}
    </Flex>
  );
}

export default Avatar;
