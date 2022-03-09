import ChakraUIRenderer from 'chakra-ui-markdown-renderer';
import React from 'react';
import ReactMarkdown from 'react-markdown';

type MarkdownProps = {
  source: string;
};

export function Markdown({ source }: MarkdownProps) {
  return (
    <ReactMarkdown components={ChakraUIRenderer()} skipHtml>
      {source}
    </ReactMarkdown>
  );
}
