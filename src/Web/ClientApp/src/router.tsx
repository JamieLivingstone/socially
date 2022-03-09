import React, { lazy, useEffect } from 'react';
import { Outlet, Route, Routes, useLocation, useNavigate, useSearchParams } from 'react-router-dom';

import { routes } from './constants';
import { useAuth } from './hooks';

const Login = lazy(() => import('./screens/auth/login'));
const Register = lazy(() => import('./screens/auth/register'));
const CreatePost = lazy(() => import('./screens/posts/create-post'));
const Home = lazy(() => import('./screens/miscellaneous/home'));
const NotFound = lazy(() => import('./screens/miscellaneous/not-found'));
const ViewPost = lazy(() => import('./screens/posts/view-post'));
const ViewProfile = lazy(() => import('./screens/profiles/view-profile'));

export function Router() {
  return (
    <Routes>
      <Route path={routes.HOME} element={<Home />} />
      <Route path="/:username" element={<ViewProfile />} />
      <Route path="/:username/:slug" element={<ViewPost />} />
      <Route path="*" element={<NotFound />} />

      <Route element={<RequireUnauthenticated />}>
        <Route path={routes.LOGIN} element={<Login />} />
        <Route path={routes.REGISTER} element={<Register />} />
      </Route>

      <Route element={<RequireAuthentication />}>
        <Route path={routes.CREATE_POST} element={<CreatePost />} />
      </Route>
    </Routes>
  );
}

function RequireAuthentication() {
  const auth = useAuth();
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    if (!auth.account && !auth.initializing) {
      navigate(`${routes.LOGIN}?redirect=${location.pathname ?? '/'}`);
    }
  }, [auth.account, auth.initializing]);

  return <Outlet />;
}

function RequireUnauthenticated() {
  const auth = useAuth();
  const navigate = useNavigate();
  const [params] = useSearchParams();

  useEffect(() => {
    if (auth.account) {
      navigate(params.get('redirect') ?? routes.HOME);
    }
  }, [auth.account]);

  return <Outlet />;
}
