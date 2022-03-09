import { HStack, Tag, TagLabel } from '@chakra-ui/react';
import React from 'react';

type TagListProps = {
  tags: Array<{ name: string }>;
};

export function TagList({ tags }: TagListProps) {
  return (
    <HStack spacing={2}>
      {tags.map((tag) => (
        <Tag colorScheme="gray" key={tag.name}>
          <TagLabel>#{tag.name}</TagLabel>
        </Tag>
      ))}
    </HStack>
  );
}
