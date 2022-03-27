import { HStack, Tag, TagLabel } from '@chakra-ui/react';
import React from 'react';
import { Link } from 'react-router-dom';

type TagListProps = {
  tags: Array<{ name: string }>;
};

function TagList({ tags }: TagListProps) {
  return (
    <HStack spacing={2}>
      {tags.map((tag) => (
        <Tag colorScheme="gray" key={tag.name} as={Link} to={`/t/${tag.name}`}>
          <TagLabel>#{tag.name}</TagLabel>
        </Tag>
      ))}
    </HStack>
  );
}

export default TagList;
