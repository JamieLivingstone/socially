import { Icon, IconButton, Menu, MenuButton, MenuItem, MenuList } from '@chakra-ui/react';
import React from 'react';
import { MdMoreVert } from 'react-icons/md';
import { Link } from 'react-router-dom';

import { Post } from '../../common/hooks/use-post';
import { useDeletePost } from '../hooks/use-delete-post';

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
        <MenuItem to={`/${post.author.username}/${post.slug}/edit`} as={Link}>
          Edit
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
