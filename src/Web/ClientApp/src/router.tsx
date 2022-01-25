import React, { lazy, useEffect } from 'react';
import { Outlet, Route, Routes, useNavigate } from 'react-router-dom';

import { routes } from './constants';
import { useAuth } from './hooks';

const CreateAccount = lazy(() => import('./screens/register'));
const CreatePost = lazy(() => import('./screens/create-post'));
const Home = lazy(() => import('./screens/home'));
const Login = lazy(() => import('./screens/login'));
const NotFound = lazy(() => import('./screens/not-found'));
const Post = lazy(() => import('./screens/post'));
const Profile = lazy(() => import('./screens/profile'));

export function Router() {
  return (
    <Routes>
      <Route path={routes.HOME} element={<Home />} />
      <Route path={routes.LOGIN} element={<Login />} />
      <Route path={routes.REGISTER} element={<CreateAccount />} />
      <Route path={`${routes.PROFILE}/:username`} element={<Profile />} />
      <Route path="/:username/:slug" element={<Post />} />
      <Route path="*" element={<NotFound />} />

      <Route element={<RequireAuth />}>
        <Route path={routes.CREATE_POST} element={<CreatePost />} />
      </Route>
    </Routes>
  );
}

function RequireAuth() {
  const auth = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (!auth.account && !auth.initializing) {
      navigate(routes.LOGIN, { replace: true });
    }
  }, [auth.account, auth.initializing]);

  return <Outlet />;
}
