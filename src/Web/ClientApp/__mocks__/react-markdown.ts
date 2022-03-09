import { ReactNode } from 'react';

// ESM / Jest compatability issues (see: https://github.com/remarkjs/remark/discussions/814)
function ReactMarkdown({ children }: { children: ReactNode }) {
  return children;
}

export default ReactMarkdown;
