import React, { lazy, useEffect } from 'react';
import { Outlet, Route, Routes, useLocation, useNavigate, useSearchParams } from 'react-router-dom';

import { useAuth } from '@hooks/use-auth';

const Login = lazy(() => import('@screens/auth/login'));
const Register = lazy(() => import('@screens/auth/register'));
const CreatePost = lazy(() => import('@screens/posts/create-post'));
const EditPost = lazy(() => import('@screens/posts/edit-post'));
const Home = lazy(() => import('@screens/miscellaneous/home'));
const NotFound = lazy(() => import('@screens/miscellaneous/not-found'));
const ViewPost = lazy(() => import('@screens/posts/view-post'));
const ListPosts = lazy(() => import('@screens/posts/list-posts'));
const ViewProfile = lazy(() => import('@screens/profiles/view-profile'));

export default function Router() {
  return (
    <Routes>
      <Route path="/" element={<ListPosts />} />
      <Route path="/t/:tag" element={<ListPosts />} />
      <Route path="/:username" element={<ViewProfile />} />
      <Route path="/:username/:slug" element={<ViewPost />} />
      <Route path="*" element={<NotFound />} />

      <Route element={<RequireUnauthenticated />}>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Route>

      <Route element={<RequireAuthentication />}>
        <Route path="/create-post" element={<CreatePost />} />
        <Route path="/:username/:slug/edit" element={<EditPost />} />
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
      navigate(`/login?redirect=${location.pathname}`);
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
      navigate(params.get('redirect') ?? '/');
    }
  }, [auth.account]);

  return <Outlet />;
}
