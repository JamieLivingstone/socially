import { Avatar, Button, Menu, MenuButton, MenuDivider, MenuItem, MenuList, Text } from '@chakra-ui/react';
import React from 'react';
import { Link } from 'react-router-dom';

import { routes } from '../constants';
import { useAuth } from '../hooks';

export function Nav() {
  const { account, logout } = useAuth();

  if (!account) {
    return (
      <>
        <Button variant="ghost" color="grey" as={Link} to={routes.LOGIN}>
          Login
        </Button>

        <Button variant="outline" as={Link} to={routes.REGISTER}>
          Register
        </Button>
      </>
    );
  }

  return (
    <>
      <Button variant="outline" mr={4} as={Link} to={routes.CREATE_POST}>
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
          <MenuItem>
            <Link to={`${routes.PROFILE}/${account.username}`}>
              <Text fontWeight="600">{account.name}</Text>
              <Text as="small">@{account.username}</Text>
            </Link>
          </MenuItem>

          <MenuDivider />

          <MenuItem as={Link} to={routes.CREATE_POST}>
            Create Post
          </MenuItem>

          <MenuDivider />

          <MenuItem onClick={logout}>Logout</MenuItem>
        </MenuList>
      </Menu>
    </>
  );
}
