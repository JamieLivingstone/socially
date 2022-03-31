import { IconButton } from '@chakra-ui/react';
import React from 'react';
import { AiFillHeart, AiOutlineHeart } from 'react-icons/ai';

import { Post } from '../../common/hooks/use-post';
import { useToggleLike } from '../hooks/use-toggle-like';

type ToggleLikeProps = {
  post: Post;
};

function ToggleLike({ post }: ToggleLikeProps) {
  const { liked, isLoading, toggle } = useToggleLike({ slug: post.slug, isLiked: post.liked });

  return (
    <IconButton
      aria-label={liked ? 'Unlike post' : 'Like post'}
      title={liked ? 'Unlike post' : 'Like post'}
      variant="ghost"
      icon={liked ? <AiFillHeart fontSize="1.5rem" /> : <AiOutlineHeart fontSize="1.5rem" />}
      disabled={isLoading}
      onClick={() => toggle()}
    />
  );
}

export default ToggleLike;
