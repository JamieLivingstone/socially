import { Avatar, Box, Button, Menu, MenuButton, MenuDivider, MenuItem, MenuList, Text } from '@chakra-ui/react';
import React from 'react';
import { Link } from 'react-router-dom';

import { useAuth } from '@hooks/use-auth';

function Nav() {
  const { account, logout } = useAuth();

  if (!account) {
    return (
      <>
        <Button variant="ghost" color="grey" as={Link} to="/login">
          Login
        </Button>

        <Button variant="outline" as={Link} to="/register">
          Register
        </Button>
      </>
    );
  }

  return (
    <>
      <Button variant="outline" mr={4} as={Link} to="/create-post">
        Create Post
      </Button>

      <Menu closeOnBlur>
        <MenuButton
          as={Button}
          rounded="full"
          variant="link"
          cursor="pointer"
          minW={0}
          _hover={{ textDecoration: 'none' }}
        >
          <Avatar size="sm" name={account.name} />
        </MenuButton>

        <MenuList>
          <MenuItem as={Link} to={`/${account.username}`}>
            <Box>
              <Text fontWeight="600">{account.name}</Text>
              <Text as="small">@{account.username}</Text>
            </Box>
          </MenuItem>

          <MenuDivider />

          <MenuItem as={Link} to="/create-post">
            Create Post
          </MenuItem>

          <MenuDivider />

          <MenuItem onClick={logout}>Logout</MenuItem>
        </MenuList>
      </Menu>
    </>
  );
}

export default Nav;
