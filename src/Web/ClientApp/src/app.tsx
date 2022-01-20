import { Box, ChakraProvider } from '@chakra-ui/react';
import React, { Suspense, lazy } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

import { Header } from './components/header';
import { routes, theme } from './constants';
import { AuthProvider } from './hooks/use-auth';
import { Loading } from './screens/loading';

const PrivacyBanner = lazy(() => import('./components/privacy-banner'));
const CreateAccount = lazy(() => import('./screens/register'));
const CreatePost = lazy(() => import('./screens/create-post'));
const Home = lazy(() => import('./screens/home'));
const Login = lazy(() => import('./screens/login'));
const NotFound = lazy(() => import('./screens/not-found'));
const Post = lazy(() => import('./screens/post'));
const Profile = lazy(() => import('./screens/profile'));

export default function App() {
  return (
    <ChakraProvider theme={theme}>
      <Box bg="gray.100" minH="100vh" display="flex" flexDirection="column">
        <Suspense fallback={<Loading />}>
          <BrowserRouter>
            <AuthProvider>
              <Header />

              <Routes>
                <Route path={routes.HOME} element={<Home />} />
                <Route path={routes.LOGIN} element={<Login />} />
                <Route path={routes.REGISTER} element={<CreateAccount />} />
                <Route path={routes.CREATE_POST} element={<CreatePost />} />
                <Route path={`${routes.PROFILE}/:username`} element={<Profile />} />
                <Route path="/:slug" element={<Post />} />
                <Route path="*" element={<NotFound />} />
              </Routes>

              <PrivacyBanner />
            </AuthProvider>
          </BrowserRouter>
        </Suspense>
      </Box>
    </ChakraProvider>
  );
}
