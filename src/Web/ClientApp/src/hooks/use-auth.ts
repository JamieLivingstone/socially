import axios from 'axios';
import { createContext, useContext, useEffect, useState } from 'react';
import { useCookies } from 'react-cookie';

type Account = {
  name: string;
  username: string;
  email: string;
};

export function useProvideAuth() {
  const [account, setAccount] = useState<Account | null>(null);
  const [initializing, setInitializing] = useState(true);
  const [cookies, setCookie, deleteCookie] = useCookies(['token']);

  useEffect(() => {
    (async function initialize() {
      if (cookies.token) {
        await fetchAccount(cookies.token);
      }

      setInitializing(false);
    })();
  }, []);

  async function fetchAccount(token: string) {
    try {
      axios.defaults.headers.common = { Authorization: `Bearer ${token}` };

      const { data } = await axios.get('/api/v1/accounts/current');

      setAccount(data);
    } catch (_) {
      logout();
    }
  }

  async function login(payload: { username: string; password: string }) {
    const { data } = await axios.post<{ token: string }>('/api/v1/accounts/login', payload);

    setCookie('token', data.token, {
      path: '/',
      sameSite: 'strict',
      maxAge: 604800, // 7 days
    });

    await fetchAccount(data.token);
  }

  async function register(payload: { name: string; username: string; email: string; password: string }) {
    const { data } = await axios.post<{ token: string }>('/api/v1/accounts/register', payload);

    setCookie('token', data.token, {
      path: '/',
      sameSite: 'strict',
      maxAge: 604800, // 7 days
    });

    await fetchAccount(data.token);
  }

  function logout() {
    deleteCookie('token', { path: '/' });
    setAccount(null);
  }

  return {
    account,
    initializing,
    login,
    register,
    logout,
  };
}

export const AuthContext = createContext<ReturnType<typeof useProvideAuth> | null>(null);

export function useAuth() {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error('useAuth must be within an AuthProvider');
  }

  return context;
}
