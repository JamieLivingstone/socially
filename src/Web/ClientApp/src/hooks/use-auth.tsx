import axios from 'axios';
import React, { createContext, useContext, useEffect, useState } from 'react';
import { useCookies } from 'react-cookie';
import { useNavigate } from 'react-router-dom';

import { routes } from '../constants';
import { Loading } from '../screens/loading';

type Account = {
  name: string;
  username: string;
  email: string;
};

function useProvideAuth() {
  const [account, setAccount] = useState<Account | null>(null);
  const [initializing, setInitializing] = useState(false);
  const [cookies, setCookie, deleteCookie] = useCookies(['token']);
  const navigate = useNavigate();

  useEffect(() => {
    (async function initialize() {
      if (cookies.token) {
        setInitializing(true);
        await fetchAccount(cookies.token);
        setInitializing(false);
      }
    })();
  }, []);

  async function fetchAccount(token: string) {
    try {
      axios.defaults.headers.common = {
        Authorization: `Bearer ${token}`,
      };

      const { data } = await axios.get('/api/v1/accounts/current');

      setAccount(data);
    } catch (_) {
      logout();
    }
  }

  async function login(payload: { username: string; password: string }) {
    const { data } = await axios.post<{ token: string }>('/api/v1/accounts/login', payload);

    setCookie('token', data.token, { secure: true, sameSite: 'strict' });
    await fetchAccount(data.token);
  }

  async function register(payload: { name: string; username: string; email: string; password: string }) {
    const { data } = await axios.post<{ token: string }>('/api/v1/accounts/register', payload);

    setCookie('token', data.token, { secure: true, sameSite: 'strict' });
    await fetchAccount(data.token);
  }

  function logout() {
    deleteCookie('token');
    setAccount(null);
    navigate(routes.HOME, { replace: true });
  }

  return {
    account,
    initializing,
    login,
    register,
    logout,
  };
}

const authContext = createContext<ReturnType<typeof useProvideAuth> | null>(null);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const auth = useProvideAuth();

  return <authContext.Provider value={auth}>{auth.initializing ? <Loading /> : children}</authContext.Provider>;
}

export function useAuth() {
  const context = useContext(authContext);

  if (!context) {
    throw new Error('useAuth must be within an AuthProvider');
  }

  return context;
}
