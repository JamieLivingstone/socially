export type Post = {
  author: {
    name: string;
    username: string;
  };
  body: string;
  commentsCount: number;
  createdAt: string;
  likesCount: number;
  slug: string;
  tags: Array<{ name: string }>;
  title: string;
  updatedAt: string;
};
