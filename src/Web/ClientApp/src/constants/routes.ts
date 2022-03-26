export const HOME = '/';
export const LOGIN = '/login';
export const REGISTER = '/register';
export const CREATE_POST = '/create-post';

export function getProfileRoute(username: string) {
  return `/${username}`;
}

