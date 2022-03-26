import { Icon, IconButton, Menu, MenuButton, MenuItem, MenuList } from '@chakra-ui/react';
import React from 'react';
import { MdMoreVert } from 'react-icons/all';
import { Link } from 'react-router-dom';

import { useDeletePost } from '../hooks/use-delete-post';
import { Post } from '../types';

type ActionsProps = {
  post: Post;
};

export function Actions({ post }: ActionsProps) {
  const { deletePost } = useDeletePost();

  return (
    <Menu>
      <MenuButton
        as={IconButton}
        aria-label="Actions"
        icon={<Icon as={MdMoreVert} fontSize="1.5rem" />}
        variant="ghost"
        colorScheme="gray"
      />

      <MenuList>
        <MenuItem>
          <Link to={`/${post.author.username}/${post.slug}/edit`}>Edit</Link>
        </MenuItem>

        <MenuItem
          onClick={async () => {
            await deletePost({ slug: post.slug });
          }}
        >
          Delete
        </MenuItem>
      </MenuList>
    </Menu>
  );
}
