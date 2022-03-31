import { Box, Flex, Heading, IconButton, Text } from '@chakra-ui/react';
import React from 'react';
import { AiOutlineDelete } from 'react-icons/ai';
import InfiniteScroll from 'react-infinite-scroll-component';
import { Link } from 'react-router-dom';
import ReactTimeago from 'react-timeago';

import { useAuth } from '@hooks/use-auth';

import { useCommentList } from '../hooks/use-comment-list';
import { useDeleteComment } from '../hooks/use-delete-comment';
import AddComment from './add-comment';

type CommentsProps = {
  slug: string;
};

function Comments({ slug }: CommentsProps) {
  const { account } = useAuth();
  const { pages, fetchNextPage, hasNextPage } = useCommentList(slug);
  const { deleteComment } = useDeleteComment();

  return (
    <Box>
      <Heading fontSize="2xl" as="h2">
        Comments
      </Heading>

      <AddComment slug={slug} />

      <InfiniteScroll
        dataLength={pages.length}
        next={() => fetchNextPage()}
        hasMore={hasNextPage ?? false}
        loader={<Text>Loading...</Text>}
      >
        {pages.map((page) =>
          page.items.map((comment) => (
            <Box mb={4} key={comment.id} borderWidth="1px" p={4} borderRadius="lg">
              <Flex alignItems="center">
                <Link to={`/${comment.author.username}`}>
                  <Text fontSize="sm" fontWeight="600">
                    {comment.author.name}
                  </Text>
                </Link>

                <Box as="span" mx={1}>
                  •
                </Box>

                <ReactTimeago component={Text} fontSize="sm" date={comment.createdAt} />

                {account?.username === comment.author.username && (
                  <Box ml="auto">
                    <IconButton
                      variant="ghost"
                      aria-label="Delete comment"
                      title="Delete comment"
                      icon={<AiOutlineDelete fontSize="1.5rem" />}
                      size="sm"
                      onClick={() =>
                        deleteComment({
                          commentId: comment.id,
                          slug,
                        })
                      }
                    />
                  </Box>
                )}
              </Flex>

              <Text>{comment.message}</Text>
            </Box>
          )),
        )}
      </InfiniteScroll>
    </Box>
  );
}

export default Comments;
